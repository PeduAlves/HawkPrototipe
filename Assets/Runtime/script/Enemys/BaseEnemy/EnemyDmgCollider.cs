using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDmgCollider : MonoBehaviour
{
    public int damage = 10;
    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Player") {

            GameEvents.Instance.PlayerTakeDamageEvent( damage );
        }
    }
}
