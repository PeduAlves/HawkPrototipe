using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyStates{

    PATROL, FOLLOW, ATTACK, DIE
}
public class BaseEnemy : MonoBehaviour, IDamageable
{

    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float attackRange = 5f;
    public float enemySpeed = 10f;
    public float patrolSpeed = 4f;
    public float enemyMaxLife = 30f;
    public GameObject enemySight;
    public Transform []patrolPoints;
    
    private int currentPatrolIndex = 0;
    private float enemyLife;
    private bool isDie = false;
    private enemyStates state;

    public int ID { get; private set; }
    private static int lastAssignedID = 0;
    private void Awake(){

        ID = lastAssignedID++;
    }

    private void Start(){
        
        enemyLife = enemyMaxLife;
        state = enemyStates.PATROL;
        GameEvents.Instance.TakeHit += TakeHit;
    }

    private void Update(){
            
        switch(state){
            case enemyStates.DIE:
                break;
            case enemyStates.PATROL:
                StartCoroutine(Patrol());
            break;
            case enemyStates.FOLLOW:
                Follow();
                break;
            case enemyStates.ATTACK:
                Attack();
            break;

        }
    }

    /* private protected virtual void Patrol(){
        
    } */
    private protected virtual void Follow(){

    }
    private protected virtual void Attack(){

    }
    protected virtual IEnumerator die(){

        transform.rotation = Quaternion.Euler(-90, 0, 0);
        isDie = true;
        Collider collider = GetComponent<Collider>();
        if (collider != null){

            collider.enabled = false;
        }
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    protected virtual IEnumerator Patrol(){

        if(Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.2f){
           
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        transform.LookAt(new Vector3(patrolPoints[currentPatrolIndex].position.x, transform.position.y, patrolPoints[currentPatrolIndex].position.z));
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, patrolSpeed * Time.deltaTime);
        yield return null;        
    }
    public void TakeHit(int ammountDamage, int id){
            
        if(id == ID){

            if(isDie) return;
            enemyLife -= ammountDamage;
            print("Enemy " + ID + " life: " + enemyLife);

            if( enemyLife > 0){

                //state = enemyStates.FOLLOW;
            }
            else{

                state = enemyStates.DIE;
                StartCoroutine(die());
            }
        }      
    }
}
