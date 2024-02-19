using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int health = 30;
    public int maxHealth = 30;

    public void TakeDamage(int damage){
        health -= damage;
        if(health <= 0){
            Die();
        }
    }
    public void Die(){
        Debug.Log("Player died");
    }

}
