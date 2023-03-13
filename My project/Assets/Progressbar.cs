using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour
{
    public Slider progressbar;
    public float maxValue = 100;
    // Start is called before the first frame update
    void Start()
    {
        progressbar.maxValue = maxValue;
        progressbar.value = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.beCaughtByPolice)
            progressbar.value -= 3 * Player.polices;
        if(progressbar.value < 100 && Input.GetKeyDown(KeyCode.E)){
            progressbar.value += 5;
        }
        else if(progressbar.value >= 100){
            Player.beCaughtByPolice = false;
            Player.polices = 0;
            PoliceMove.savePlayerLife = true;
        }
    }
}
