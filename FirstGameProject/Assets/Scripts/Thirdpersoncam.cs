using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Thirdpersoncam : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook playercam;
    [SerializeField] CinemachineFreeLook combatCam;
    [SerializeField] CinemachineFreeLook combatCam2;
    
    [SerializeField] CinemachineFreeLook playercam2;
    [Header("References")]
    public Transform orientation;
    public Transform orientation2;
    public Transform player;
    public Transform player2;
    public Transform playerObj;
    public Transform playerObj2;
    public Rigidbody rb;
    public Rigidbody rb2;
    
    public CameraStyle currentStyle;
    public Transform combatLookAt;
    public Transform combatLookAt2;

    public enum CameraStyle
    {
        Basic,
        Combat
    }

    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState =CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
 
        //rotate orientation
        if(TurnManager.GetInstance().IsItPlayerTurn(1))
        {
        //rotate player object
        if(currentStyle == CameraStyle.Basic)
        {
            playercam.Priority = playercam2.Priority + combatCam.Priority +combatCam2.Priority +1;
            Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y,transform.position.z);
            orientation.forward = viewDir.normalized;
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if(inputDir !=Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }
        if(currentStyle == CameraStyle.Combat)
        {
            combatCam2.Priority =1;
            playercam2.Priority = 1;
            playercam.Priority = 1;
            combatCam.Priority = playercam.Priority + playercam2.Priority +combatCam2.Priority +1;
            Vector3 DirToCombatLook = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y,transform.position.z);
            orientation.forward = DirToCombatLook.normalized;
            playerObj.forward = DirToCombatLook.normalized;
        }


        }
        if(TurnManager.GetInstance().IsItPlayerTurn(2))
        {
        
        Vector3 viewDir = player2.position - new Vector3(transform.position.x, player2.position.y,transform.position.z);
        orientation2.forward = viewDir.normalized;

        //rotate player object
        if(currentStyle == CameraStyle.Basic)
        {
            playercam2.Priority= playercam.Priority + combatCam.Priority + combatCam2.Priority +1;
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation2.forward * verticalInput + orientation2.right * horizontalInput;

            if(inputDir !=Vector3.zero)
            {
                playerObj2.forward = Vector3.Slerp(playerObj2.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }
        if(currentStyle == CameraStyle.Combat)
        {
            combatCam.Priority =1;
            playercam2.Priority = 1;
            playercam.Priority = 1;
            combatCam2.Priority = playercam.Priority + playercam2.Priority + combatCam.Priority +1;
            Vector3 DirToCombatLook = combatLookAt2.position - new Vector3(transform.position.x, combatLookAt2.position.y,transform.position.z);
            orientation.forward = DirToCombatLook.normalized;
            playerObj2.forward = DirToCombatLook.normalized;
        }

        }
        
    }
}
