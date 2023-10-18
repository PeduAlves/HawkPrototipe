using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement playerMovement;

    private void Update() {

        playerMovement.walk();
        playerMovement.jump();
        playerMovement.dash();
    }
    private void FixedUpdate() {
        
        playerMovement.playerGravity();
    }

}
