using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySapo : BaseEnemy
{
    public float launchAngle = 70f;
    public float gravity = -9.8f;
    public Rigidbody rb;
    public float sapoAttackDelay = 1f;
    public GameObject enemySigth;

    protected override IEnumerator Attack(){

        isAttacking = true;
        enemySigth.SetActive(false);


        Vector3 targetPosition = player.position;
        LaunchTowardsTarget(targetPosition);

        state = enemyStates.FOLLOW;
        playerInSight = false;
        enemySigth.SetActive(true);
        yield return new WaitForSeconds(sapoAttackDelay);
        isAttacking = false;
    }

    void LaunchTowardsTarget(Vector3 targetPosition){

        // Direção do salto (ignora a altura inicial)
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;
        float distance = direction.magnitude;

        // Converte o ângulo de lançamento para radianos
        float launchAngleRad = Mathf.Deg2Rad * launchAngle;

        // Calcula a velocidade necessária para alcançar o jogador, dado o ângulo
        float velocity = Mathf.Sqrt(distance * Mathf.Abs(gravity) / Mathf.Sin(2 * launchAngleRad));

        // Componentes da velocidade horizontal e vertical
        Vector3 velocityVector = direction.normalized * velocity * Mathf.Cos(launchAngleRad);
        velocityVector.y = velocity * Mathf.Sin(launchAngleRad);

        // Aplica a velocidade calculada ao Rigidbody
        rb.velocity = velocityVector;
    }
}
