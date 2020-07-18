using UnityEngine;


//A short script that rotates the Planet.
public class PlanetRotation : MonoBehaviour
{

    public float rotationSpeed = 5f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
