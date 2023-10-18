using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private bool jumpInput;
    private bool dashInput;

       private void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetButtonDown("Jump");
        dashInput = Input.GetButtonDown("Dash");

    }

    public float GetHorizontalInput()
    {
        return horizontalInput;
    }

    public float GetVerticalInput()
    {
        return verticalInput;
    }
    public bool GetJumpInput()
    {
        return jumpInput;
    }
    public bool GetDashInput()
    {
        return dashInput;
    }

    public float dashDirectionInput(){

        if( GetHorizontalInput() >= 0 ){
            return 1;
        }
        else{
            return -1;
        }
    }
}
