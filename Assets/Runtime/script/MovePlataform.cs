using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlataform : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    private bool goingToPos2 = true;

    private void Start() {
        
        transform.position = pos1.position;
    }

    private void FixedUpdate() {
        
        if( goingToPos2 ){

            transform.position = Vector3.MoveTowards(transform.position, pos2.position, speed * Time.deltaTime);
            if( transform.position == pos2.position ){
                goingToPos2 = false;
            }
        }
        else{

            transform.position = Vector3.MoveTowards(transform.position, pos1.position, speed * Time.deltaTime);
            if( transform.position == pos1.position ){
                goingToPos2 = true;
            }
        }
    }
}
