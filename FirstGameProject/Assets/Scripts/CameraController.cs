using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public Transform target2;
    public Vector3 offset;
    
    public float pitch = 2f;
    // Start is called before the first frame update
    public float ZoomSpeed = 2f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float yawSpeed = 500f;

    private float currentZoom = 10f;
    private float yawInput = 0f;
   
    //private float upInput = 0f;

  
  
    void Start()
    {
  
    }
    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        
        //Get this drag camera to work
        if(Input.GetMouseButton(1)) 
        {
            //yawInput += Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
            //upInput += Input.GetAxis("Mouse Y") * yawSpeed * Time.deltaTime;
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(GetComponent<TurnManager>().IsItPlayerTurn(2))
        {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, yawInput);
        }
        else if((GetComponent<TurnManager>().IsItPlayerTurn(1)))
        {
        transform.position = target2.position - offset * currentZoom;
        transform.LookAt(target2.position + Vector3.up * pitch);
        transform.RotateAround(target2.position, Vector3.up, yawInput);
        }

      
        
    }
}
