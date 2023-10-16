using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float speed = 12f;
    
    private CharacterController controller;
    private float z;
    private float y;
    private Vector3 velocity;


    public void Start() {
     
        controller = GetComponent<CharacterController>();
    }

    public void Update(){
        
        ApplyGravity();
        PlayerJump();
        GetInputs();
        MovePlayer();
    }

    #region MyMethods

        //Apply gravity on the player, increase velocity.y by gravity * Time.deltaTime²
        private void ApplyGravity(){

            if (!controller.isGrounded){

                velocity.y += gravity * Time.deltaTime;
            }
            else{

                velocity.y = -0.5f;
            }

            controller.Move(velocity * Time.deltaTime);
        }

        //Get the inputs from the player, Horizontal( for de movement) and Vertical(for the Aim)
        public void GetInputs(){

            z = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
        }

        //Move the player on the z axis, multiply by speed and Time.deltaTime
        public void MovePlayer(){

            controller.Move(new Vector3(0f, 0f, z) * speed * Time.deltaTime);
        }
    
        //Jump the player, if the player is grounded and press the jump button
        public void PlayerJump(){
    
        if (Input.GetButtonDown("Jump") && controller.isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    #endregion
}
