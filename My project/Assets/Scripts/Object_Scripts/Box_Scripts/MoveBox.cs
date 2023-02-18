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
    private float correctionValue = 0f;
    private float distance;
    GameObject player;
    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

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

        distance = transform.localScale.x / 2 + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = Vector3.zero;

        if (onSpin == true)
        {
            // 회전
            Vector3 vec = new Vector3(0,0,90);
            transform.Rotate(vec);

            // 가로로 긴 오브젝트일 시 위치 조정
            if (spun == false && transform.localScale.x > transform.localScale.y)
            {
                distance = transform.localScale.y / 2 + 0.5f;
                transform.position += Vector3.up * correctionValue;
                spun = true;
            }
            else if (spun == true && transform.localScale.x > transform.localScale.y)
            {
                distance = transform.localScale.x / 2 + 0.5f;
                transform.position -= Vector3.up * correctionValue;
                spun = false;
            }

            // 세로로 긴 오브젝트일 시 위치 조정
            if (spun == false && transform.localScale.x < transform.localScale.y)
            {
                distance = transform.localScale.y / 2 + 0.5f;
                transform.position -= Vector3.up * correctionValue;
                spun = true;
            }
            else if (spun == true && transform.localScale.x < transform.localScale.y)
            {
                distance = transform.localScale.x / 2 + 0.5f;
                transform.position += Vector3.up * correctionValue;
                spun = false;
            }
            onSpin = false;
        }
        if (onMove == true)
        {
            // 박스 이동
            // 바라보는 방향 확인
            dir = transform.position.x - player.transform.position.x;
            if (dir > 0)
            {
                if (adjustment == true)
                {
                    // 플레이어 위치 조정
                    Vector3 playerPos = player.transform.position;
                    playerPos.x = transform.position.x - distance;
                    player.transform.position = playerPos;
                    adjustment = false;
                }
                else
                {
                    // 박스 이동
                    Vector3 pos = transform.position;
                    pos.x = player.transform.position.x + distance;
                    transform.position = pos;

                }
            }
            else if (dir < 0)
            {
                if (adjustment == true)
                {
                    // 플레이어 위치 조정
                    Vector3 playerPos = player.transform.position;
                    playerPos.x = transform.position.x + distance;
                    player.transform.position = playerPos;
                    adjustment = false;
                }
                else
                {
                    // 박스 이동
                    Vector3 pos = transform.position;
                    pos.x = player.transform.position.x - distance;
                    transform.position = pos;
                }
            }

            // 박스를 옮기던 중 타 오브젝트와 충돌이 안되는 점 수정 필요..
        }
    }
}
