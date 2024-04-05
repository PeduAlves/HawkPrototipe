using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, IDamageablePlayer
{
    public int health = 30;
    public int maxHealth = 30;

    private void Start() {
        
        health = maxHealth;
        GameEvents.Instance.PlayerTakeDamage += PlayerTakeDamage;
        GameEvents.Instance.PlayerHeal += PlayerHeal;
    }

    public void PlayerTakeDamage(int ammountDamage){

        health -= ammountDamage;
        if(health <= 0){
           Die();
           Time.timeScale = 0;
        }
    }

    public void PlayerHeal(int ammountHeal){

        health += ammountHeal;
        if(health > maxHealth){
            health = maxHealth;
        }
    }

    public void Die(){
        
        GameEvents.Instance.PlayerTakeDamage -= PlayerTakeDamage;
        GameEvents.Instance.PlayerHeal -= PlayerHeal;
    }

}
