using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private bool jumpInputCached;
    private bool jumpInput;
    private float jumpInputCacheDuration = 0.2f;
    private float jumpInputCacheTimer;

       private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Verifique se o jogador pressionou o botão de pulo (exemplo: "Space")
        jumpInput = Input.GetButtonDown("Jump");

        // Atualize o cache de input de pulo apenas se o input atual for verdadeiro
        if (jumpInput)
        {
            jumpInputCached = true;
            jumpInputCacheTimer = jumpInputCacheDuration; // Inicialize o contador de tempo
        }

        // Reduza o contador de tempo do cache do input de pulo
        if (jumpInputCacheTimer > 0)
        {
            jumpInputCacheTimer -= Time.deltaTime;
        }
        else
        {
            jumpInputCached = false; // Limpe o cache quando o tempo expirar
        }
    }


    // Método para verificar o input de pulo do cache e redefinir o cache
    public bool ConsumeJumpInput(){
        if (jumpInputCached)
        {
            jumpInputCached = false;
            return true;
        }
        return false;
    }

    public float GetHorizontalInput()
    {
        return horizontalInput;
    }

    public float GetVerticalInput()
    {
        return verticalInput;
    }
}
