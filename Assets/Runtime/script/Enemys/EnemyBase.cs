using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum enemyState{

    REST, PATROL, SPOT, FOLLOW, FURY
}

public class EnemyBase : MonoBehaviour, IDamageable{
//     private bool isAlert;
//     public bool isAttack = false;
//     private int idWayPoint;
    private bool isDie;
    public Transform player; // Referência ao jogador
    public float sightRange = 10f; // Alcance de visão do inimigo
    public float attackRange = 5f; // Alcance de ataque do inimigo
    public float patrolSpeed = 2f; // Velocidade de patrulha
    public float followSpeed = 4f; // Velocidade de seguir o jogador
    public enemyState state;
    public GameObject SpotingEffect;
    public int MaxLife = 30;
    public int Life;

    private static int lastAssignedID = 0;
    public int ID { get; private set; }
    private void Awake(){

        ID = lastAssignedID++;
    }
    private void Start() {

        Life = MaxLife;
        GameEvents.Instance.TakeHit += TakeHit;
        ChangeState(state);
        SpotingEffect.SetActive(false);
    }

    void Update() {
        
        StateManager();
    }

    #region MeusMetodos

        public void TakeHit(int ammountDamage, int id){
            
            if(id == ID){

                if(isDie) return;

                Life -= ammountDamage;

                if( Life > 0){

                    ChangeState( enemyState.FURY );
                }
                else{

                    StartCoroutine("Died");
                }
            }      
        }
        private void EnemyDeath(){

            GameEvents.Instance.PlayerHealEvent(10);
            Destroy(gameObject);    
        }
        bool PlayerInSight(){
            
            // Verifique se o jogador está dentro do alcance de visão
            return Vector3.Distance(transform.position, player.position) <= sightRange;
        }
        bool PlayerInAttackRange(){
            
            // Verifique se o jogador está dentro do alcance de ataque
            return Vector3.Distance(transform.position, player.position) <= attackRange;
        }
        void StateManager(){

            switch( state ){
                
                case enemyState.SPOT:

                    StartCoroutine("SPOT");
                    ChangeState( enemyState.FOLLOW );  
                break;

                case enemyState.FOLLOW: 
                    
                                        
                break;
                
                case enemyState.FURY:
                
                    
                break;
            }
        }

        void ChangeState( enemyState newState ){

            //Para todas as corotinas
            StopAllCoroutines();
            state = newState;


            switch( state ){
                
                case enemyState.SPOT:

                    StartCoroutine("SPOT");
                break;
                case enemyState.FOLLOW:                  

                    StartCoroutine("FOLLOW");
                break;
                case enemyState.FURY:

                    StartCoroutine("FURY");
                break;
                case enemyState.REST:

                    StartCoroutine("REST");
                break;
                case enemyState.PATROL:

                    StartCoroutine("PATROL");
                break;
            }
        }

        IEnumerator REST(){
            transform.rotation = Quaternion.Euler(-90, 0, 0);
            if (PlayerInSight()) ChangeState( enemyState.SPOT);

            yield return new WaitForSeconds(1);
        }
        IEnumerator SPOT(){
            transform.rotation = Quaternion.Euler(0, 0, 0);
                    
            StartCoroutine(SpotingEffectCoroutine());  
                    
            if (PlayerInSight())
                ChangeState(enemyState.FOLLOW);
            else
                ChangeState(enemyState.PATROL);
            yield return new WaitForSeconds(1f);
        }
        IEnumerator FOLLOW(){
                                                   
            transform.position = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
            Vector3 directionToPlayer = player.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0f, directionToPlayer.z));
            transform.rotation = targetRotation;
    
            if (PlayerInAttackRange())ChangeState(enemyState.FURY);
            else if (!PlayerInSight())ChangeState(enemyState.PATROL);

            yield return new WaitForSeconds(1);
        }
        IEnumerator FURY(){

            if (!PlayerInAttackRange()) ChangeState(enemyState.FOLLOW);
            yield return new WaitForSeconds(10);
        }
        IEnumerator PATROL( ){
            
            if (PlayerInSight()) ChangeState( enemyState.SPOT);
            yield return new WaitForSeconds(1);
        }
        IEnumerator Died(){

            isDie = true;
            yield return new WaitForSeconds(3f);
            Destroy(this.gameObject);
            GameEvents.Instance.TakeHit -= TakeHit;
        }
        IEnumerator SpotingEffectCoroutine(){

            SpotingEffect.SetActive(true);
            yield return new WaitForSeconds(1f);
            SpotingEffect.SetActive(false);
        }
    #endregion
}