using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private LineRenderer lineRenderer;
    

    public void DrawCurvedTrajectory(Vector3 force)
    {
        float projectileMass = projectilePrefab.GetComponent<Rigidbody>().mass;
        Vector3 velocity = (force / projectileMass) * Time.fixedDeltaTime;
        //float flightDuration = (2 *)
    }
}
