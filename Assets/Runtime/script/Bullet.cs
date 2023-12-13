using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    
    public float bulletForce = 20f;
    public Rigidbody rb;
    public void OnObjectSpawn(){
        
       rb.AddForce( 0f, 0f, 1f * bulletForce * Time.deltaTime);

    }

    private void FixedUpdate() {

        rb.velocity = new Vector3( 0f, 0f, 1f * bulletForce * Time.deltaTime);

    }

}
