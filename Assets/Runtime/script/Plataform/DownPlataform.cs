using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DownPlataform : MonoBehaviour
{
    public TopColliderPlataform checkPlayerTop;
    public BotColliderPlataform checkPlayerBot;
    public BoxCollider boxCollider;
    private int layer;

    private void Start()
    {
        layer = gameObject.layer;
    }
    private void Update()
    {
        if (checkPlayerTop.IsPlayerOnPlatform() && Input.GetKeyDown(KeyCode.S)){

            boxCollider.isTrigger = true; 
            gameObject.layer = 18;   
            StartCoroutine(wait());            
        }
        if (checkPlayerBot.IsPlayerOnPlatform()){

            boxCollider.isTrigger = true; 
            gameObject.layer = 18;               
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")){

            boxCollider.isTrigger = false;
            gameObject.layer = layer;
        }
    }
    IEnumerator wait(){
        checkPlayerBot.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        checkPlayerBot.gameObject.SetActive(true);
    }
}
