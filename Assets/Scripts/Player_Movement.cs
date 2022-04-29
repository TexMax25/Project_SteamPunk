using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float gravity = 20f;
    public float fallVelocity;
    public float jumpForce;
    public float slideVelocity;
    public float SlopeForceDown;

    private Vector3 movePlayer;
    private Vector3 PlayerInput;
    

    private Animator anim;
    public CharacterController player;

    private float HorizontalMove, VerticalMove;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    public bool isOnSlope = false;
    private Vector3 hitNormal;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HorizontalMove = Input.GetAxis("Horizontal");
        VerticalMove = Input.GetAxis("Vertical");

        PlayerInput = new Vector3(HorizontalMove, 0, VerticalMove);
        PlayerInput = Vector3.ClampMagnitude(PlayerInput, 1);



        CamDirection();

        movePlayer = PlayerInput.x * camRight + PlayerInput.z * camForward;
        movePlayer = movePlayer * movementSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);
        


        SetGravity();
        PlayerMovements();

        player.Move(movePlayer * Time.deltaTime);

        anim.SetFloat("VelX", HorizontalMove);
        anim.SetFloat("VelY", VerticalMove);
        

        
      
    }

//Funcion para movieminto de personaje segun la camara
    public void CamDirection()
    {

        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;

    }

// Funcion para la gravedad
    public void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
            
        }
        else
        {
           fallVelocity -= gravity * Time.deltaTime;
           movePlayer.y = fallVelocity; 
           anim.SetFloat("VerticalVelocity", player.velocity.y);
        }
        anim.SetBool("Grounding", player.isGrounded);
        SlideDown();
    }

// funcion para los movimientos del personaje
    public void PlayerMovements()
    {
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
            anim.SetTrigger("Jumping");
            
        }

    }


    //funcion para Comparar si esta o no en rampa y deslizarse
    public void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;

        if (isOnSlope)
        {
            movePlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
            movePlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;

            movePlayer.y += SlopeForceDown;
        }
    }


    //funcion para detectar normales
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;

    }

    private void OnAnimatorMove()
    {

    }


}
