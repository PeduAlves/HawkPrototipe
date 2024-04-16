using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, IDamageablePlayer
{
    public int health = 30;
    public int maxHealth = 30;
    public GameObject LastBackPoint;
    public CharacterController controller;
    public float timeToBackPoint = 0.5f;
    private Vector3 backPoint;

    public static PlayerStatus Instance;
    private void Awake()=>Instance = this;

    private void Start() {
        
        health = maxHealth;
        GameEvents.Instance.PlayerTakeDamage += PlayerTakeDamage;
        GameEvents.Instance.PlayerHeal += PlayerHeal;
        GameEvents.Instance.PlayerReturnPoint += ReturnBackPoint;
    }

    public void PlayerTakeDamage(int ammountDamage){

        health -= ammountDamage;
        print("player take damage" + ammountDamage + " health: " + health);
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
    public void ReturnBackPoint(){
        StartCoroutine(ReturnBackPointCourotine());
    }

    IEnumerator ReturnBackPointCourotine(){

        if(LastBackPoint == null){
            backPoint = new Vector3(0,0,0);
        }else{

            backPoint = LastBackPoint.transform.position;
        }
        yield return new WaitForSeconds(timeToBackPoint);
        controller.enabled = false;
        transform.position = backPoint;
        controller.enabled = true;
    }

    public void Die(){
        
        GameEvents.Instance.PlayerTakeDamage -= PlayerTakeDamage;
        GameEvents.Instance.PlayerHeal -= PlayerHeal;
    }

}
