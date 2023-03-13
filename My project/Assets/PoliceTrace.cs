using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceTrace : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player" && transform.parent.GetComponent<PoliceMove>().traceMode){
            Vector3 playerPos = collision.transform.position;
            if(playerPos.x > transform.position.x){
                transform.parent.GetComponent<PoliceMove>().nextMove = 1;
                transform.parent.GetComponent<PoliceMove>().saveDir = 1;
            }
            else{
                transform.parent.GetComponent<PoliceMove>().nextMove = -1;
                transform.parent.GetComponent<PoliceMove>().saveDir = -1;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player" && transform.parent.GetComponent<PoliceMove>().traceMode){
            transform.parent.GetComponent<PoliceMove>().nextMove = 0;
        }
    }
}
