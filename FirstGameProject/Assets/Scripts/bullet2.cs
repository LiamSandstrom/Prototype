using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2 : MonoBehaviour
{
    [SerializeField] public Transform shootingStartPosition;
    [SerializeField] private Rigidbody ProjectileBody;
    // Start is called before the first frame update
    public Vector3 groundDirection;
    public float v0;
    public float angle;

    public float time;
    [SerializeField] private Vector3 rotationbullet;
    void Start()
    {
        StartCoroutine(Coroutine_Movement(groundDirection.normalized, v0, angle, time));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationbullet * Time.deltaTime);
    }
     public IEnumerator Coroutine_Movement(Vector3 direction, float v0, float angle, float time)//
    {
        float t = 0;
        while(t < time)
        {
            float x = v0 * t * Mathf.Cos(angle);
            float y = v0 * t * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t,2);
            transform.position = shootingStartPosition.position + direction * x + Vector3.up * y;
            t += Time.deltaTime;
            yield return null;
        }
    }
     private void OnTriggerEnter(Collider collision)
    {
        GameObject collisionObject = collision.gameObject;
        
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
