using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private static int lastAssignedID = 0;
    public int ID { get; private set; }

    private void Awake(){
        
        ID = lastAssignedID++;
    }

    public int MaxLife = 3;
    private int Life;

    private void Start() {

        Life = MaxLife;
        GameEvents.Instance.EnemyTakeDamage += TakeDamage;
    }
    public void TakeDamage(int ammountDamage, int id){

      if(id == ID){

        Life -= ammountDamage;
        if(Life == 0 ){

            EnemyDeath();
        }
      }  
        
    }

    private void EnemyDeath(){

        Destroy(gameObject);
    }
}
