using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopColliderPlataform : MonoBehaviour
{
    private bool isPlayerOnPlatform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
        }
    }

    public bool IsPlayerOnPlatform()
    {
        return isPlayerOnPlatform;
    }
}
