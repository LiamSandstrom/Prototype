using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManeger : MonoBehaviour
{
    private static PickupManeger instance;

    [SerializeField] GameObject pickupPrefab;

    private void Awake()
    {
        if(instance = null)
        {
            instance = this;
        }
        else
        {
            //Destroy(this);
        } 
    }
    public static PickupManeger GetInstance()
    {
        return instance;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("aaaaa");
            GameObject newPickup = Instantiate(pickupPrefab);
            newPickup.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z);
        }
    }
}
