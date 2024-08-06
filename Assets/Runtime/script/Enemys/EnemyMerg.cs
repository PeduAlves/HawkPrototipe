using System.Collections;
using System.Numerics;
using UnityEngine;
public class EnemyMerg : BaseEnemy
{
    public float flySpeed = 10f;
    public float jumpDistance = 10f;
    public float diveSpeed = 10f;
    public float jumpTime = 0.5f;
    public float mergAttackDelay = 0.5f;


    protected override IEnumerator Attack(){

        isAttacking = true;
        print("EnemyMerg Attack");
        //Mover o inimigo para cima
        transform.LookAt(player.position);
        float lookingAt = this.transform.rotation.y > 160 ? -1f : 1f;
        
        Vector3 attackDirection = new Vector3(0, 1f, 0);

        // Salto para cima
        Vector3 targetPosition = transform.position + attackDirection * jumpDistance;
        float elapsedTime = 0f;

        while (elapsedTime < jumpTime)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, (elapsedTime / jumpTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //avançar para o player

        vector3 diveDirection = (player.position - transform.position).normalized;

        while (Vector3.Distance(transform.position, player.position) > 1f)
        {
            transform.LookAt(player.position);
            transform.position += diveDirection * diveSpeed * Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(mergAttackDelay);
        state = enemyStates.FOLLOW;
        transform.LookAt(player.position);
        yield return new WaitForSeconds(mergAttackDelay);
        isAttacking = false;
        
        print("EnemyMerg Attack End");
    }
}


