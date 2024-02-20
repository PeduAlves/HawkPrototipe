using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private float rawHorizontalInput;
    private float verticalInput;
    private bool jumpInput;
    private bool dashInput;
    private bool aimInput;
    private bool shootInput;
    private bool isCrouching;

       private void Update(){

        rawHorizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetButtonDown("Jump");
        dashInput = Input.GetButtonDown("Dash");
        aimInput = Input.GetButton("Aim");
        shootInput = Input.GetButton("Fire1");
        isCrouching = Input.GetKey(KeyCode.LeftControl);
    }

    public float GetHorizontalInput(){
        return (rawHorizontalInput > 0.1f) ? 1f : (rawHorizontalInput < -0.1f) ? -1f : 0f;
    }
    public float GetVerticalInput(){
        return verticalInput;
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
    public bool GetCrouchInput(){
        return isCrouching;
    }
}
