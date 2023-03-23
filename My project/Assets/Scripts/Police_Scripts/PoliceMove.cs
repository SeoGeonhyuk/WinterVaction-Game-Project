using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMove : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool savePlayerLife = false;
    Rigidbody2D rigid;
    public int nextMove;//경찰이 움직이는 방향
    public int normalSpeed = 1;//경찰의 기본 이동 속도
    public float traceSpeed = 3.6f;//경찰의 추격 모드 이동 속도
    public float playerGagePlus = 5;//경찰의 플레이어 감지 게이지 증가 속도
    public float playerGage = 0;
    public float traceGage = 0;
    private bool normalMode = false;
    public bool traceMode = false;
    private bool conciousMode = false;
    private bool conciousPausePlayer = false;
    private bool caught = false;
    public int saveDir = 0;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Think();
        normalMode = true;
        if(nextMove == 0)
            nextMove = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(savePlayerLife){//플레이어가 경찰과 싸워서 이겼을 때
            Debug.Log("FixedUpdate savePlayerlife running");
            rigid.velocity = new Vector2(-100 * nextMove * Time.deltaTime, rigid.velocity.y);
            // CancelInvoke();
            nextMove = 0;
            Invoke("Think", 6);
            traceMode = false;
            conciousPausePlayer = false;
            normalMode = true;
        }

        //땅체크
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHitGround = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (rayHitGround.collider == null){
            nextMove = -1 * nextMove;
            saveDir = nextMove;
            CancelInvoke();
        }
        
        //플레이어 감지
        Vector2 PlayerVec = new Vector2(rigid.position.x + nextMove * 2f, rigid.position.y + 0.4f);
        RaycastHit2D rayHitPlayer = Physics2D.Raycast(PlayerVec, Vector3.up, 1, LayerMask.GetMask("Player"));
        if(rayHitPlayer.collider != null && (normalMode)){
                normalMode = false;
                conciousPausePlayer = true;
                CancelInvoke();
            }
        else if(rayHitPlayer.collider != null && conciousPausePlayer){
            if(playerGage < 100)
                playerGage += Time.deltaTime * playerGagePlus;
        }
        else if(rayHitPlayer.collider == null && conciousPausePlayer){
            if(playerGage > 0){
                playerGage -= Time.deltaTime * playerGagePlus;
            }
            if(playerGage <= 0){
                playerGage = 0;
                conciousPausePlayer = false;
                normalMode = true;
                Debug.Log("Thinking...");
                Think();
                Invoke("Think", 6);
                if(nextMove == 0){
                    nextMove = -1;
                } 
            }
        }
        else if(rayHitPlayer.collider == null && traceMode){
            nextMove = 0;
        }
        //모드에 따른 움직임 속도
        if(normalMode)
            rigid.velocity = new Vector2(nextMove * normalSpeed, rigid.velocity.y);
        else if(conciousPausePlayer)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);

            if(playerGage >= 100){
                conciousPausePlayer = false;
                traceMode = true;
            }
        }
        else if(traceMode)
        {
            rigid.velocity = new Vector2(saveDir * traceSpeed, rigid.velocity.y);
            if(nextMove != 0){
                if(traceGage <= 100)
                    traceGage +=  playerGagePlus;
            }
            if(nextMove == 0){
                if(traceGage > 0)
                    traceGage -= Time.deltaTime * playerGagePlus;
                if(traceGage <= 0){
                    traceGage = 0;
                    saveDir = 0;
                    conciousPausePlayer = true;
                    playerGage = 50;
                    traceMode = false;
                }
            }
        }
    }
    void Think(){
        Debug.Log("Thinking...");
        nextMove = Random.Range(-1, 2);
        if(savePlayerLife)
            savePlayerLife = false;
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Player"))
            nextMove = 0;
    }
    void OnCollisionExit2D(Collision2D col){
        //if(col.gameObject.CompareTag("Player"))
            //rigid.velocity = new Vector2(0, rigid.velocity.y);
    }
}


    // void TurnOnAndOffConciousPausePlayer()
    // {
    //     conciousPausePlayer = !conciousPausePlayer;
    // }
