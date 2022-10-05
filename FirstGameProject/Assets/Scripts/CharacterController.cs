using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
 
    // Update is called once per frame
    public float speed = 400f;
    [SerializeField] private float jumpspeed = 10000f;
    [SerializeField]private Camera cam;
     Rigidbody myrigidbody;
    GameObject jumphitbox;
    GameObject jumphitbox1;
    GameObject jumphitbox2;
    GameObject jumphitbox3;
    GameObject jumphitbox4;
    Jumpcheak jumpcheak;
    Jumpcheak jumpcheak1;
    Jumpcheak jumpcheak2;
    Jumpcheak jumpcheak3;
    Jumpcheak jumpcheak4;

    HealthComponent hitHealthcompnent;
    //int layerMask = 1 << 6;
    
    [SerializeField] private MeshRenderer meshRenderer;

    public int index;

    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    public float groundDrag;
    Animator animator;
    float runningSpeed = 530;
    public float Movetimer;
    public float turntime = 3f;
    TurnManager turnManager;
    public bool fired;
   
   void Start()
   {
    myrigidbody = GetComponent<Rigidbody>();
    myrigidbody.freezeRotation = true;
    turnManager = cam.GetComponent<TurnManager>();
   
    animator = transform.GetChild(5).gameObject.GetComponent<Animator>();

    jumphitbox = transform.GetChild(0).gameObject;
    jumphitbox1 = transform.GetChild(1).gameObject;
    jumphitbox2 = transform.GetChild(2).gameObject;
    jumphitbox3 = transform.GetChild(3).gameObject;
    jumphitbox4 = transform.GetChild(4).gameObject;
    jumpcheak = jumphitbox.GetComponent<Jumpcheak>();
    jumpcheak1 = jumphitbox1.GetComponent<Jumpcheak>();
    jumpcheak2 = jumphitbox2.GetComponent<Jumpcheak>();
    jumpcheak3 = jumphitbox3.GetComponent<Jumpcheak>();
    jumpcheak4 = jumphitbox4.GetComponent<Jumpcheak>();
   }


     void Update()
    {
        if(TurnManager.GetInstance().IsItPlayerTurn(index))
        {
            if(fired != true)
            {
                if(jumpcheak.IsGrounded() || jumpcheak1.IsGrounded() || jumpcheak2.IsGrounded() || jumpcheak3.IsGrounded() || jumpcheak4.IsGrounded())
                {
                    myrigidbody.drag = groundDrag;
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                                    
                        jump();
                        animator.Play("jump");
                    }   
                }
                else
                {
                   animator.SetBool("CanJump", false);
                   myrigidbody.drag = 1.5f;
                }  
                if(Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical") !=0)
                {
                        
                    Movetimer += Time.deltaTime;
                    if(Movetimer <= turntime)
                    {
                        MovePlayer(); 
                        MyInput();
                        SpeedControl();
                        if(Input.GetKey(KeyCode.LeftShift))
                        {
                            CharacterRun();
                        }
                        else
                        {
                            if(fired != true)
                            {
                                speed = 400;
                            }                      
                        }

                    }                        
                }   
                else
                {
                   Removespeed();
                }
                if(Input.GetKeyDown(KeyCode.Mouse1))
                {
                    if(cam.GetComponent<Thirdpersoncam>().currentStyle == Thirdpersoncam.CameraStyle.Combat)
                    {
                        cam.GetComponent<Thirdpersoncam>().currentStyle = Thirdpersoncam.CameraStyle.Basic;
                    }
                    else if(cam.GetComponent<Thirdpersoncam>().currentStyle == Thirdpersoncam.CameraStyle.Basic)
                    {
                        cam.GetComponent<Thirdpersoncam>().currentStyle = Thirdpersoncam.CameraStyle.Combat;
                    }     
                }
            }

        }
        else
        {
            Movetimer = 0;
            Removespeed();
            resetspeed();
        }
        
    
    }
    public void Firedweapon()
    {
        fired = true;
    }
    public void resetspeed()
    {
        fired = false;
    }
    public void Removespeed()
    {
        speed = 0;
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsRunning", false);
    }
    private void FixedUpdate()
    {
       
    }

    private void jump()
    {
        myrigidbody.AddForce(transform.up * jumpspeed);
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if(horizontalInput.Equals(0) && verticalInput.Equals(0))
        {
            animator.SetBool("IsMoving", false);
        }
        else
        {
            animator.SetBool("IsMoving", true);
        }
    }

    private void MovePlayer()
    {
        //walk direction looking
        moveDirection =orientation.forward * verticalInput + orientation.right * horizontalInput;

        myrigidbody.AddForce(moveDirection.normalized * speed *10f * Time.deltaTime, ForceMode.Force);
    }
    private void CharacterRun()

    {
        speed = runningSpeed;
    }

    private void SpeedControl()
    {
        //limit velocity
        Vector3 flatVel = new Vector3(myrigidbody.velocity.x,0f,myrigidbody.velocity.z);
        if ( flatVel.magnitude > 3.5f)
        {
            animator.SetBool("IsRunning",true);
        }
        else
        {
            animator.SetBool("IsRunning",false);
        }

        if(flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            myrigidbody.velocity = new Vector3(limitedVel.x, myrigidbody.velocity.y, limitedVel.z);
        }
    }


    
}
