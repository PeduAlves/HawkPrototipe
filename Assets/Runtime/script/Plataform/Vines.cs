using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vines : MonoBehaviour
{
    public PlayerInputs inputs;
    public PlayerMovement playerMovement;

    private void OnTriggerStay(Collider other) {
    
        if( other.gameObject.tag == "Player" ){

            if(inputs.GetUpInput()){

                playerMovement.Climb();
            }
            if(inputs.GetCrouchInput()){

                playerMovement.noClimb();
            }
        }
   }
   private void OnTriggerExit(Collider other){

       if( other.gameObject.tag == "Player" ){
           
           playerMovement.noClimb();
       }
   }
}
