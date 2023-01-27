using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDoorSprite : MonoBehaviour
{
    public SpriteRenderer doorRenderer; // 스프라이트 렌더러 선언
    public Sprite[] Sprites;            // 닫힌 문/열린 문 스프라이트 가져오기
    public bool onOpen;               // PlayerRayCast에서 신호 받기

    void Start()
    {
        onOpen = false;
        doorRenderer = GetComponent<SpriteRenderer>(); // 게임오브젝트의 스프라이트 렌더러 컴포넌트 가져오기
    }

    void Update()
    {   
        if(onOpen == true)
        {
            doorRenderer.sprite = Sprites[1];
        }
        else
        {
            doorRenderer.sprite = Sprites[0];
        }
    }
}