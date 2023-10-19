using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public BaseRevolver revolver;
    public PlayerInputs inputs; 

    private void Update() {

        playerMovement.walk();
        playerMovement.jump();
        playerMovement.dash();
        
        if(inputs.GetShootInput()){
            revolver.shoot();
        }
        
    }
    private void FixedUpdate() {
        
        playerMovement.playerGravity();
    }

}
