using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlataform : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;

    private void Start() {
        
        transform.position = pos1.position;
    }

    private void FixedUpdate() {
        
        if(transform.position == pos1.position){
            transform.position = Vector3.MoveTowards(transform.position, pos2.position, speed * Time.deltaTime);
        }
        else if(transform.position == pos2.position){
            transform.position = Vector3.MoveTowards(transform.position, pos1.position, speed * Time.deltaTime);
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position, pos2.position, speed * Time.deltaTime);
        }
    }

}
