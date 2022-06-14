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
        subCam.depth = 20;
    }

    private void OnEnable()
    {
        Vector3 targetPos = target.transform.position;
        transform.position = new Vector3(targetPos.x - 0.3f, targetPos.y + 2.4f, targetPos.z - 5.3f);
        //transform.rotation *= Quaternion.Euler(new Vector3(21, 180, 0));
        transform.LookAt(targetPos);
    }
}
