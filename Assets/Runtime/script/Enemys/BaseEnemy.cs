using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyStates{

    PATROL, FOLLOW, ATTACK, DIE
}
public class BaseEnemy : MonoBehaviour, IDamageable
{

    public LayerMask whatIsGround, whatIsPlayer;
    public float attackRange = 5f;
    public float enemySpeed = 10f;
    public float patrolSpeed = 4f;
    public float attackDelay = 2f;
    public float attackDistance = 2f;  
    public float enemyMaxLife = 30f;
    public GameObject gun;
    public Transform []patrolPoints;
    
    private Transform player;
    private int currentPatrolIndex = 0;
    private float enemyLife;
    private bool isDie = false;
    private bool isAttacking = false;
    private bool playerInSight = false;
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
        if(patrolPoints.Length == 0) state = enemyStates.FOLLOW;
        player = PlayerMovement.Instance.transform;
    }

    private void Update(){
            
        switch(state){
            case enemyStates.DIE:
                break;

            case enemyStates.PATROL:
                if (playerInSight) state = enemyStates.FOLLOW;
                else StartCoroutine(Patrol());
            break;

            case enemyStates.FOLLOW:
                if(!playerInSight)state = enemyStates.PATROL;
                else Follow();
            break;

            case enemyStates.ATTACK:
                if(!playerInSight) state = enemyStates.PATROL;
                if(PlayerInAttackRange() && !isAttacking) StartCoroutine(Attack());
                else Follow();
            break;

        }
    }
    private protected virtual void Follow(){
        if(PlayerInAttackRange()){ 
            
            state = enemyStates.ATTACK;
            return;
        }
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        transform.position = Vector3.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
    }
    protected virtual IEnumerator Attack(){
        
        isAttacking = true;
        gun.transform.localScale = new Vector3(0.1f, attackDistance, 0.2f);
        yield return new WaitForSeconds(attackDelay);
        gun.transform.localScale = new Vector3(0.1f, 0.4f, 0.2f);
        isAttacking = false;   
    }
    protected virtual IEnumerator die(){

        transform.rotation = Quaternion.Euler(-90, 0, 0);
        isDie = true;
        Collider collider = GetComponent<Collider>();
        if (collider != null){

            collider.enabled = false;
        }
        GameEvents.Instance.PlayerAddKillStreakEvent();
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

            if( enemyLife > 0){
                
                state = enemyStates.FOLLOW;
            }
            else{

                state = enemyStates.DIE;
                StartCoroutine(die());
            }
        }      
    }
    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Player")) {

            playerInSight = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        
        if (other.CompareTag("Player")) {
            
            playerInSight = false;
        }
    } 
    private bool PlayerInAttackRange(){
        
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }
    private void OnDrawGizmos() {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public Transform GetTransform()
    {
        return transform;
    }
}
