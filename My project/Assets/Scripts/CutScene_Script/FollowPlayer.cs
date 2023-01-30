using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // 카메라의 플레이어 추적 기능

    // inspector에서 플레이어 지정
    public GameObject player;

    void Start()
    {
    }

    void Update()
    {
        // 위치 설정 (가시성을 위해 플레이어보다 y축이 1높게 설정함.)
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, 0);
    }
}
