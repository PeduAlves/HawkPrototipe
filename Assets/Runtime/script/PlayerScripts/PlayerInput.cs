using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private float rawHorizontalInput;
    private bool UpInput;
    private bool jumpInput;
    private bool dashInput;
    private bool aimInput;
    private bool shootInput;
    private bool isCrouching;
    private bool hatSkillIput;
    private bool hawkEyeInput;
    
    //recebe os inputs do jogador
       private void Update(){

        rawHorizontalInput = Input.GetAxis("Horizontal");
        UpInput = Input.GetButton("Up");
        jumpInput = Input.GetButtonDown("Jump");
        dashInput = Input.GetButtonDown("Dash");
        aimInput = Input.GetButton("Aim");
        shootInput = Input.GetButton("Fire1");
        hatSkillIput = Input.GetButton("Fire2");
        isCrouching = Input.GetButton("Crouch");
        hawkEyeInput = Input.GetButton("HawkEye");
    }

    //Funcoes de retorno dos inputs
    public float GetHorizontalInput(){

        //função para retornar um valor entre -1 e 1
        return (rawHorizontalInput > 0.1f) ? 1f : (rawHorizontalInput < -0.1f) ? -1f : 0f;
    }
    public bool GetUpInput(){
        return UpInput;
    }
    public bool GetJumpInput(){
        return jumpInput;
    }
    public bool GetDashInput(){
        return dashInput;
    }
    public bool GetAimInput(){
        return aimInput;
    }
    public bool GetShootInput(){
        return shootInput;
    }
    public bool GetHatSkillInput(){
        return hatSkillIput;
    }
    public bool GetCrouchInput(){
        return isCrouching;
    }
    public bool GetHawkEyeInput(){
        return hawkEyeInput;
    }
}
