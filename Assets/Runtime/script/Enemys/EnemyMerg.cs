using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMerg : BaseEnemy
{
    protected override IEnumerator Attack(){

        isAttacking = true;

        yield return new WaitForSeconds(attackDelay);

        isAttacking = false;
    }
}
