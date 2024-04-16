using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseDangerBasic : MonoBehaviour
{
    public int ammountDamage = 10;

    private void OnTriggerEnter(Collider other){

        if( other.gameObject.tag == "Player"){

            GameEvents.Instance.PlayerTakeDamageEvent(ammountDamage);
            GameEvents.Instance.PlayerReturnPointEvent();
            
        }
    }
}
