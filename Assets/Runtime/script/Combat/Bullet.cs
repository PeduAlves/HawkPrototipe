using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    public float bulletForce = 20f;
    public float timeForDisable = 2f;
    public int damage = 10;
    public float yAxisForce; 
    public float zAxisForce;
    public Rigidbody rb;
    private Vector3 shootDirection; 
    
    public void OnObjectSpawn(){
        
        StartCoroutine(DisableBullet());
    }

    private void FixedUpdate() {

        shootDirection = new Vector3( 0 , yAxisForce, zAxisForce );
        rb.velocity = shootDirection * bulletForce;
    }

    private void OnTriggerEnter( Collider other ){

        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null){

           GameEvents.Instance.TakeHitEvent(damage, damageable.ID);
        }

        gameObject.SetActive(false);
    }

    IEnumerator DisableBullet(){

        yield return new WaitForSeconds(timeForDisable);
        gameObject.SetActive(false);
    }
    public void SetForces(float yAxis, float zAxis){

        yAxisForce = yAxis;
        zAxisForce = zAxis;
    }
}
