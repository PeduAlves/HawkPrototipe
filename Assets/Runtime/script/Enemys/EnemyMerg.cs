using System.Collections;
using System.Numerics;
using UnityEngine;
public class EnemyMerg : BaseEnemy
{
    public float jumpDistance = 10f;
    public float diveDistance = 10f;
    public float jumpTime = 0.5f;
    public float diveTime = 0.5f;
    public float mergAttackDelay = 0.5f;
    public GameObject enemySigth;

    //Por algum motivo não aparente, os vector3, so estão funcionando com o UnityEngine.Vector3

    protected override IEnumerator Attack(){

        isAttacking = true;
        enemySigth.SetActive(false);
        //float lookingAt = this.transform.rotation.y > 160 ? -1f : 1f;

        float elapsedTime = 0f;
        UnityEngine.Vector3 attackDirection = new UnityEngine.Vector3(0, 1f, 0);
        UnityEngine.Vector3 targetPosition = transform.position + (attackDirection * jumpDistance);
        while (elapsedTime < jumpTime){

            transform.position = UnityEngine.Vector3.Lerp(transform.position, targetPosition, (elapsedTime / jumpTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        UnityEngine.Vector3 diveDirection = player.position;
        while (transform.position.y > player.position.y + 1f){

            transform.position = UnityEngine.Vector3.Lerp(transform.position, diveDirection, (elapsedTime / diveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        state = enemyStates.FOLLOW;
        playerInSight = false;
        enemySigth.SetActive(true);
        yield return new WaitForSeconds(mergAttackDelay);
        isAttacking = false;
        
        print("EnemyMerg Attack End");
    }
}


