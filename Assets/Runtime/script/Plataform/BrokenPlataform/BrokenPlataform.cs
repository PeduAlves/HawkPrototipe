using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BrokenPlataform : MonoBehaviour
{   
    public float timeForBreak = 1f;
    public float timeForReativate = 3f;
    public GameObject brokenPlataform;
    public BoxCollider boxCollider;
    public MeshRenderer meshRenderer;
    private bool isEnable = true;

    
    private void OnTriggerEnter(Collider other) {
        
        if( other.gameObject.tag == "Player" && isEnable){
            isEnable = false;
            StartCoroutine(BreakPlataform());
        }
    }
    
    IEnumerator BreakPlataform(){

        yield return new WaitForSeconds(timeForBreak);
        StartCoroutine(ReativatePlataform());

        boxCollider.enabled = false;
        meshRenderer.enabled = false;
        this.boxCollider.enabled = false;
    }

    IEnumerator ReativatePlataform(){

        yield return new WaitForSeconds(timeForReativate);
        boxCollider.enabled = true;
        meshRenderer.enabled = true;
        this.boxCollider.enabled = true;
        isEnable = true;
    }
}
