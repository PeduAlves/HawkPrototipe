using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRevolver : MonoBehaviour
{
    
    public GameObject bullet;
    public int qntBullet = 6;
    public int balasNoTambor;
    public Transform bulletSpawn;
    public PlayerInputs inputs;
    ObjectPooler objectPooler;

    void Start(){

        objectPooler = ObjectPooler.Instance;
        
        balasNoTambor = qntBullet;
    }

    public void PlayerShoot(){
        
        if(balasNoTambor != 0){

            StartCoroutine(Shoot());
            balasNoTambor--;
        }
        else{ 

            Reload();
            print("recarregando");
        }
    }

    public void Reload(){

        balasNoTambor = qntBullet;
    }

    IEnumerator Shoot(){

        GameObject bullet = objectPooler.SpawnFromPool("Bullet", bulletSpawn.position, bulletSpawn.rotation);

        if (bullet != null)
        {
            // Configurar a posição e outras propriedades do projétil
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            // Ativar o objeto para começar o tiro
            bullet.SetActive(true);
        }

        yield return null; 
    }
}

