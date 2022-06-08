using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BattleCamera : MonoBehaviour
{
    private Camera subCam = null;

    private void Start()
    {
        subCam = GetComponent<Camera>();
    }

    public void SettingBattleCam()
    {
        if (subCam.depth <= 6)
        {
            subCam.depth = 20;
        }
        else
        {
            subCam.depth = -10;
        }
    }
}
