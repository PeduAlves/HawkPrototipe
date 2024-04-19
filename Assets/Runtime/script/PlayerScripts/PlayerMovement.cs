using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInputs inputs;
    public CharacterController controller;
    public float speed = 12f;
    public float speedScale = 1f;
    public float gravity = -19.62f;
    public float gravityScale = 1f;
    public float jumpHeight = 5f;
    public GameObject groundChecker;
    public LayerMask groundMask;
    private bool isGrounded;
    private Vector3 jumpVector = new(0, 0, 0);
    public float dashTime = 0.025f;
    public float dashSpeed = 0.2f;
    public float jumpSpeed = 0.2f;
    public float climbSpeed = 10f;
    public bool facingRight = true;
    private bool isClimb = false; 
    private bool isDash = false;
    public float waitClimb = 0.3f;
    public BoxCollider HawkEyeCollider;   
    public float HawkEyeTimer = 10f;
    public bool isHawkEyeReady = true;
    public bool isHawkEyeActive = false;

    public static PlayerMovement Instance;

    private void Awake() => Instance = this;

    private void Start() {
        HawkEyeCollider.enabled = false;
    }

    public bool GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundChecker.transform.position, 0.3f, groundMask);
        return isGrounded;
    }
    
    public void PlayerGravity(){
            
            if(!isClimb){
                
                jumpVector.y += gravity* gravityScale * Time.deltaTime;

                if(GroundCheck() && jumpVector.y < 0){
                    jumpVector.y = -2f;
                }

                controller.Move(jumpVector * Time.deltaTime);
            } 
        }
    
    public void Jump(){
        //print(groundChecker.IsGrounded());
        if(GroundCheck() && inputs.GetJumpInput()){
            
            jumpVector.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            controller.Move(jumpVector * jumpSpeed * Time.deltaTime);
        }
    }

    public void Walk(){
        
        if(isDash) return;

        float horizontalInput = inputs.GetHorizontalInput();
        int UpInput = inputs.GetUpInput() ? 1 : 0;

        //função para verificar a direção que o personagem está olhando
        if (horizontalInput > 0){

            facingRight = true;
        }
        else if (horizontalInput < 0){

            facingRight = false;
        }

        //função para rotacionar o personagem
        if (horizontalInput != 0){

            Vector3 moveDirection = new Vector3(0,UpInput,horizontalInput);
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        //função para mover o personagem
        Vector3 horizontalMove = new Vector3(0,0, horizontalInput);
        controller.Move(horizontalMove * Time.deltaTime * speed * speedScale);
    }

    public void Crouch(){

        if(inputs.GetCrouchInput() && GroundCheck()){

            speed = 6f;
            transform.localScale = new Vector3(1, 0.5f, 1);
        }else{
                
            speed = 12f;
            transform.localScale = new Vector3(1, 1, 1);   
        }
   }

    public IEnumerator Dash(){
        
        isDash = true;
        float dashSide = facingRight ? 1 : -1;
        float atualTime = 0;
        while(atualTime <= dashTime){
            controller.Move( new Vector3(0, 0, dashSide * dashSpeed));
            atualTime += Time.deltaTime;
            yield return null;
        }
        isDash = false;
        yield return new WaitForSeconds(0.5f);   
       
    }

    public void Aim(){

        if(inputs.GetAimInput()){

            speed = 0f;
        }
    }

    public void HawkEyeSkill(){

        if(inputs.GetHawkEyeInput() && isHawkEyeReady && !isHawkEyeActive){

            print("HawkEye");
            StartCoroutine(HawkEyeCourotine());
        }
    }
    IEnumerator HawkEyeCourotine(){
        
        isHawkEyeActive = true;
        HawkEyeCollider.enabled = true;
        print("HawkEye active");
        yield return new WaitForSeconds(HawkEyeTimer);
        StopHawkEye();
    }
    public void StopHawkEye(){

        StopCoroutine(HawkEyeCourotine());
        HawkEyeCollider.enabled = false;
        print("HawkEye desactive");
        isHawkEyeActive = false;
    }
    public void Climb(){

        StartCoroutine(ClimbLadder());
    }
    public void noClimb(){
        
        isClimb = false;
        StopCoroutine(ClimbLadder());
    }

    IEnumerator ClimbLadder(){

        isClimb = true;
        Vector3 verticalMove = new Vector3(0, 5, 0);
        controller.Move(verticalMove * Time.deltaTime * climbSpeed);
        yield return null;
        if(inputs.GetCrouchInput()) isClimb = false;
    }
}