using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGuideSign : MonoBehaviour
{
    public float Distance;                // 래이캐스트 인식 범위
    public float DelayTime;               // 안내 스프라이트를 숨기는 딜레이
    public SpriteRenderer signRenderer;  // 스프라이트 렌더러 선언
    private bool onDelay;               // 스프라이트숨기는 딜레이 활성화/비활성화

    void Start()
    {
        signRenderer.enabled = false;
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(1,-1,0), Distance, LayerMask.GetMask("Player"));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector3(-1,-1,0), Distance, LayerMask.GetMask("Player"));

        Debug.DrawRay(transform.position, new Vector3(1, -2, 0), Color.green, 0.1f);
        Debug.DrawRay(transform.position, new Vector3(-1, -2, 0), Color.green, 0.1f);

        if (hit.collider != null || hit2.collider != null)
        {
            onDelay = true;
            signRenderer.enabled = true;
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
