using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotColliderPlataform : MonoBehaviour
{
    private bool isPlayerBellowPlataform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerBellowPlataform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerBellowPlataform = false;
        }
    }

    public bool IsPlayerOnPlatform()
    {
        return isPlayerBellowPlataform;
    }
}

