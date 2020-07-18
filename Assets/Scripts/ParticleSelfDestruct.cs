using UnityEngine;


//A short script that destroys the particle effect after 1 second
public class ParticleSelfDestruct : MonoBehaviour
{
	void Start()
	{
		Destroy(gameObject, 1.0f);
	}
}
