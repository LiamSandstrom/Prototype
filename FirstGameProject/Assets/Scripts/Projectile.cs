using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody ProjectileBody;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Initialize()
    {

        
        isActive = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            //transform.Translate(transform.forward * speed * Time.deltaTime);

          
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        
        
        Debug.Log("Aaaaaaaa");
        if(collisionObject.TryGetComponent<HealthComponent>(out HealthComponent hcomp))
        {
           collisionObject.GetComponent<HealthComponent>().DamageEnemy(50f);
           Destroy(gameObject); 
        }
        else
        {
            Destroy(gameObject);  
        }
         
    }
}
