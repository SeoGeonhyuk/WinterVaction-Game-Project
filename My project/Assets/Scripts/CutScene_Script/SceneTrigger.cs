using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SceneTrigger : MonoBehaviour
{
    public TimelineAsset timeline;              // 재생하고자하는 타임라인 가져오기
    public SpriteRenderer Blinder_Top;          // 블라인더 스프라이트(위) 가져오기
    public SpriteRenderer Blinder_Bottom;       // 블라인더 스프라이트(아래) 가져오기
    private PlayableDirector playableDirector;  // PlayableDirector 스크립트
    private bool canShowScene;                  // 컷씬 시청을 1회 제한으로 지정하는 변수
    private bool isJumping;

    void Start()
    {
        // PlayerableDirector 스크립트 가져오기
        playableDirector = GetComponent<PlayableDirector>();

        canShowScene = true;
    }

    void Update()
    { 
    }

    // 다른 씬 불러오기 방법 (미완)
    /*
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player" && canShowScene == true)
        {
            canShowScene = false;
            SceneManager.LoadScene("Scene2",LoadSceneMode.Additive);
        }
    }
    */

    // 타임라인 불러오기 방법
    private void OnTriggerEnter2D(Collider2D other) 
    {
        // 트리거 발동 조건 확인 및 컷씬 활성화
        if (other.gameObject.tag == "Player" && canShowScene == true)
        {
            canShowScene = false;
            playableDirector.Play();        // 컷씬 활성화
        }
    }
}
