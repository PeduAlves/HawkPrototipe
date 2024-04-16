using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableCover : MonoBehaviour, IDamageable
{   
    public int life = 0;
    public int maxlife = 30;

    public int ID { get; private set; }
    private static int lastAssignedID = 0;
    private void Awake(){

        ID = lastAssignedID++;
    }

    private void Start() {
        
        life = maxlife;
        GameEvents.Instance.TakeHit += TakeHit;
    }

    public void TakeHit(int ammountDamage, int id){
        
        if(ID == id){

            life -= ammountDamage;
            if(life <= 0){

                Destroy(gameObject);
            }
        }
    }
    public Transform GetTransform(){

       return transform;
    }
}
