using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour, IDamageablePlayer
{   
    public bool isDie = false;
    public int health = 30;
    public int maxHealth = 30;
    public GameObject LastBackPoint;
    public CharacterController controller;
    public float timeToBackPoint = 0.5f;
    public float runSpeed = 1.5f;
    public float normalSpeed = 1f;
    public float runWaitTime = 15f;
    public int killStreak = 0;
    private Vector3 backPoint;
    private Coroutine currentKillStreakCoroutine;
    public static PlayerStatus Instance;
    private void Awake()=>Instance = this;

    private void Start() {
        
        health = maxHealth;
        GameEvents.Instance.PlayerTakeDamage += PlayerTakeDamage;
        GameEvents.Instance.PlayerHeal += PlayerHeal;
        GameEvents.Instance.PlayerReturnPoint += ReturnBackPoint;
        GameEvents.Instance.PlayerAddKillStreak += AddKillStreak;
    }

    public void RestartGame(){

        if(PlayerInputs.Instance.GetShootInput() && isDie){
            print("restart");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
    }

    public void PlayerTakeDamage(int ammountDamage){

        health -= ammountDamage;
        print("player take damage" + ammountDamage + " health: " + health);
        if(health <= 0){
           Die();
        }
    }

    public void PlayerHeal(int ammountHeal){

        health += ammountHeal;
        if(health > maxHealth){
            health = maxHealth;
        }
    }
    public void ReturnBackPoint(){
        StartCoroutine(ReturnBackPointCoroutine());
    }

    IEnumerator ReturnBackPointCoroutine(){

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

        Time.timeScale = 0;
        GameEvents.Instance.PlayerTakeDamage -= PlayerTakeDamage;
        GameEvents.Instance.PlayerHeal -= PlayerHeal;
        GameEvents.Instance.PlayerReturnPoint -= ReturnBackPoint;
        GameEvents.Instance.PlayerAddKillStreak -= AddKillStreak;
        isDie = true;
    }

    public void AddKillStreak(){

        killStreak++;
        if(killStreak >= 3){

            PlayerMovement.Instance.speedScale = runSpeed;
        }
        if(currentKillStreakCoroutine != null){

            StopCoroutine(currentKillStreakCoroutine);
        }
        currentKillStreakCoroutine = StartCoroutine(killStreakCoroutine());
    }

    IEnumerator killStreakCoroutine(){

        yield return new WaitForSeconds(runWaitTime);
        killStreak = 0;
        PlayerMovement.Instance.speedScale = normalSpeed;

        currentKillStreakCoroutine = null;
    }
}
