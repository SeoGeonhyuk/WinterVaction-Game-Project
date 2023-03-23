using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour
{
    public Slider progressbar;
    public float maxValue = 100;
    // Start is called before the first frame update
    void Awake()
    {
    }
    void Start()
    {
        progressbar.maxValue = maxValue;
        progressbar.value = 50;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(progressbar.value < 100 && Input.GetKey(KeyCode.E) && Player.beCaughtByPolice){
            progressbar.value += 1f;
        }
        if(progressbar.value >= 100f && Player.beCaughtByPolice){
            Player.beCaughtByPolice = false;
            GameManager.polices -= 1;
            progressbar.value = 50;
        }
        else if(Player.beCaughtByPolice)
            progressbar.value -= 0.2f * GameManager.polices;
    }
}
