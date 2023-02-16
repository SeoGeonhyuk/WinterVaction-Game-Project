using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    // 주의 : 회전은 가로로 긴 오브젝트를 기준으로 진행됩니다.
    public bool onSpin = false;
    public bool spun = false;
    public bool onMove = false;
    public float dir;
    private float correctionValue = 0f;
    private float distance;
    GameObject player;
    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        // 오브젝트의 'y'크기에 맞추어 회전 시 위치 조정값 배정
        correctionValue = transform.localScale.y / 2;

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

            // 위치 조정 (1회, 3회, 5회 .. 회전 시 조정되도록 설계함.)
            if (spun == false)
            {
                distance = transform.localScale.y / 2 + 0.5f;
                transform.position += Vector3.up * correctionValue;
                spun = true;
            }
            else
            {
                distance = transform.localScale.x / 2 + 0.5f;
                transform.position -= Vector3.up * correctionValue;
                spun = false;
            }
            onSpin = false;
        }
        if (onMove == true)
        {
            dir = transform.position.x - player.transform.position.x;
            if (dir > 0)
            {
                Vector3 pos = transform.position;
                pos.x = player.transform.position.x + distance;
                transform.position = pos;
            }
            else if (dir < 0)
            {
                Vector3 pos = transform.position;
                pos.x = player.transform.position.x - distance;
                transform.position = pos;
            }
        }
    }
}
