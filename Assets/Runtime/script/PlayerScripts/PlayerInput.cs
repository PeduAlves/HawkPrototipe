using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private bool jumpInput;
    private bool dashInput;
    private bool aimInput;
    private bool shootInput;

       private void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetButtonDown("Jump");
        dashInput = Input.GetButtonDown("Dash");
        aimInput = Input.GetButton("Aim");
        shootInput = Input.GetButtonDown("Fire1");
    }

    public float GetHorizontalInput(){
        return horizontalInput;
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
}
