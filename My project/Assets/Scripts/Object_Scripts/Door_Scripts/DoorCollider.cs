using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    public GameObject door;
    public GameObject player;

    void Start()
    { 
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), door.GetComponent<BoxCollider2D>());
        Physics2D.IgnoreLayerCollision(7,6);
    }

    private void Update()
    {
        // 플레이어가 문에 막히는 것 방지
        
        //Physics2D.IgnoreCollision(7,6);
    }
}