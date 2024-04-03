using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrel : MonoBehaviour, IDamageable
{
    private static int lastAssignedID = 0;
    public int ID { get; private set; }
    public Transform ExplodePoint;
    public float ExplodeRadius = 12f;
    public int ExplodeDamage = 10;
    public LayerMask ExplodeLayerMask;

    private void Awake(){

        ID = lastAssignedID++;
    }
    
    private void Start() {
        
        GameEvents.Instance.TakeHit += TakeHit;
    }

    public void TakeHit(int ammountDamage, int id){

        if(id == ID){

            Explode();
            GameEvents.Instance.TakeHit -= TakeHit;
        }
    }

    private void Explode(){

        Destroy(gameObject);
        Collider[] colliders = Physics.OverlapSphere( ExplodePoint.position, ExplodeRadius, ExplodeLayerMask );

        foreach(Collider collider in colliders){
        
            if (collider.gameObject != gameObject){

                IDamageable damageable = collider.GetComponent<IDamageable>();

                if (damageable != null){

                    damageable.TakeHit(ExplodeDamage, damageable.ID);
                }
            }
        }
    }
}