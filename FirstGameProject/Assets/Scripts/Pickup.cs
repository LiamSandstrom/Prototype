using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    CharacterWeapon characterWeapon;
    Firewithtrejectory firewithtrejectory;

    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider collider)
    {
        firewithtrejectory = collider.gameObject.GetComponent<Firewithtrejectory>();
        firewithtrejectory.addammo();
        Destroy(gameObject);
    }
}
