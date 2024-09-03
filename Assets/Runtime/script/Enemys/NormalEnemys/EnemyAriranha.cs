using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAriranha : BaseEnemy
{
    public float ariranhaAttackDelay = 1f;
    public float attackTime = 0.75f;
    public GameObject enemySigth;
    public GameObject ariranhaAttackArea;
    protected override IEnumerator Attack(){

        isAttacking = true;
        enemySigth.SetActive(false);

        ariranhaAttackArea.SetActive(true);
        yield return new WaitForSeconds( attackTime );
        ariranhaAttackArea.SetActive(false);

        playerInSight = false;
        state = enemyStates.FOLLOW;
        enemySigth.SetActive(true);
        yield return new WaitForSeconds( ariranhaAttackDelay );
        isAttacking = false;
    }
}
