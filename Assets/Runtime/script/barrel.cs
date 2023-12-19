using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrel : MonoBehaviour, IDamageable
{
    private static int lastAssignedID = 0;
    public int ID { get; private set; }

    private void Awake(){

        ID = lastAssignedID++;
    }
    private void Start() {
        
        GameEvents.Instance.TakeHit += TakeHit;
    }


    public void TakeHit(int ammountDamage, int id){

        if(id == ID){

            print("BUMMMMMM");
            Destroy(gameObject);
            GameEvents.Instance.TakeHit -= TakeHit;
        }
    }
}
