using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    public Sprite[] Sprites;    // 도움말 스프라이트 가져오기
    public int Distance;        // 인식 범위
    public GameObject target;   // 레이캐스트로 인식한 게임오브젝트
    public GameObject guideSign;
    private float time;
    private float keyDownTime;
    private bool canTransferBox = false;
    private bool onPushMode = false;
    private bool canOffPushMode = false;
    private float dirValue;
    private Player playerComponent;
    private SpriteRenderer guideSignSpriteRenderer;

    Vector3 dirVec;

    void Start()
    {
        //guideSign = transform.GetChild(0).gameObject;
        guideSignSpriteRenderer = guideSign.GetComponent<SpriteRenderer>();

        playerComponent = GetComponent<Player>();
    }

    void Update()
    {
        // 플레이어 방향 체크 (레이케스트 용)
        if (onPushMode == false)
        {
            dirValue = Input.GetAxisRaw("Horizontal");
            if (dirValue < 0)
            {
                dirVec = new Vector3(Vector3.left.x, Vector3.left.y);
                //guideSignSpriteRenderer.flipX = true;
                //guideSignSpriteRenderer.transform.localScale = new Vector2(-0.05f, 0.05f);
            }
            else if (dirValue > 0)
            {
                dirVec = new Vector3(Vector3.right.x, Vector3.right.y);
                //guideSignSpriteRenderer.flipX = false;
                //guideSignSpriteRenderer.transform.localScale = new Vector2(0.05f, 0.05f);
            }
        }

        //'문' 인식
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y), dirVec, Distance, LayerMask.GetMask("Object"));

        // 레이케스트 디버그
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y), dirVec, Color.blue, 0.1f);

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            if (target.tag == "Door" && onPushMode == false)
            {
                // 'guideSign' 보이게 하기
                guideSignSpriteRenderer.sprite = Sprites[1];
                //guideSign = transform.GetChild(0).gameObject;
                //guideSignSpriteRenderer.enabled = true;

                // 문 열기 함수
                OpenDoor();
            }

            if (target.tag == "Box")
            {
                // 'guideSign' 보이게 하기
                guideSignSpriteRenderer.sprite = Sprites[1];
                //guideSign = transform.GetChild(0).gameObject;
                //guideSignSpriteRenderer.enabled = true;

                // 박스 변형 함수
                TransferBox();
            }
        }
        else
        {
            // 'guideSign' 가리기
            guideSignSpriteRenderer.sprite = Sprites[0];
            //guideSignSpriteRenderer.enabled = false;
        }
    }

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
                    playerComponent.canJump = false;
                    canTransferBox = false;
                    onPushMode = true;
                    moveBox.adjustment = true;
                    moveBox.onMove = true;      // MoveBox 스크립트에서 위치 조정
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
                    Debug.Log("Spin!!");
                }

                keyDownTime = 0f;
            }
        }
        else
        {
            // 밀기 모드
            if (Input.GetKeyDown("e") == true)
            {
                // 밀기 모드 해제 확인 [버그 방지용]
                canOffPushMode = true;
            }
            if (Input.GetKeyUp("e") == true && canOffPushMode == true)
            {
                // 밀기 모드 해제 확인
                playerComponent.canJump = true;
                moveBox.onMove = false;
                onPushMode = false;
                canOffPushMode = false;
            }
        }
    }
}

