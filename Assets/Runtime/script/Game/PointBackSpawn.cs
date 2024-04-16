using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBackSpawn : MonoBehaviour
{   
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){

            PlayerStatus.Instance.LastBackPoint = gameObject; 
        }
    }
}
