using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceTrace : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player" && transform.parent.GetComponent<PoliceMovementRefactored>().policeType == PoliceMovementRefactored.policeState.Tracing){
            Vector3 playerPos = collision.transform.position;
            if(playerPos.x > transform.position.x){
                transform.parent.GetComponent<PoliceMovementRefactored>().nextMove = 1;
            }
            else if(playerPos.x <= transform.position.x){
                transform.parent.GetComponent<PoliceMovementRefactored>().nextMove = -1;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        /*if(collision.gameObject.tag == "Player" && transform.parent.GetComponent<PoliceMovementRefactored>().policeType == PoliceMovementRefactored.policeState.Tracing && transform.parent.GetComponent<PoliceMovementRefactored>().traceGage >= 0){
                transform.parent.GetComponent<PoliceMovementRefactored>().traceGage -=
                    transform.parent.GetComponent<PoliceMovementRefactored>().traceGagePlus;
        }*/
    }
}
