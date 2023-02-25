using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateGuideSign : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = transform.parent.transform.position.y + (transform.parent.transform.localScale.y / 2) + 0.5f;
        transform.position = pos;
    }
}
