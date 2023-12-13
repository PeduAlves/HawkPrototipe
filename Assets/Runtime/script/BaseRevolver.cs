using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRevolver : MonoBehaviour
{
    
    public GameObject bullet;
    public Transform bulletSpawn;
    public PlayerInputs inputs;
    ObjectPooler objectPooler;

    void Start(){

        objectPooler = ObjectPooler.Instance;
    }

    public void PlayerShoot(){
        
        StartCoroutine(Shoot());
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

        yield return null; // Opcional, dependendo do seu caso

        // Realizar outras ações após o tiro, se necessário
    }
}

