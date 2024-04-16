using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vines : MonoBehaviour
{

    private void OnTriggerStay(Collider other) {
    
        if( other.gameObject.tag == "Player" ){

            if(PlayerInputs.Instance.GetUpInput()){

                PlayerMovement.Instance.Climb();
            }
            if(PlayerInputs.Instance.GetCrouchInput()){

                PlayerMovement.Instance.noClimb();
            }
        }
   }
   private void OnTriggerExit(Collider other){

       if( other.gameObject.tag == "Player" ){
           
           PlayerMovement.Instance.noClimb();
       }
   }
}
