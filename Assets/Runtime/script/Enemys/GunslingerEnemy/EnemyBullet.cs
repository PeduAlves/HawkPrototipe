using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletForce = 20f;
    public float timeForDisable = 2f;
    public int damage = 10;
    public float yAxisForce = 0; 
    public float zAxisForce = -1;
    public Rigidbody rb;
    protected Vector3 shootDirection; 

    protected virtual void OnEnable() {

        StartCoroutine(DisableBullet());
        shootDirection = new Vector3(0, PlayerController.Instance.transform.position.y, PlayerController.Instance.transform.position.z) - transform.position;
    }

    private void FixedUpdate() {

        rb.velocity = shootDirection.normalized * bulletForce;
    }

    private void OnTriggerEnter( Collider other ){

        IDamageablePlayer damageable = other.GetComponent<IDamageablePlayer>();

        if (damageable != null){

           GameEvents.Instance.PlayerTakeDamageEvent(damage);
        }

        gameObject.SetActive(false);
    }

    public IEnumerator DisableBullet(){

        yield return new WaitForSeconds(timeForDisable);
        Destroy(this.gameObject);
    }
}
