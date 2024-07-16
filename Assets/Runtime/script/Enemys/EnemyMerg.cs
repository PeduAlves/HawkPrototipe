using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMerg : BaseEnemy
{

    private bool mergIsAttacking = false;
    private Transform playerTransform;
  
    private protected override void Start()
    {
        base.Start();
        playerTransform = PlayerController.Instance.transform;
    }
    protected override IEnumerator Attack(){
        mergIsAttacking = true;
        // Implementação específica para o EnemyMerg
        Debug.Log("EnemyMerg is attacking");
        yield return new WaitForSeconds(attackDelay);
        // Código adicional para o ataque, se necessário
        mergIsAttacking = false;   
    }

    

}
