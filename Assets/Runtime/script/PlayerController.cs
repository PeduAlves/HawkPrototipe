using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement playerMovement;

    private void Update() {

        playerMovement.walk();
        playerMovement.jump();
    }
    private void FixedUpdate() {
        
        playerMovement.playerGravity();
    }

}
