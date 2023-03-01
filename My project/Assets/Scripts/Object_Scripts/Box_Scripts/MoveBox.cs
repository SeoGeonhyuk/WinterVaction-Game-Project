using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    // 주의 : 회전은 가로로 긴 오브젝트를 기준으로 진행됩니다.
    public bool onSpin = false;
    public bool spun = false;
    public bool onMove = false;
    public bool adjustment = true;
    public float dir;
    //private string freezePosition = "X";  // 가로가 길 경우 = X, 세로가 길 경우 = Y (주의 : 현재 가로로 긴 물체만 정상 작동합니다.)
    private bool isFixed = true;
    private float correctionValue = 0f;
    private float distance;
    GameObject player;
    Rigidbody2D rigidbody;
    Vector3 oldPlayerPos;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        // 오브젝트의 'y'크기에 맞추어 회전 시 위치 조정값 배정
        if (transform.localScale.x > transform.localScale.y)
        {
            correctionValue = transform.localScale.y / 2;
        }
        if (transform.localScale.x < transform.localScale.y)
        {
            correctionValue = transform.localScale.x / 2;
        }

        // 플레이어 찾기
        player = GameObject.Find("Player");

        distance = transform.localScale.x / 2 + 0.27f;
    }

    // Update is called once per frame
    void Update()
    {
        if (onSpin == true)
        {
            // 포지션 값 고정 해제
            /*if (freezePosition == "X")
            {
                rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
            else if (freezePosition == "Y")
            {
                rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            }*/
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;


            // 회전
            Vector3 vec = new Vector3(0,0,90);
            transform.Rotate(vec);

            // 가로로 긴 오브젝트일 시 위치 조정
            if (spun == false && transform.localScale.x > transform.localScale.y)
            {
                distance = transform.localScale.y / 2 + 0.27f;
                transform.position += Vector3.up * correctionValue;
                spun = true;
                //freezePosition = "Y";
            }
            else if (spun == true && transform.localScale.x > transform.localScale.y)
            {
                distance = transform.localScale.x / 2 + 0.27f;
                transform.position -= Vector3.up * correctionValue;
                spun = false;
                //freezePosition = "X";
            }

            // 세로로 긴 오브젝트일 시 위치 조정
            if (spun == false && transform.localScale.x < transform.localScale.y)
            {
                distance = transform.localScale.y / 2 + 0.27f;
                transform.position -= Vector3.up * correctionValue;
                spun = true;
                //freezePosition = "X";
            }
            else if (spun == true && transform.localScale.x < transform.localScale.y)
            {
                distance = transform.localScale.x / 2 + 0.27f;
                transform.position += Vector3.up * correctionValue;
                spun = false;
                //freezePosition = "Y";
            }
            
            // 포지션 값 고정
            onSpin = false;
            Invoke("OffFreezePosition",0.5f);
        }
        if (onMove == true)
        {
            // 포지션 값 고정 해제
            /*if (freezePosition == "X")
            {
                rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
            else if (freezePosition == "Y")
            {
                rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            }*/
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
            isFixed = false;

            // 박스 이동 (당기기)
            // 바라보는 방향 확인
            dir = transform.position.x - player.transform.position.x;
            Vector3 playerPos = player.transform.position;

            if (dir > 0)
            {
                if (adjustment == true)
                {
                    // 플레이어 위치 조정
                    //Vector3 playerPos = player.transform.position;
                    playerPos.x = transform.position.x - distance;
                    player.transform.position = playerPos;
                    adjustment = false;
                    oldPlayerPos = playerPos;
                }
                else
                {
                    // 박스 이동
                    if (playerPos.x < oldPlayerPos.x)
                    {
                        Vector3 pos = transform.position;
                        pos.x = playerPos.x + distance;
                        transform.position = pos;
                        oldPlayerPos.x = playerPos.x;
                    }
                    else if (playerPos.x > oldPlayerPos.x)
                    {
                        oldPlayerPos.x = playerPos.x;
                    }
                }
            }
            else if (dir < 0)
            {
                if (adjustment == true)
                {
                    // 플레이어 위치 조정
                    playerPos = player.transform.position;
                    playerPos.x = transform.position.x + distance;
                    player.transform.position = playerPos;
                    adjustment = false;
                    oldPlayerPos = playerPos;
                }
                else
                {
                    if (playerPos.x > oldPlayerPos.x)
                    {
                        // 박스 이동
                        Vector3 pos = transform.position;
                        pos.x = playerPos.x - distance;
                        transform.position = pos;
                        oldPlayerPos.x = playerPos.x;
                    }
                    else if (playerPos.x < oldPlayerPos.x)
                    {
                        oldPlayerPos.x = playerPos.x;
                    }
                }
            }
        }
        else
        {
            if (isFixed == false)
            {
                // 포지션 값 고정
                rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                isFixed = true;
            }
        }
    }

    void OffFreezePosition()
    {
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
