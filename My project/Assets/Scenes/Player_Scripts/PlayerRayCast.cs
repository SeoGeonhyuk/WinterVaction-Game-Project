using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    public int Distance;        //인식 범위
    public GameObject target;   //레이캐스트로 인식한 게임오브젝트

    Vector3 dirVec;

    void Start()
    {
    }

    void Update()
    {
        // 플레이어 방향 체크 (레이케스트 용)

        float h = Input.GetAxisRaw("Horizontal");
        if (h == -1)
            dirVec = new Vector3(Vector3.left.x, Vector3.left.y);
        else if (h == 1)
            dirVec = new Vector3(Vector3.right.x, Vector3.right.y);


        //'문' 인식

        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y), dirVec, LayerMask.GetMask("Door"));

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(1,0,0), Distance, LayerMask.GetMask("Door"));
        //RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector3(-1,0,0), Distance, LayerMask.GetMask("Door"));

        // 레이케스트 디버그
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y), dirVec, Color.blue, 0.1f);

     

        if (hit.collider != null)
        {
            //target = hit.collider.gameObject;
            //OpenDoor();
        }
        //if (hit2.collider != null)
        {
            //target = hit2.collider.gameObject;
            //OpenDoor();
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
