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
        Searching,
        Following,
        Attacking
    }

    public Transform player; // Referência ao jogador
    public float sightRange = 10f; // Alcance de visão do inimigo
    public float attackRange = 2f; // Alcance de ataque do inimigo
    public float patrolSpeed = 2f; // Velocidade de patrulha
    public float followSpeed = 4f; // Velocidade de seguir o jogador

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
    }

    IEnumerator UpdateState()
    {
        while (true)
        {
            switch (currentState)
            {
                case EnemyState.Resting:
                    transform.rotation = Quaternion.Euler(090, 090, 090);
                    if (PlayerInSight())
                        currentState = EnemyState.Spoting;
                    break;

                case EnemyState.Patrolling:
                    // Implemente o comportamento de patrulhar aqui
                    if (PlayerInSight())
                        currentState = EnemyState.Spoting;
                    break;

                case EnemyState.Spoting:
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    
                    // Implemente o comportamento de avistar aqui
                    if (PlayerInSight())
                        currentState = EnemyState.Following;
                    else
                        currentState = EnemyState.Searching;
                    break;

                case EnemyState.Searching:
                    // Implemente o comportamento de Searching aqui
                    if (PlayerInSight())
                        currentState = EnemyState.Spoting;
                    else
                        currentState = EnemyState.Patrolling;
                    break;

                case EnemyState.Attacking:
                    // Implemente o comportamento de atacar aqui
                    if (!PlayerInAttackRange())
                        currentState = EnemyState.Following;
                    break;

                case EnemyState.Following:
                    // Implemente o comportamento de seguir aqui
                    if (PlayerInAttackRange())
                        currentState = EnemyState.Attacking;
                    else if (!PlayerInSight())
                        currentState = EnemyState.Searching;
                    break;
            }

            yield return null;
        }
    }

       bool PlayerInSight()
    {
        // Verifique se o jogador está dentro do alcance de visão
        return Vector3.Distance(transform.position, player.position) <= sightRange;
    }

    bool PlayerInAttackRange()
    {
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
}
