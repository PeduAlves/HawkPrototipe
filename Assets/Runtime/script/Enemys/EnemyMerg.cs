using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMerg : BaseEnemy
{
    protected override void Update(){

        switch(state){
            case enemyStates.DIE:
                break;

            case enemyStates.PATROL:
                if (playerInSight) state = enemyStates.FOLLOW;
                else StartCoroutine(Patrol());
            break;

            case enemyStates.FOLLOW:
                if(!playerInSight)state = enemyStates.FOLLOW;
                else Follow();
            break;

            case enemyStates.ATTACK:
                if(!playerInSight) state = enemyStates.FOLLOW;
                if(PlayerInAttackRange() && !isAttacking) StartCoroutine(Attack());
                else Follow();
            break;
        }
    }
    protected override void Follow()
    {
        base.Follow();
        print ("Following");
    }

    protected override IEnumerator Attack()
    {
        yield return base.Attack();
        print ("Attacking");
    }



}
