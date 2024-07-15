using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMerg : BaseEnemy
{
    
    private bool isAttacking = false;
    private Transform playerTransform= PlayerController.Instance.transform;
    

    protected override IEnumerator Attack(){
        isAttacking = true;
        // Implementação específica para o EnemyMerg
        Debug.Log("EnemyMerg is attacking");
        yield return new WaitForSeconds(attackDelay);
        // Código adicional para o ataque, se necessário
        isAttacking = false;   
    }

    private protected override void Follow()
    {
        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);
    }
}
