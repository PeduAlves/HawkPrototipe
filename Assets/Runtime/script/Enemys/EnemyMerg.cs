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

        //Mover o inimigo para cima
        //float lookingAt = this.transform.rotation.y > 160 ? -1f : 1f;
        UnityEngine.Vector3 attackDirection = new UnityEngine.Vector3(0, 1f, 0);

        // Salto para cima
        UnityEngine.Vector3 targetPosition = transform.position + (attackDirection * jumpDistance);
        float elapsedTime = 0f;

        while (elapsedTime < jumpTime){

            transform.position = UnityEngine.Vector3.Lerp(transform.position, targetPosition, (elapsedTime / jumpTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //avançar para o player
        UnityEngine.Vector3 diveDirection = player.position;
        elapsedTime = 0f;

        while (transform.position.y > player.position.y + 1f){

            transform.position = UnityEngine.Vector3.Lerp(transform.position, player.position, (elapsedTime / diveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(mergAttackDelay);
        state = enemyStates.PATROL;
        isAttacking = false;
        enemySigth.SetActive(true);
        
        print("EnemyMerg Attack End");
    }
}


