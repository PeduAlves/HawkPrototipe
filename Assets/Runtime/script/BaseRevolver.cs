using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRevolver : MonoBehaviour
{
    
    public GameObject bullet;
    public Transform bulletSpawn;
    public float bulletSpeed = 1500f;
    public PlayerInputs inputs;
    ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    public void Shoot(){

        objectPooler.SpawnFromPool("Bullet", bulletSpawn.position, bulletSpawn.rotation);
    }
}
