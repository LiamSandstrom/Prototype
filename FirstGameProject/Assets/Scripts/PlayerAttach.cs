using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttach : MonoBehaviour
{
    public GameObject player;
    public GameObject player2;
    public GameObject playerp;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            player.transform.parent = transform;
            Debug.Log("bb");
        }
        else if(other.gameObject == player2)
        {
            player2.transform.parent = transform; 
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            player.transform.parent = playerp.transform;
        }  
        else if(other.gameObject == player2)
        {
            player2.transform.parent = null; 
        }
        
    }
}
