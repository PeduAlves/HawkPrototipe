using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRevolver : MonoBehaviour
{
    
    public GameObject bullet;
    public Transform bulletSpawn;
    public float bulletSpeed = 1500f;
    public PlayerInputs inputs;

    public void shoot(){

        GameObject bulletInstance = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        bulletInstance.GetComponent<Rigidbody>().AddForce(0f, 0f, 1f * bulletSpeed);  
    }
}
