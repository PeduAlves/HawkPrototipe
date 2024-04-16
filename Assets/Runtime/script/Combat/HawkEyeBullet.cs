using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HawkEyeBullet : MonoBehaviour
{
    public int damage = 0;
    public Transform target;
    public float speed = 30f;
    public IDamageable enemyTarget;

    private void Update() {
        
        if (target != null){
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter( Collider other ){

        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable.ID == enemyTarget.ID){

            GameEvents.Instance.TakeHitEvent(damage, damageable.ID);
            gameObject.SetActive(false);
        }       
    }
}
