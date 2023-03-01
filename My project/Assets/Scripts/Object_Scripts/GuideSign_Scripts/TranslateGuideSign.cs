using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateGuideSign : MonoBehaviour
{
    public GameObject player;

    void Start()
    {

    }

    void Update()
    {
        Vector3 pos = player.transform.position;
        pos.y = player.transform.position.y + (player.transform.localScale.y / 2) + 0.5f;
        transform.position = pos;
    }
}
