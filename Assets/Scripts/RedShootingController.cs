﻿using UnityEngine;

//Shooting Controller Requires Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class RedShootingController : MonoBehaviour
{
    #region Variables

    // The firing range of the Cube.Can be modified via the inspector.

    [Header("Shooting Radius Setting"), SerializeField, Range(50, 300)]
    private float shootingRadius = 150f; 

    private readonly int cubeDamage = 1; 

    private readonly float fireRate = 1f; //1 shot per second.

    private float nextShotTime = 0f; //shooting interval.

    private Vector3 origin; //The origin point of the Cube (its center).

    //The line to be drawn when the cube shoots.
    public LineRenderer lineRenderer;

    //The GREEN Layermask goes here so the OverlapShpere shoots only the enemy team.
    public LayerMask greenLayerMask;

    //The PLANET Layermask goes here so if the planet is in the way of the cubes they cannot shoot each other.
    public LayerMask planetLayerMask;
    #endregion


    //Using the Fixed update for standard check intervals independent of framerate
    private void FixedUpdate()
    {
        //updating the origin position as the cube is orbiting around the Planet.
        origin = transform.position;

        //A check to see if 1 second has passed so the cube can fire again.
        if (Time.time > nextShotTime)
        {
            RedCubeShoot();
            nextShotTime = Time.time + fireRate;
        }
    }

    //Method that activates an OverlapSphere and shoots the nearest target.
    private void RedCubeShoot()
    {
        Collider colliderToHit = null;

        Collider[] hitColliders = Physics.OverlapSphere(origin, shootingRadius, greenLayerMask);

        colliderToHit = FindNearestTarget(hitColliders);

        if (colliderToHit != null)
        {
            //Check if the planet is NOT in the way so the cubes cannot shoot each other. 
            if (!Physics.Raycast(origin, colliderToHit.transform.position - origin, shootingRadius, planetLayerMask, QueryTriggerInteraction.Collide))
            {
                //Draw the Hit Line and apply damage to the enemy.
                DrawHitLine(colliderToHit);
                colliderToHit.gameObject.GetComponent<CubeController>().TakeDamage(cubeDamage);
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

    //Method that finds and returns the nearest Green Cube collider.
    private Collider FindNearestTarget(Collider[] colliders)
    {
        //Initialize nearest distance with a huge number.
        float nearestEnemyDistance = Mathf.Infinity;
        float distance;

        Collider colliderToHit = null;

        foreach (Collider collider in colliders)
        {
            //Calculate the distance between the cube and the target.
            distance = (transform.position - collider.transform.position).sqrMagnitude;
            if (distance < nearestEnemyDistance)
            {
                nearestEnemyDistance = distance;
                colliderToHit = collider;
            }
        }

        return colliderToHit;
    }

    //Method that draws a line from the Cube that's shooting to the cube that's being shot at.
    private void DrawHitLine(Collider col)
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, col.transform.position);
    }

    //If you select any cube from the Scene View or Hierarchy then this method draws a RED sphere for a graphical representation.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin, shootingRadius);
    }
}