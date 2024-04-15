using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HawkEye : MonoBehaviour
{
    public BaseEnemy enemy;
    public 
    void OnTriggerStay(Collider other){

        if(other.CompareTag("Enemy")){

            enemy = other.GetComponent<BaseEnemy>();            
            print("Enemy Detected" + enemy.ID);
        }
    }
}
