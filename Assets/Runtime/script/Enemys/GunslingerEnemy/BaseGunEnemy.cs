using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGunEnemy : BaseEnemy
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float fireRate = 1f;
    public float bulletInterval = 0.5f;   
    private float nextFire = 0f;

    protected override IEnumerator Attack(){
        
        gun.transform.LookAt(player);
        if(Time.time >= nextFire){
            
            Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            nextFire = Time.time + fireRate;
        }
        yield return new WaitForSeconds(bulletInterval);
    
    }
}
