using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    
    public float bulletForce = 20f;
    public Rigidbody rb;
    public void OnObjectSpawn(){

       StartCoroutine(DisableBullet());
    }

    private void FixedUpdate() {

        rb.velocity = new Vector3( 0f, 0f, 1f * bulletForce * Time.deltaTime);
    }

    private void OnCollisionEnter( Collision other ){}

    IEnumerator DisableBullet(){

        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
