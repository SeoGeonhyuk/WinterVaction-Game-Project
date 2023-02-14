using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    void Start()
    { 
    }

    private void Update()
    {
        // 플레이어가 문에 막히는 것 방지
        Physics2D.IgnoreLayerCollision(7,6);
    }
}