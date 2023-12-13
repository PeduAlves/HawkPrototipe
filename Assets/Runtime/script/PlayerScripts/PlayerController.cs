using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public BaseRevolver revolver;
    public PlayerInputs inputs; 

    private void Update() {

        playerMovement.Walk();
        playerMovement.Jump();
        
        if(inputs.GetShootInput()){
            revolver.Shoot();
        }
        if(inputs.GetDashInput()){
            playerMovement.StartCoroutine("Dash");
        }
        
    }
    private void FixedUpdate() {
        
        playerMovement.PlayerGravity();
    }

}
