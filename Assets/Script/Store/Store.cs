using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [Range(0,100)]
    public int level;

    float exppr;
    float expsi;
    float sas;

    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

            exppr = level + ((Mathf.Pow(level * 2, 2) / 2) + level);
            expsi = level - (Mathf.Log(level, level + 1f));

            Debug.Log($"현 레벨 : {level} 레벨 당 다음 레벨까지 얻어야 하는 exp : {exppr} exp가치 : {expsi}");
        }
    }
}
