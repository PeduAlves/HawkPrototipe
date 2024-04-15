using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public BaseRevolver revolver;
    public PlayerInputs inputs; 
    public BaseHatSkill hatSkill;
    public float nextFireTime = 0.01f;
    public float fireRate = 0.5f;
    public float nextDashTime = 0.01f;
    public float dashRate = 1f;
    private bool isDash = false;
    private bool airDash = true;

    private void Update() {

        playerMovement.Aim();   
        playerMovement.Walk();
        playerMovement.Jump();
        hatSkill.HatSkill();
        
        //calcula o tempo para o próximo tiro
        if(inputs.GetShootInput() && Time.time >= nextFireTime){

            revolver.PlayerShoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

        //calcula o tempo para o próximo dash
        if(inputs.GetDashInput() && !isDash && Time.time >= nextDashTime){
            
            isDash = true;
            nextDashTime = Time.time + 1f / dashRate;
        }    
    }

    private void FixedUpdate() {
           
        playerMovement.PlayerGravity();
        playerMovement.Crouch();

        if(playerMovement.GroundCheck()) airDash = true;
        
        if(isDash && airDash){

            playerMovement.StartCoroutine(playerMovement.Dash());

            isDash = false;
            airDash = false;
        }
    }
}
