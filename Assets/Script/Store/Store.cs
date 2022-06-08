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

            Debug.Log($"�� ���� : {level} ���� �� ���� �������� ���� �ϴ� exp : {exppr} exp��ġ : {expsi}");
        }
    }
}
