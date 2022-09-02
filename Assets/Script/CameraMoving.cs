using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public GameObject target = null;
    [Range(0, 1)]public float lerpPower = 0.1f;

    void Update()
    {
        MovingCam();
    }

    void MovingCam()
    {
        Vector3 pos = target.transform.position;
        pos.y = transform.position.y;
        transform.position = Vector3.Lerp(transform.position, pos, lerpPower);
    }
}
