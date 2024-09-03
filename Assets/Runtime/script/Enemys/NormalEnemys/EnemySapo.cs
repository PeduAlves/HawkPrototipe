using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemySapo : BaseEnemy
{
    public float launchAngle = 70f;
    public float gravity = -9.8f;
    public Rigidbody rb;
    public float sapoAttackDelay = 0.5f;
    public GameObject enemySigth;
    private bool isGrounded = true;
    public float groundCheckerHeight = 0.5f;

    protected override void Update() {
        
        groundChecker();
        base.Update();
    }

    protected override IEnumerator Attack(){
        
        if(!isGrounded) yield break;
    
        isAttacking = true;
        enemySigth.SetActive(false);

        Vector3 targetPosition = player.position;            
        Vector3 direction = targetPosition - transform.position;            
        float distance = direction.magnitude;
        float launchAngleRad = Mathf.Deg2Rad * launchAngle;
        float velocity = Mathf.Sqrt(distance * Mathf.Abs(gravity) / Mathf.Sin(2 * launchAngleRad));  
        Vector3 velocityVector = direction.normalized * velocity * Mathf.Cos(launchAngleRad);
        velocityVector.y = velocity * Mathf.Sin(launchAngleRad);
        rb.velocity = velocityVector;
        
        //playerInSight = false;
        state = enemyStates.FOLLOW;
        playerInSight = true;
        yield return new WaitForSeconds(sapoAttackDelay);
        isAttacking = false;
        playerInSight = false;
        enemySigth.SetActive(true);
    }


    protected void groundChecker(){

        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckerHeight)){

            isGrounded = true;
        }
        else isGrounded = false;
    }
}
