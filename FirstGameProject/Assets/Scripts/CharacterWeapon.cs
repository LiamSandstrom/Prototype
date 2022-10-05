using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootingStartPosition;
    public bool Activated;
    [SerializeField]private Camera cam;
    public float shootforce;
    public float upwardsforce;
    public int charindex;
    
 

    // Update is called once per frame
    void Start()
    {
        //charindex = GetComponent<CharacterController>().index;
    }
    void Update()
    {
        if(TurnManager.GetInstance().IsItPlayerTurn(charindex))
        if(Activated)
        {
            if(cam.GetComponent<Thirdpersoncam>().currentStyle == Thirdpersoncam.CameraStyle.Basic)
            {
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Ray ray = cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
                    RaycastHit hit;

                    Vector3 targetPoint;
                    if(Physics.Raycast(ray, out hit))
                    {
                        targetPoint = hit.point;
                    }
                    else
                    {
                        targetPoint = ray.GetPoint(75);
                    }
                    Vector3 directionWithoutSpread = targetPoint- shootingStartPosition.position;
                    GameObject newProjectile = Instantiate(projectilePrefab, shootingStartPosition.position, Quaternion.identity);
                    newProjectile.transform.forward = directionWithoutSpread.normalized;

                    newProjectile.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootforce, ForceMode.Impulse);
                    newProjectile.GetComponent<Rigidbody>().AddForce(cam.transform.up * upwardsforce, ForceMode.Impulse);
                }
            }
        }

    }

    public void ActivateWep()
    {
        Activated = true;
    }
}
