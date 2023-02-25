using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGuideSign : MonoBehaviour
{
    // 주의! 이 스크립트는 '가이드 스프라이트'가 적용되는 '부모'에 넣어주세요!!

    public float Distance;                      // 래이캐스트 인식 범위
    public float DelayTime;                     // 안내 스프라이트를 숨기는 딜레이
    public SpriteRenderer signRenderer;         // 스프라이트 렌더러 선언
    public bool onSignal;
    public GameObject myself;
    public GameObject player;
    private ChangeDoorSprite changeDoorSprite;   //체인지 도어 스프라이트 선언
    private PlayerRayCast playerRayCast;
    private bool onDelay;                       // 스프라이트숨기는 딜레이 활성화/비활성화
    private bool onShow;

    void Start()
    {
        playerRayCast = player.GetComponent<PlayerRayCast>();
        changeDoorSprite = GetComponent<ChangeDoorSprite>(); // 게임오브젝트의 체인지 도어 스프라이트 컴포넌트 가져오기

        signRenderer.enabled = false;
    }

    void Update()
    {
        /* Vector3 pos = transform.position;
        pos.y = transform.position.y / 2;

        //RaycastHit2D hit = Physics2D.Raycast(pos, dirVec, Distance, LayerMask.GetMask("Player"));
        RaycastHit2D hit = Physics2D.Raycast(pos, new Vector3(1,0,0), Distance, LayerMask.GetMask("Player"));
        RaycastHit2D hit2 = Physics2D.Raycast(pos, new Vector3(-1,0,0), Distance, LayerMask.GetMask("Player"));

        //Debug.DrawRay(pos, dirVec, Color.blue, 0.1f);
        Debug.DrawRay(pos, new Vector3(1, 0, 0), Color.green, 0.1f);
        Debug.DrawRay(pos, new Vector3(-1, 0, 0), Color.green, 0.1f); */

        //if (hit.collider != null || hit2.collider != null)
        //if (onSignal == true)
        if (playerRayCast.target == myself)
        {
            if (myself.tag == "Door")
            {
                if (changeDoorSprite.onOpen != true)
                {
                    onDelay = true;
                    signRenderer.enabled = true;
                }
                else
                {
                    HideSprite();
                }
            }
            if (myself.tag == "Box")
            {
                onDelay = true;
                signRenderer.enabled = true;
            }
        }
        else
        {
            if (onDelay == true)
            {
                onDelay = false;
                Invoke("HideSprite",DelayTime);
            }
        }
    }

    // 스프라이트 숨기기
    void HideSprite()
    {
        signRenderer.enabled = false;
    }
}
