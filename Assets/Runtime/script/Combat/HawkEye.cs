using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HawkEye : MonoBehaviour
{   
    public Transform BulletSpawn;
    public int damage = 0;
    private List<IDamageable> enemiesInRange = new List<IDamageable>();
    public int baseDamage = 4;
    public int damageGrowth = 4; 
    public PlayerInputs inputs;
    public PlayerMovement playerMovement;
    private ObjectPooler objectPooler;
    private HawkEyeBullet hawkEyeBullet;
    private GameObject enemyObject;

    private void Start(){

        objectPooler = ObjectPooler.Instance;
    }
    

    private void OnEnable(){

        damage = 0;
        enemiesInRange.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable enemy = other.GetComponent<IDamageable>();
            if (enemy != null && !enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Add(enemy);
            }
        }
    }
    private void OnTriggerExit(Collider other){

        if (other.CompareTag("Enemy"))
        {
            IDamageable enemy = other.GetComponent<IDamageable>();
            if (enemy != null && enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Remove(enemy);
            }
        }
    }

    private void OnTriggerStay(Collider other) {

        StartCoroutine(IncreaseDamage());
        if (inputs.GetShootInput()){

            foreach (IDamageable enemy in enemiesInRange){
                HawkEyeShoot( enemy );
            }

            playerMovement.StopHawkEye();
        }
    }

    IEnumerator IncreaseDamage(){
        
        damage += damageGrowth;
        yield return new WaitForSeconds(1f);
    }

    IEnumerator HawkEyeShoot( IDamageable enemy){

        GameObject hawkBullet = objectPooler.SpawnFromPool("HawkBullet", BulletSpawn.position, BulletSpawn.transform.rotation);
        
        if (hawkBullet != null){

            print("HawkEyeShoot");
            hawkEyeBullet = hawkBullet.GetComponent<HawkEyeBullet>();
        
            if (hawkEyeBullet != null){

                print("HawkEyeBullet");
                hawkEyeBullet.damage = damage;
                hawkEyeBullet.target = enemy.GetTransform();
                hawkEyeBullet.enemyTarget = enemy;
                hawkBullet.SetActive(true);
            }
        }
        yield break;
    }
}
