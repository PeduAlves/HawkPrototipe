using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCagado : BaseEnemy
{   
    public float dashTime = 1f;
    public float chargeTime = 1.5f;
    public GameObject enemySigth;

    protected override IEnumerator Attack(){
        
        isAttacking = true;
        enemySigth.SetActive(false);

        Vector3 attackDirection = new Vector3(player.position.x, transform.position.y, player.position.z);
        float elapsedTime = 0f;
        yield return new WaitForSeconds( chargeTime );
        while (elapsedTime < dashTime){

            transform.position = Vector3.Lerp(transform.position, attackDirection, (elapsedTime / dashTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isAttacking = false;
        state = enemyStates.FOLLOW;
        playerInSight = false;
        enemySigth.SetActive(true);
    }
}
