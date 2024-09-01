using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJaburu : BaseEnemy
{
    public float jaburuAttackDelay = 1f;
    public float attackTime = 0.75f;
    public GameObject enemySigth;
    public GameObject jaburuAttackArea;
    protected override IEnumerator Attack(){

        isAttacking = true;
        enemySigth.SetActive(false);
        
        jaburuAttackArea.SetActive(true);
        yield return new WaitForSeconds( attackTime );
        jaburuAttackArea.SetActive(false);

        playerInSight = false;
        state = enemyStates.FOLLOW;
        enemySigth.SetActive(true);
        yield return new WaitForSeconds( jaburuAttackDelay );
        isAttacking = false;
    }
}
