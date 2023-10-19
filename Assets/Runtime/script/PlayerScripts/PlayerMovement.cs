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
    public float dashSpeed = 2000f;
    private bool isDash = false;

    public bool groundCheck(){
        if(Physics.CheckSphere( groundChecker.transform.position, 0.3f, groundMask)){
            return true;
        }
        else{
            return false;
        }
    }

    public void playerGravity(){
            
            jumpVector.y += gravity * Time.deltaTime;
            controller.Move(jumpVector * Time.deltaTime);
        }

    public void jump(){
            
        if(groundCheck() && inputs.GetJumpInput()){
            
            jumpVector.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            controller.Move(jumpVector * Time.deltaTime);
        }
    }

    public void walk(){

    Vector3 horizontalMove = new Vector3(0, 0, inputs.GetHorizontalInput());

    controller.Move( horizontalMove * Time.deltaTime * speed);

   }

    public void dash(){

    if(inputs.GetDashInput() && !isDash){
        
        isDash = true;
        Vector3 dashVector = new Vector3(0, 0, inputs.dashDirectionInput());
        controller.Move( dashVector * Time.deltaTime * dashSpeed);
    }
    if(groundCheck()){
        isDash = false;
    }
   }

//  public void stopToAim(){

//         if(inputs.GetAimInput()){
//             controller.Move( new Vector3(0, 0, 0));
//         }
//    }
    

}
