using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed;//이동속도 값 설정
    public float jumpPower;//점프 값 설정
    public bool canJump = true;
    private bool isJumping = false; //점프 한 번만 되게 설정
    private bool playerDirection = true;
    Rigidbody2D rigid;

    //애니메이터 파라메터
    public Animator animator;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate() // 플레이어 움직임은 Update 문이 아니라 FixedUpdate 문에 써야대용 아니면 캐릭터가 움직일 때 덜덜 떨리는 현상이 생기더라고요
    {
        if (GameManager.canPlayerMove)
        {
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

            if (Input.GetButton("Jump") && !isJumping && canJump == true)
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                isJumping = true;
            }
        }


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
