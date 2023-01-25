using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed;//이동속도 값 설정
    public float jumpPower;//점프 값 설정
    private bool isJumping = false; //점프 한 번만 되게 설정
    Rigidbody2D rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.canPlayerMove){
            float h = Input.GetAxisRaw("Horizontal");
            rigid.AddForce(Vector2.right*h, ForceMode2D.Impulse);
            if (rigid.velocity.x > maxSpeed){
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            }
            else if (rigid.velocity.x < maxSpeed*(-1)){
                rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
            }
            if (Input.GetButton("Jump") && !isJumping){
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                isJumping = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Ground")) { 
                isJumping = false;   
            }
    }
}
