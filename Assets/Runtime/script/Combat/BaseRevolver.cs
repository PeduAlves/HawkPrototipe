using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRevolver : MonoBehaviour
{
    
    public GameObject bullet;
    public int qntBullet = 6;
    public int balasNoTambor;
    public Transform bulletSpawn;
    private ObjectPooler objectPooler;
    public float reloadTime = 1.5f;
    private bool isReloading;
    private float yAxisShoot;
    private float zAxisShoot;

    void Start(){

        objectPooler = ObjectPooler.Instance;
        balasNoTambor = qntBullet;
    }

    public virtual void PlayerShoot(){
        
        if(balasNoTambor > 0){
            
            calculateShootDirection();
            StartCoroutine(Shoot());
            balasNoTambor--;
        }
        else{ 
            
            if(!isReloading){

                print("recarregando");
                StartCoroutine(Reload());
            } 
        }
    }

    protected virtual IEnumerator Reload(){
        
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        balasNoTambor = qntBullet;
        isReloading = false;
    }

    protected virtual IEnumerator Shoot(){

        GameObject bullet = objectPooler.SpawnFromPool("Bullet", bulletSpawn.position, bulletSpawn.rotation);

        if (bullet != null){
            
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = bulletSpawn.rotation;

            Bullet bulletScript = bullet.GetComponent<Bullet>();

            if (bulletScript != null){

                bulletScript.SetForces(yAxisShoot, zAxisShoot);
            }
            bullet.SetActive(true);
        }

        yield return null; 
    }

    private void calculateShootDirection(){
        
        if(PlayerInputs.Instance.GetUpInput()) yAxisShoot = 1;
        else yAxisShoot = 0;

        if(PlayerMovement.Instance.facingRight) zAxisShoot = 1;
        else zAxisShoot = -1;

        if(PlayerInputs.Instance.GetUpInput() && (PlayerInputs.Instance.GetHorizontalInput() == 0f)){

            yAxisShoot = 1;
            zAxisShoot = 0;
        }
    }
}

