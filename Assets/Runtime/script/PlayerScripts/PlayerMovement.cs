using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInputs inputs;
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -19.62f;
    public float jumpHeight = 5f;
    public GameObject groundChecker;
    public LayerMask groundMask;
    private Vector3 jumpVector = new(0, 0, 0);
    public float dashTime = 0.025f;
    public float dashSpeed = 1.2f;
    public bool facingRight = true;

    public static PlayerMovement Instance;
    private void Awake()=>Instance = this;
    

    public bool GroundCheck(){
        if(Physics.CheckSphere( groundChecker.transform.position, 0.3f, groundMask)){
            return true;
        }
        else{
            return false;
        }
    }

    public void PlayerGravity(){
            
            jumpVector.y += gravity * Time.deltaTime;
            controller.Move(jumpVector * Time.deltaTime);
        }

    public void Jump(){
            
        if(GroundCheck() && inputs.GetJumpInput()){
            
            jumpVector.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            controller.Move(jumpVector * Time.deltaTime);
        }
    }

    public void Walk(){
        
        float horizontalInput = inputs.GetHorizontalInput();

        if(horizontalInput > 0 ){

            transform.rotation = Quaternion.Euler(0, 0, 0);
            facingRight = true;
        }
        else if(horizontalInput < 0){

            transform.rotation = Quaternion.Euler(0, 180, 0);
            facingRight = false;
        }

        Vector3 horizontalMove = new Vector3(0, 0, horizontalInput);
        controller.Move( horizontalMove * Time.deltaTime * speed);
   }

   public IEnumerator Dash(){
    
        float atualTime = 0;
        while(atualTime <= dashTime){
            controller.Move( new Vector3(0, 0, inputs.GetHorizontalInput()* dashSpeed));
            atualTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);   
       
   }

}
