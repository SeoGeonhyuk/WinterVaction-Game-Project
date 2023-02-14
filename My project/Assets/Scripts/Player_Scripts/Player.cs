using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed;//이동속도 값 설정
    public float jumpPower;//점프 값 설정
    public float ladderSpeed;//사다리 속도 값 설정
    private bool isJumping = false; //점프 한 번만 되게 설정
    private bool isLaddering = false;
    private bool isLadderingcanMove = true;
    private bool playerDirection = true;
    private int playerLayer;
    private int groundLayer;
    Rigidbody2D rigid;

    //애니메이터 파라메터
    public Animator animator;

    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        groundLayer = LayerMask.NameToLayer("Ground");
    }
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate() // 플레이어 움직임은 Update 문이 아니라 FixedUpdate 문에 써야대용 아니면 캐릭터가 움직일 때 덜덜 떨리는 현상이 생기더라고요
    {
        if (GameManager.canPlayerMove && isLadderingcanMove)
        {
            rigid.gravityScale = 3;
            Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, false);
            maxSpeed = 4;
            float h = Input.GetAxisRaw("Horizontal");
            if (h > 0)
            {
                rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
                if (rigid.velocity.x > maxSpeed)
                {
                    playerDirection = true;
                    rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
                    transform.localScale = new Vector2(0.5f, 0.5f);
                    animator.SetBool("IsWalk", true);
                }
            }
            else if (h < 0)
            {
                rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
                if (rigid.velocity.x < maxSpeed * (-1))
                {
                    playerDirection = false;
                    rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
                    transform.localScale = new Vector2(-0.5f, 0.5f);
                    animator.SetBool("IsWalk", true);

                }
            }
            else
            {
                if (playerDirection)
                    transform.localScale = new Vector2(0.5f, 0.5f);
                else
                    transform.localScale = new Vector2(-0.5f, 0.5f);
                maxSpeed = 0;
                animator.SetBool("IsWalk", false);
            }

            if (Input.GetButton("Jump") && !isJumping)
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                isJumping = true;
            }
        }

        if(isLaddering){
            float h = Input.GetAxisRaw("Vertical");
            if(h > 0)
                this.transform.Translate(0, 2 * Time.deltaTime, 0);
            else if(h < 0)
                this.transform.Translate(0, -2 * Time.deltaTime, 0);
            else
                this.transform.Translate(0, 0, 0);
            Debug.Log("사다리2");
            Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, true);
            if (Input.GetButton("Jump") && !isJumping)
            {
                Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, false);
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                rigid.gravityScale = 3;
                isJumping = true;
                GameManager.canPlayerMove = true;
                isLaddering = false;
            }
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") && isJumping)
        {
            isJumping = false;
        }
        else if (col.gameObject.CompareTag("Ground") && isLaddering)
        {
            rigid.gravityScale = 3;
            isLaddering = false;
            isLadderingcanMove = true;
            Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, false);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {

    }
    
    void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("Ladder") && Input.GetAxisRaw("Vertical") > 0)
        {
            rigid.gravityScale = 0;
            rigid.velocity = new Vector2(0, 0);
            Debug.Log("사다리");
            animator.SetBool("IsWalk", false);
            isLaddering = true;
            isLadderingcanMove = false;
        }
        if (col.gameObject.CompareTag("LadderTop") && Input.GetAxisRaw("Vertical") < 0)
        {
            rigid.gravityScale = 0;
            rigid.velocity = new Vector2(0, 0);
            Debug.Log("사다리");
            animator.SetBool("IsWalk", false);
            isLaddering = true;
            isLadderingcanMove = false;
        }

        // else if (col.gameObject.CompareTag("Ladder-Middle") && Input.GetAxisRaw("Vertical") != 0)
        // {
        //     animator.SetBool("IsWalk", false);
        //     GameManager.canPlayerMove = false;
        //     float h = Input.GetAxisRaw("Vertival");
        // }

        // else if (col.gameObject.CompareTag("Ladder-Bottom") && Input.GetAxisRaw("Vertical") != 0)
        // {
        //     animator.SetBool("IsWalk", false);
        //     GameManager.canPlayerMove = false;
        //     float h = Input.GetAxisRaw("Vertival");
        // }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("LadderTop"))
        {

            rigid.gravityScale = 3;
            isLaddering = false;
            isLadderingcanMove = true;
            Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, false);
        }
    }
}
