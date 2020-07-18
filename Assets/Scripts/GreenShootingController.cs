using UnityEngine;


//Shooting Controller Requires Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class GreenShootingController : MonoBehaviour
{
    #region Variables
    [Header("Shooting Radius Setting"), SerializeField, Range(10, 250)]
    private float shootingRadius = 100f;

    private int cubeDamage = 1;

    private float fireRate = 1f; //1 shot per second

    private float nextShotTime = 1f; //shooting interval

    private Vector3 origin;

    public LayerMask redLayerMask; //The RED Layermask goes here so the OverlapShpere shoots only the enemy team.
    #endregion

   
    //Using the Fixed update for standard check intervals independent of framerate
    private void FixedUpdate()
    {
        //updating the origin position as the cube is orbiting around the Planet.
        origin = transform.position;

        //A check to see if 1 second has passed so the cube can fire again.
        if (Time.time > nextShotTime)
        {
            GreenCubeShoot();
            nextShotTime = Time.time + fireRate;
        }
    }

    //Method that activates an OverlapSphere and shoots the nearest target
    private void GreenCubeShoot()
    {
        Collider colliderToHit = null;

        Collider[] hitColliders = Physics.OverlapSphere(origin, shootingRadius, redLayerMask);

        colliderToHit = FindNearestTarget(hitColliders); 

        if(colliderToHit != null)
        {
            colliderToHit.gameObject.GetComponent<CubeController>().TakeDamage(cubeDamage);
        }
    }

    //Method that finds and returns the nearest Red Cube collider
    private Collider FindNearestTarget(Collider[] colliders)
    {
        //Initialize nearest distance with a huge number
        float nearestEnemyDistance = Mathf.Infinity;
        float distance;

        Collider colliderToHit = null;

        foreach (Collider collider in colliders)
        {
            distance = (transform.position - collider.transform.position).sqrMagnitude;
            if (distance < nearestEnemyDistance)
            {
                nearestEnemyDistance = distance;
                colliderToHit = collider;
            }
        }

        return colliderToHit;
    }

    //If you select any cube from the Scene View then this method draws a GREEN sphere for a graphical representation
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(origin, shootingRadius);
    }
}
