using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    public GameObject door;
    public GameObject player;

    void Start()
    { 
        // 플레이어가 문에 막히는 것 방지
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), door.GetComponent<Collider2D>());
    }

    private void Update()
    {
        
    }
}