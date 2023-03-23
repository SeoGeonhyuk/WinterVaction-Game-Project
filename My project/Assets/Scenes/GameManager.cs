using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int polices = 0;
    public static bool isPause = false;
    public static bool canPlayerMove = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPause){
            canPlayerMove = false;
        }
        else
            canPlayerMove = true;
    }
}
