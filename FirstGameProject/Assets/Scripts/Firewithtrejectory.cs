using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewithtrejectory : MonoBehaviour
{
    [SerializeField] float _InitialVelocity;
    [SerializeField] float _Angle;
    [SerializeField] public LineRenderer _Line;
    [SerializeField] float _Step;
    [SerializeField] public Camera cam;
    [SerializeField]  public Transform shootingStartPosition;
    [SerializeField] private GameObject projectilePrefab;
    public int index;
    public int ammo;
    public CharacterController characterController;
    private void Start()
    {
        characterController = transform.GetComponentInParent<CharacterController>();
    }
    // Update is called once per frame
    private void Update()
    {

        if(TurnManager.GetInstance().IsItPlayerTurn(index))
        {
            if(cam.GetComponent<Thirdpersoncam>().currentStyle == Thirdpersoncam.CameraStyle.Combat)
            {
                if(ammo > 0)
                {
                    _Line.gameObject.SetActive(true);
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if(Physics.Raycast(ray, out hit))
                    {
                        Vector3 direction =hit.point - shootingStartPosition.position;
                        Vector3 groundDirection = new Vector3(direction.x, 0, direction.z);
                        Vector3 targetPos = new Vector3(groundDirection.magnitude, direction.y, 0);
                        float height = targetPos.y + targetPos.magnitude /2f;
                        height = Mathf.Max(0.01f, height);
                        float angle;
                        float v0;
                        float time;
                        CalculatePathWithHeight(targetPos, height, out v0, out angle,out time);

                        DrawPath(groundDirection.normalized, v0,angle,time, _Step);
                        if(Input.GetKey(KeyCode.Mouse0))
                        {
                            if(characterController.fired == false)
                            {
                            StopAllCoroutines();
                            //StartCoroutine(Coroutine_Movement(groundDirection.normalized, v0, angle, time));
                            GameObject newProjectile = Instantiate(projectilePrefab, shootingStartPosition.position, Quaternion.identity);
                            newProjectile.GetComponent<bullet2>().groundDirection = groundDirection;
                            newProjectile.GetComponent<bullet2>().v0 = v0;
                            newProjectile.GetComponent<bullet2>().angle = angle;
                            newProjectile.GetComponent<bullet2>().time = time;
                            ammo -= 1;
                            characterController.Removespeed();
                            characterController.Firedweapon();
                            Debug.Log(characterController.speed);
                            }
                        }
                    }
                }
            }
        else
        {
            
            _Line.gameObject.SetActive(false);
        }
    }
      else
        {
            characterController.resetspeed();
            characterController.speed = 400;
            _Line.gameObject.SetActive(false);
        }
      

    }
    
    private void DrawPath(Vector3 direction, float v0, float angle, float time, float step) //
    {
        step = Mathf.Max(0.01f, step);
        _Line.positionCount = (int)(time / step) +2;
        int count = 0;
        for (float i = 0; i < time; i += step)
        {
            float x=v0 * i * Mathf.Cos(angle);
            float y= v0 * i * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(i, 2);
            _Line.SetPosition(count, shootingStartPosition.position + direction * x + Vector3.up * y);
            count++;
        }
        float xfinal = v0 * time * Mathf.Cos(angle);
        float yfinal = v0 * time * Mathf.Sin(angle) -0.5f * -Physics.gravity.y * Mathf.Pow(time, 2);
        _Line.SetPosition(count, shootingStartPosition.position + direction * xfinal + Vector3.up * yfinal);
    }

    IEnumerator Coroutine_Movement(Vector3 direction, float v0, float angle, float time)//
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

    private void CalculatePath(Vector3 targetPos, float angle, out float v0,out float time)
    {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float v1 = Mathf.Pow(xt,2) *g;
        float v2 = 2* xt * Mathf.Sin(angle) * Mathf.Cos(angle);
        float v3 = 2 * yt * Mathf.Pow(Mathf.Cos(angle), 2);
        v0 = Mathf.Sqrt(v1 / (v2-v3));

        time = xt/(v0 * Mathf.Cos(angle));
    }
    private void CalculatePathWithHeight(Vector3 targetPos, float h, out float v0, out float angle, out float time)//
    {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float b = Mathf.Sqrt(2 * g * h);
        float a = (-0.5f * g);
        float c = -yt;

        float tplus = QuadracticEquation(a,b,c,1);
        float tmin= QuadracticEquation(a,b,c, -1);
        time = tplus > tmin ? tplus : tmin;

        angle= Mathf.Atan(b*time / xt);
        v0 = b / Mathf.Sin(angle);
    }
        private float QuadracticEquation(float a, float b, float c, float sign)
    {
        return (-b + sign * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
    }

    public void addammo()
    {
        ammo +=1;
    }
}
