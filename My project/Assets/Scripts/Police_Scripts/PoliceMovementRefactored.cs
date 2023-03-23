using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class PoliceMovementRefactored : MonoBehaviour
{
    public enum policeState // 경찰 상태 종류
{
    Normal,
    Concious,
    Tracing,
    Fighting,
    Stun
};
    
    
    public policeState policeType; // 경찰 상태를 나타내는 변수
    
    public int nextMove; // 경찰이 움직이는 방향
    
    public float normalSpeed; // 일반 모드일 때 경찰이 움직이는 속도
    
    public float traceSpeed; // 추적 모드일 때 경찰이 움직이는 속도
    
    public float conciousGage; // 의심 모드일 때 경찰의 플레이어 의심 게이지
    public float conciousGagePlus; // 의심 모드일 때 경찰의 플레이어 의심 게이지 증가량

    public float traceGage; // 추적 모드일 때 경찰의 플레이어 추적 게이지
    public float traceGagePlus; // 추적 모드일 때 경찰의 플레이어 추적 게이지 증가량

    public bool stunCheck; // 기절 모드일 때 경찰의 기절 체크
    public int stunTime; // 기절 모드일 때 경찰의 기절 시간 설정을 위한 변수
    private float stunTimeCheck; // 기절 모드일 때 경찰의 기절 시간 체크를 위한 변수
    
    private Rigidbody2D rigid; // 경찰 오브젝트를 가져오기 위한 변수
    
    private RaycastHit2D rayHitGround; // 이동 중 땅을 감지하기 위한 변수
    private Vector2 wallVec; // 땅을 감지하는 벡터의 길이
    
    private RaycastHit2D rayHitPlayer; // 이동 중 플레이어를 감지하기 위한 변수
    private Vector2 playerVec; // 플레이어를 감지하는 벡터의 길이
    
    // Start is called before the first frame update
    void Awake()
    {
        stunCheck = false;
        rigid = GetComponent<Rigidbody2D>();
        PoliceUsuallyMove();
        stunTimeCheck = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate () {
        switch (policeType)
        {
            case policeState.Normal:
                // case1 : normal //일반 모드
                // 사용 변수: 경찰 상태
                
                // normal 에 해당하는 움직임
                // 플레이어를 발견하지 못했을 경우, 좌우로 느린 속도로 움직인다.
                // 변경 분기 (체크해서 꼭 한번만 실행되도록)
                // 플레이어를 발견했을 경우 해당 모드가 종료되고 concious 모드로 변경된다.
                policeMeetWall();
                rigid.velocity = new Vector2(Time.deltaTime * nextMove * normalSpeed, 0);
                if (PoliceMeetPlayer())
                {
                    policeType = policeState.Concious;
                    conciousGage = 10;
                }
                break;
            
            case policeState.Concious:
                // case2 : concious //의심 모드. 플레이어를 발견했을 때 일반 모드에서 변경되는 모드
                // 사용 변수: 플레이어 의심 게이지, 플레이어 의심 게이지 증가량, 경찰 상태
                
                // concious 에 해당하는 움직임
                // 플레이어가 감지되는 동안 그 자리에 서 있으며 계속 감지되는 경우 플레이어 의심게이지가 증가한다.
                // 플레이어 의심 게이지가 100이 되면, 해당 모드가 종료되고 tracing 모드로 변경된다.
                // 플레이어가 감지되지 않을 경우 플레이어 의심게이지가 감소되며, 플레이어 의심게이지가 0이 될 경우 해당 모드가 종료되고
                // 일반 모드로 변경된다.

                // 변경 분기 (체크해서 꼭 한번만 실행되도록)
                // 추적 모드 변경
                // 일반 모드 변경
                rigid.velocity = new Vector2(0, 0);
                if (PoliceMeetPlayer())
                {
                    conciousGage += Time.deltaTime * conciousGagePlus;
                }
                else
                {
                    conciousGage -= Time.deltaTime * conciousGagePlus;
                }

                if (conciousGage >= 100)
                {
                    conciousGage = 0;
                    traceGage = 10;
                    policeType = policeState.Tracing;
                }
                else if (conciousGage < 0)
                {
                    conciousGage = 0;
                    policeType = policeState.Normal;
                }
                break;
            
            case policeState.Tracing:
                // case3 : tracing // 추적 모드. 의심 모드에서 플레이어 의심 게이지가 100이 될 경우 해당 모드로 변경된다.
                // 사용 변수: 플레이어 추적 게이지, 플레이어 추적 게이지 증가량, 경찰 상태
                
                // tracing 에 해당하는 움직임
                // 플레이어가 추적 범위 안에 계속 감지되는 경우 플레이어 추적 게이지가 증가한다.
                // 플레이어가 추적 범위 안에 없는 경우 플레이어 추적 게이지가 감소한다.
                // 플레이어 추적 게이지가 0이 아닌 경우 계속 플레이어를 추적한다.
        
                // 변경 분기 (체크해서 꼭 한번만 실행되도록)
                // 플레이어 추적 게이지가 0일 경우 concious의 플레이어 의심게이지를 50으로 설정하고 해당 모드를 종료시킨 뒤
                // 의심 모드로 변경된다.
                policeMeetWall();
                rigid.velocity = new Vector2(Time.deltaTime * nextMove * traceSpeed, 0);
                if (PoliceMeetPlayer() && traceGage <= 100)
                    traceGage += traceGagePlus * Time.deltaTime;
                else
                    traceGage -= traceGagePlus * Time.deltaTime;
                if (traceGage <= 0)
                {
                    traceGage = 0;
                    conciousGage = 50;
                    policeType = policeState.Concious;
                }
                break;
            
            case policeState.Fighting:
                // case4 : fighting // 전투 모드. 플레이어가 경찰의 몸에 닿을 경우 해당 모드로 변경된다.
                // 사용 변수: 미정
                
                // fighting 에 해당하는 움직임
                // fighting 모드 중에 플레이어가 경찰과의 싸움에서 이기기 전까지 정지한다.
                
                // 변경 분기 (체크해서 꼭 한번만 실행되도록)
                // 플레이어가 경찰과의 싸움에서 이겼을 경우 해당 모드가 종료되고 기절 모드로 변경된다.
                rigid.velocity = new Vector2(0, 0);
                if (!Player.beCaughtByPolice)
                {
                    PlayerWin();
                    policeType = policeState.Stun;
                }

                break;
            
            case policeState.Stun:
                // case5 : stun // 기절 모드. 플레이어가 경찰과의 싸움에서 이겼을 경우 해당 모드로 변경된다.
                // 사용 변수: 기절 확인
                
                // stun 에 해당하는 움직임
                // stun 모드 중에 경찰은 플레이어 곁에서 멀리 튕겨져 나가고 그 자리에서 6초 동안 기절(정지)한다.
                
                // 변경 분기 (체크해서 꼭 한번만 실행되도록)
                // 6초간 기절 후 해당 모드가 종료되고 일반 모드로 변경된다.
                if (stunTimeCheck < stunTime)
                {
                    stunTimeCheck += Time.deltaTime;
                }
                else
                {
                    stunTimeCheck = 0;
                    policeType = policeState.Normal;
                }
                break;
            
            default:
                break;
        }
        // 다른 case : 플레이어가 탈출했을 때, 졌을 때 (기절했을 때) (한번만 실행이 되게) (Coroutine / Invoke - 몇 초 뒤에 실행이 되게 만들면 좋을 듯?)
    }

    // 벽 만났을 때 경찰 이동 방향 결정 (Method)
    void policeMeetWall()
    {
        wallVec = new Vector2(rigid.position.x + (nextMove * 0.2f), rigid.position.y);
        Debug.DrawRay(wallVec, Vector3.down, new Color(0, 1, 0));
        rayHitGround = Physics2D.Raycast(wallVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (rayHitGround.collider == null)
        {
            nextMove = -1 * nextMove;
        }
    }
    
    // 플레이어 감지했을 때 경찰 행동 결정 (Method)
    bool PoliceMeetPlayer()
    {
        playerVec = new Vector3(nextMove, 0);
        Debug.DrawRay(new Vector3(rigid.position.x, rigid.position.y), playerVec, new Color(1, 0, 0));
        rayHitPlayer = Physics2D.Raycast(new Vector3(rigid.position.x, rigid.position.y), playerVec, 2, LayerMask.GetMask("Player"));
        if (rayHitPlayer.collider != null)
        {
            return true;
        }
        return false;
        }
    
    // 평소 경찰 이동 방향 결정 (Method)
    void PoliceUsuallyMove()
    {
        nextMove = Random.Range(-1, 2);
        if(nextMove == 0)
            PoliceUsuallyMove();
    }
    
    // 플레이어 만났을 때 경찰 행동 (Method)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            policeType = policeState.Fighting;
            Player.beCaughtByPolice = true;
            GameManager.polices += 1;
        }
    }
    
    // 플레이어가 경찰과의 싸움에서 승리하여 경찰이 튕겨지는 행동 (Method)
    public void PlayerWin()
    {
        rigid.AddForce(new Vector2((-1 * nextMove * 70) * Time.deltaTime, (rigid.position.y + 50) * Time.deltaTime), ForceMode2D.Impulse); //여기서 거리 조절 하세요
        traceGage = 0;
    }

    // 플레이어가 경찰과의 싸움에서 승리하여 튕겨져 나왔을 때 경찰 행동 (Method)
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            policeType = policeState.Stun;
            GameManager.polices -= 1;
        }
    }
    
}
