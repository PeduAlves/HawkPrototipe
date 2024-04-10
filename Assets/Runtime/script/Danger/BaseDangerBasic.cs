using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseDangerBasic : MonoBehaviour
{
    public int ammountDamage = 10;
    public PlayerStatus playerStatus;

    private void OnTriggerEnter(Collider other){

        if( other.gameObject.tag == "Player"){

            if(playerStatus != null){

                playerStatus.PlayerTakeDamage(ammountDamage);
                playerStatus.ReturnBackPoint();
            }
        }
    }
}
