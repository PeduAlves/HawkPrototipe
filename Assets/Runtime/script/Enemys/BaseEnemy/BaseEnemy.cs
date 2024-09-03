using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyStates{

    PATROL, FOLLOW, ATTACK, DIE
}
public class BaseEnemy : MonoBehaviour, IDamageable
{

    public float attackRange = 5f;
    public float enemySpeed = 10f;
    public float patrolSpeed = 4f;
    public float attackDelay = 2f;
    public float attackDistance = 2f;  
    public float enemyMaxLife = 30f;
    public GameObject gun;
    public Transform []patrolPoints;
    
    protected Transform player;
    Vector3 playerPosition;
    protected int currentPatrolIndex = 0;
    protected float enemyLife;
    protected bool isDie = false;
    protected bool isAttacking = false;
    protected bool playerInSight = false;
    public int damage = 10;
    public enemyStates state;

    public int ID { get; protected set; }
    public static int lastAssignedID = 0;
    protected void Awake(){

        ID = lastAssignedID++;
    }

    protected virtual void Start(){
        
        enemyLife = enemyMaxLife;
        state = enemyStates.PATROL;
        GameEvents.Instance.TakeHit += TakeHit;
        if(patrolPoints.Length == 0) state = enemyStates.FOLLOW;
        player = PlayerMovement.Instance.transform;
    }

    protected virtual void Update(){
        
        playerPosition = new Vector3( player.position.x , transform.position.y, player.position.z);

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
                if(PlayerInAttackRange() && !isAttacking) {

                    transform.LookAt(playerPosition);
                    StartCoroutine(Attack());
                }
                else Follow();
            break;

        }
    }
    protected virtual void Follow(){
        
        if(PlayerInAttackRange() && !isAttacking){ 
            
            state = enemyStates.ATTACK;
            return;
        }
        if(isAttacking)return;
        transform.LookAt(playerPosition);
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, enemySpeed * Time.deltaTime);
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
        Rigidbody rb = GetComponent<Rigidbody>();
        if (collider != null){

            collider.enabled = false;
        }
        if (rb != null){

            rb.useGravity = true;
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
    protected void OnCollisionEnter(Collision other) {

        if (other.gameObject.tag == "Player") {

            GameEvents.Instance.PlayerTakeDamageEvent( damage );
        }
    
    }
    protected void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Player")) {

            playerInSight = true;
        }
    }
    protected void OnTriggerExit(Collider other) {
        
        if (other.CompareTag("Player")) {
            
            playerInSight = false;
        }
    } 
    protected bool PlayerInAttackRange(){
        
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }
    protected void OnDrawGizmos() {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public Transform GetTransform()
    {
        return transform;
    }
}
