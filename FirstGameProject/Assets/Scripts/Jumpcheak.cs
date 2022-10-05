using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpcheak : MonoBehaviour
{
    public bool Grounded;
    private float distancecheak = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down, Color.green, distancecheak);
        if(Physics.Raycast(transform.position,Vector3.down, distancecheak))
        {
            Grounded = true;
        }
        else 
        {
            Grounded = false;
        }
    }

    public bool IsGrounded()
    {
        return Grounded;
    }



}
