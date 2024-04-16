using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBackSpawn : MonoBehaviour
{   
    public PlayerStatus playerStatus;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){

            playerStatus.LastBackPoint = gameObject; 
        }
    }
}
