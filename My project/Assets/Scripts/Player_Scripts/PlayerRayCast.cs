using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    public int Distance;        //인식 범위
    public GameObject target;   //레이캐스트로 인식한 게임오브젝트'
    private float time;
    private float keyDownTime;
    private bool canTransferBox = false;
    private bool onPushMode = false;
    private bool canOffPushMode = false;
    private float dirValue;
    private Player playerComponent;

    Vector3 dirVec;

    void Start()
    {
        playerComponent = GetComponent<Player>();
        //originalJumpPower = playerComponent.jumpPower;
        //Debug.Log(originalJumpPower);
    }

    void Update()
    {
        // 플레이어 방향 체크 (레이케스트 용)
        
        if (onPushMode == false)
        {
            dirValue = Input.GetAxisRaw("Horizontal");
            if (dirValue == -1)
                dirVec = new Vector3(Vector3.left.x, Vector3.left.y);
            else if (dirValue == 1)
                dirVec = new Vector3(Vector3.right.x, Vector3.right.y);
        }


        //'문' 인식

        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y), dirVec, Distance, LayerMask.GetMask("Object"));

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(1,0,0), Distance, LayerMask.GetMask("Door"));
        //RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector3(-1,0,0), Distance, LayerMask.GetMask("Door"));

        // 레이케스트 디버그
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y), dirVec, Color.blue, 0.1f);

     

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            if (target.tag == "Door")
            if (target.tag == "Door" && onPushMode == false)
            {
                // 문 열기 함수
                OpenDoor();
            }

            if (target.tag == "Box")
            {
                // 박스 변형 함수
                TransferBox();
            }
        }
        /*if (hit2.collider != null)
        {
            target = hit2.collider.gameObject;
            OpenDoor();
        }*/
    }
    
    //문 열기
    // 문 열기
    void OpenDoor()
    {
        if (Input.GetKeyDown("e") == true)
            {
                ChangeDoorSprite changeDoorSprite = target.GetComponent<ChangeDoorSprite>();
                changeDoorSprite.onOpen = true;
            }
    }

    // 박스 변형(회전, 이동)
    void TransferBox()
    {
        MoveBox movebox = target.GetComponent<MoveBox>();
        MoveBox moveBox = target.GetComponent<MoveBox>();
        if (onPushMode == false)
        {
            if (Input.GetKey("e") == true)
            {
                // 키를 누르고 있는 시간 확인
                canTransferBox = true;
                keyDownTime += Time.deltaTime;
                if (keyDownTime >= 1 && canTransferBox == true)
                {
                    // 밀기 모드로 전환
                    //playerComponent.jumpPower = 0f;
                    moveBox.dir = dirValue;
                    playerComponent.canJump = false;
                    canTransferBox = false;
                    onPushMode = true;
                    keyDownTime = 0f;
                }
            }
            else
            {
                if (canTransferBox == true)
                {
                    // 박스 회전
                    canTransferBox = false;
                    moveBox.onSpin = true;
                    Debug.Log("ONNNNNNNNNNNNNNNNNNNNNNNNNN");
                }

        if (Input.GetKeyDown("e") == true)
                keyDownTime = 0f;
            }
        }
        else
        {
            // 밀기 모드
            moveBox.onMove = true;

            if (Input.GetKeyDown("e") == true)
            {
                //MoveBox.onSpin = true;
                // 밀기 모드 해제 확인 [버그 방지용]
                canOffPushMode = true;
            }
        
        //if (Input)
            if (Input.GetKeyUp("e") == true && canOffPushMode == true)
            {
                // 밀기 모드 해제 확인
                //playerComponent.jumpPower = originalJumpPower;
                playerComponent.canJump = true;
                moveBox.onMove = false;
                onPushMode = false;
                canOffPushMode = false;
                //Debug.Log(originalJumpPower);
                //Debug.Log(playerComponent.jumpPower);
                Debug.Log("OFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
            }

            // 바라보는 방향 고정
        }
    }
}

