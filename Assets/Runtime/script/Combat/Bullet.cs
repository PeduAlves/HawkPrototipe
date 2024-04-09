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
    public PlayerInputs PlayerInputs;
    
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

            damageable.TakeHit(damage, damageable.ID);
        }

        gameObject.SetActive(false);
    }

    IEnumerator DisableBullet(){

        yield return new WaitForSeconds(timeForDisable);
        gameObject.SetActive(false);
    }
    public void SetForces(float yAxis, float zAxis){

        // Configurar as forças do eixo Y e Z para serem usadas na direção da bala
        yAxisForce = yAxis;
        zAxisForce = zAxis;
    }
}
