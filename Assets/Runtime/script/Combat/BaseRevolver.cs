using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRevolver : MonoBehaviour
{
    
    public GameObject bullet;
    public int qntBullet = 6;
    public int balasNoTambor;
    public Transform bulletSpawn;
    ObjectPooler objectPooler;
    public PlayerInputs inputs;
    public PlayerMovement movement;
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
            
            // Configurar a posição e outras propriedades do projétil
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = bulletSpawn.rotation;

            // Obter o componente Bullet para configurar as forças do eixo Y e Z
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            if (bulletScript != null){

                bulletScript.SetForces(yAxisShoot, zAxisShoot);
            }
            // Ativar o objeto para começar o tiro
            bullet.SetActive(true);
        }

        yield return null; 
    }

    private void calculateShootDirection(){
        
        if(inputs.GetUpInput()) yAxisShoot = 1;
        else yAxisShoot = 0;

        if(movement.facingRight) zAxisShoot = 1;
        else zAxisShoot = -1;

        if(inputs.GetUpInput() && (inputs.GetHorizontalInput() == 0f)){

            yAxisShoot = 1;
            zAxisShoot = 0;
        }
    }
}

