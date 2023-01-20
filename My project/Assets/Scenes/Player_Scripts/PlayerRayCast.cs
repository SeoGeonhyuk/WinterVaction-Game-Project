using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    public int Distance;        //인식 범위
    public GameObject target;   //레이캐스트로 인식한 게임오브젝트

    void Start()
    {
    }

    void Update()
    {   
        //'문' 인식
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(1,0,0), Distance, LayerMask.GetMask("Door"));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector3(-1,0,0), Distance, LayerMask.GetMask("Door"));

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            OpenDoor();
        }
        if (hit2.collider != null)
        {
            target = hit2.collider.gameObject;
            OpenDoor();
        }
    }
    
    //문 열기
    void OpenDoor()
    {
        if (Input.GetKeyDown("e") == true)
            {
                ChangeDoorSprite call = target.GetComponent<ChangeDoorSprite>();
                call.openDoor = true;
            }
    }
}
