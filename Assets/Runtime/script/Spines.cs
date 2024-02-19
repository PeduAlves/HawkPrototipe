using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spines : MonoBehaviour
{
    public int damage = 10;
    private void OnTriggerEnter( Collider other ){

        IDamageablePlayer damageable = other.GetComponent<IDamageablePlayer>();

        if (damageable != null){

            damageable.PlayerTakeDamage(damage);
        }
    }
}
