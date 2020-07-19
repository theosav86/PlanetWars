using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CubeController : MonoBehaviour
{
    #region Variables

    private readonly int cubeHealthPoints = 3;

    private int currentHealthPoints;

    private Vector3 randomAxis;

    //The center of the planet in the Scene. The cube rotates around it.
    public Transform worldCenter; 

    //The speed at which the cubes are orbiting around the planet. Can be modified via the Inspector.
    //[Header("Orbit Speed Setting"), Range(5, 50)] initial range was 20
    private int orbitSpeed;

    //Slot for the health text under the Cubes Canvas.
    [Header("Reference to UI Element")]
    public TextMeshProUGUI healthBarText;

    //slot for the explosion particle effect.
    [Header("Particle Effect")]
    public GameObject explosion;

    #endregion

    //Initialize health points and select a random axis.
    void Start()
    {
        currentHealthPoints = cubeHealthPoints;

        randomAxis = SelectRandomAxis();

        orbitSpeed = Random.Range(20, 26);
    }

    //Method that returns a random Axis.
    private Vector3 SelectRandomAxis()
    {
        int randomAxisIndex = UnityEngine.Random.Range(0, 3);

        //0 is for X axis.
        //1 is for Y axis.
        //2 is for Z axis.
        switch (randomAxisIndex)
        {
            case 0:
                return Vector3.right;
            case 1:
                return Vector3.up;
            case 2:
                return Vector3.forward;
        }

        return Vector3.zero; 
    }

    //Executing movement in Fixed Update.
    private void FixedUpdate()
    {
        CubeMovement(); 
    }

    //Method that makes the cube rotate around the Planet (world center).
    private void CubeMovement()
    {
        transform.RotateAround(worldCenter.position, randomAxis, orbitSpeed * Time.deltaTime);
        transform.LookAt(worldCenter);
    }

   
    //Method that applies damage to the Cube, also the number above the Cube gets updated here.
    public void TakeDamage(int damageValue)
    {
        currentHealthPoints -= damageValue;

        healthBarText.text = currentHealthPoints.ToString();

        if(currentHealthPoints <= 0)
        {
            CubeDeath();
        }
    }

    //Just before the Cube gets destroyed it calls the corresponding event to update the score value.
    private void OnDisable()
    {
        //Call appropriate event to reduce score depending the cube's color.
        if (transform.gameObject.CompareTag("GreenCube"))
        {
            ScoreBroker.CallGreenCubeKilled();
        }
        else if (transform.gameObject.CompareTag("RedCube"))
        {
            ScoreBroker.CallRedCubeKilled();
        }
    }

    //Method that kills the Cube.
    private void CubeDeath()
    {
        //Play Destruction Particles effect. The particle effect has a self desruct script.
        Instantiate(explosion, transform.position, Quaternion.identity);

        //Destroy game object.
        Destroy(gameObject);
    }
}