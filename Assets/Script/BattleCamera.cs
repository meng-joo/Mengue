using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BattleCamera : MonoBehaviour
{
    private Camera subCam = null;
    public GameObject target = null;

    private void Awake()
    {
        subCam = GetComponent<Camera>();
        subCam.depth = 0;
    }

    private void OnEnable()
    {
        Vector3 targetPos = target.transform.position;
        transform.position = new Vector3(targetPos.x, targetPos.y + 4 ,targetPos.z + 5f);
        //transform.rotation *= Quaternion.Euler(new Vector3(21, 180, 0));
        transform.LookAt(targetPos); ;
    }

    public void SettingBattleCam()
    {
        if (Moving._isBattle)
        {
            subCam.depth = 20;
        }
        else
        {
            Debug.Log("¹¹¿©¤Á");
            subCam.depth = -10;
        }
    }
}
