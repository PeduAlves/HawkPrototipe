using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    public float bulletForce = 20f;
    public float timeForDisable = 2f;
    public int damage = 1;
    public Rigidbody rb;
    private float horizontalInput = 1f;
    
    public void OnObjectSpawn(){
        
        StartCoroutine(DisableBullet());
        if(PlayerMovement.Instance.facingRight){
            
            horizontalInput = 1f;
        }
        else{
            
            horizontalInput = -1f;
        }
    }

    private void FixedUpdate() {

        rb.velocity = new Vector3( 0f, 0f, horizontalInput * bulletForce * Time.deltaTime);
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
}
