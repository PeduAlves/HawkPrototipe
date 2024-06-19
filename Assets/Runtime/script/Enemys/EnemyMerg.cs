using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMerg : BaseEnemy
{
    
    private bool isAttacking = false;
    

    protected override IEnumerator Attack(){
        isAttacking = true;
        // Implementação específica para o EnemyMerg
        Debug.Log("EnemyMerg is attacking");
        yield return new WaitForSeconds(attackDelay);
        // Código adicional para o ataque, se necessário
        isAttacking = false;   
    }
}
