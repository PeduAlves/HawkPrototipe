using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour, IDamageable
{

    public enum EnemyState{

        Resting,
        Patrolling,
        Spoting,
        Following,
        Attacking
    }

    public Transform player; // Referência ao jogador
    public float sightRange = 10f; // Alcance de visão do inimigo
    public float attackRange = 5f; // Alcance de ataque do inimigo
    public float patrolSpeed = 2f; // Velocidade de patrulha
    public float followSpeed = 4f; // Velocidade de seguir o jogador
    public GameObject SpotingEffect;
    public Transform[] patrolPoints; // Pontos de patrulha

    private static int lastAssignedID = 0;
    public EnemyState currentState;
    public int ID { get; private set; }

    private void Awake(){

        ID = lastAssignedID++;
    }

    public int MaxLife = 30;
    public int Life;

    private void Start() {

        Life = MaxLife;
        GameEvents.Instance.TakeHit += TakeHit;
        StartCoroutine(UpdateState());
        SpotingEffect.SetActive(false);
    }

    IEnumerator UpdateState()
    {
        while (true)
        {
            switch (currentState)
            {
                case EnemyState.Resting:
                    transform.rotation = Quaternion.Euler(-90, 0, 0);
                    if (PlayerInSight())
                        currentState = EnemyState.Spoting;
                    break;

                case EnemyState.Patrolling:
                    
                    if (PlayerInSight())
                        currentState = EnemyState.Spoting;
                    break;

                case EnemyState.Spoting:
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    
                    StartCoroutine(SpotingEffectCoroutine());
                    
                    // Implemente o comportamento de avistar aqui
                    if (PlayerInSight())
                        currentState = EnemyState.Following;
                    else
                        currentState = EnemyState.Patrolling;
                    break;

                case EnemyState.Attacking:
                    // Implemente o comportamento de atacar aqui
                    if (!PlayerInAttackRange())
                        currentState = EnemyState.Following;
                    break;

                case EnemyState.Following:
                                       
                    transform.position = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
                    Vector3 directionToPlayer = player.position - transform.position;
                    Quaternion targetRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0f, directionToPlayer.z));
                    transform.rotation = targetRotation;
    
                    if (PlayerInAttackRange())
                        currentState = EnemyState.Attacking;
                    else if (!PlayerInSight())
                        currentState = EnemyState.Patrolling;
                    break;
            }

            yield return null;
        }
    }

       bool PlayerInSight(){
        // Verifique se o jogador está dentro do alcance de visão
        return Vector3.Distance(transform.position, player.position) <= sightRange;
    }

    bool PlayerInAttackRange(){
        // Verifique se o jogador está dentro do alcance de ataque
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }

    public void TakeHit(int ammountDamage, int id){
    
        if(id == ID){

            Life -= ammountDamage;
            if(Life <= 0 ){

                EnemyDeath();
            }
        }      
    }
    private void EnemyDeath(){

        GameEvents.Instance.PlayerHealEvent(10);
        Destroy(gameObject);
        GameEvents.Instance.TakeHit -= TakeHit;
    }

    IEnumerator SpotingEffectCoroutine(){

        SpotingEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        SpotingEffect.SetActive(false);
    }

}
