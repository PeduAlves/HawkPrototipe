using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public BaseRevolver revolver;
    public PlayerInputs inputs; 
    public float nextFireTime = 0.5f;
    public float fireRate = 0.5f;

    private void Update() {

        playerMovement.Walk();
        playerMovement.Jump();
        
        if(inputs.GetShootInput() && Time.time >= nextFireTime){

            revolver.PlayerShoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

        if(inputs.GetDashInput()){
            
            playerMovement.StartCoroutine("Dash");
        }
        
    }
    private void FixedUpdate() {
        
        playerMovement.PlayerGravity();
    }

}
