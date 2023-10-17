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


    // private void Update() {
    //     walk();
    //     playerGravity();
    // }

    public bool groundCheck(){
        if(Physics.CheckSphere( groundChecker.transform.position, 0.3f, groundMask)){
            return true;
        }
        else{
            return false;
        }
    }

    public void playerGravity(){

        
    }

    public void jump(){
        if( inputs.jumpInput && groundCheck()){

            Vector3 jumpVector = new Vector3(0, jumpHeight, 0);

            jumpVector.y += Mathf.Sqrt(jumpHeight * -3f * gravity);

            jumpVector.y += gravity * Time.deltaTime;
            controller.Move(jumpVector * Time.deltaTime);

            
            print("jump");
            inputs.jumpInput = false;
            print("jump false");
        }
    }

    public void walk(){

    Vector3 horizontalMove = new Vector3(0, 0, inputs.GetHorizontalInput());

    controller.Move( horizontalMove * Time.deltaTime * speed);

   }
}
