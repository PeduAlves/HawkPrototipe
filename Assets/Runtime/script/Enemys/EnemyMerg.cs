using System.Collections;
using UnityEngine;
public class EnemyMerg : BaseEnemy
{
    public float jumpDistance = 10f;
    public float diveDistance = 10f;
    public float jumpTime = 0.5f;
    public float diveTime = 0.5f;
    public float mergAttackDelay = 0.5f;
    public GameObject enemySigth;

    protected override IEnumerator Attack(){

        isAttacking = true;
        enemySigth.SetActive(false);

        float elapsedTime = 0f;
        Vector3 attackDirection = new Vector3(0, 1f, 0);
        Vector3 targetPosition = transform.position + (attackDirection * jumpDistance);
        while (elapsedTime < jumpTime){

            transform.position = Vector3.Lerp(transform.position, targetPosition, (elapsedTime / jumpTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        Vector3 diveDirection = player.position;
        while (transform.position.y > player.position.y + 1f){

            transform.position = Vector3.Lerp(transform.position, diveDirection, (elapsedTime / diveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        state = enemyStates.FOLLOW;
        playerInSight = false;
        enemySigth.SetActive(true);
        yield return new WaitForSeconds(mergAttackDelay);
        isAttacking = false;
    }
}




