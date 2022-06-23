using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleCamera : MonoBehaviour
{
    private Camera subCam = null;
    public GameObject target = null;
    public Image hitSprite = null;

    public PlayerBehave _playerBehave;

    private void Awake()
    {
        subCam = GetComponent<Camera>();
        subCam.depth = 20;
    }

    private void OnEnable()
    {
        SetCamera();
    }


    public void EnemyAttack()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(subCam.transform.DOMove(target.transform.position, 0.3f));
        seq.AppendCallback(() => transform.LookAt(target.transform.up));
        seq.AppendCallback(() => subCam.fieldOfView = 120);
        seq.AppendInterval(3f);
        //seq.Append(subCam.transform.DOShakePosition(0.2f, 10f));
        seq.AppendCallback(() => hitSprite.gameObject.SetActive(true));
        seq.AppendCallback(() => StartCoroutine(FadeHitImage()));
        //seq.AppendCallback(() => hitSprite.gameObject.SetActive(false));
        seq.AppendInterval(1f);
        seq.AppendCallback(() => SetCamera());
    }

    IEnumerator FadeHitImage()
    {
        Color c = hitSprite.color;

        while(c.a <= 1)
        {
            c.a += 0.1f;
            hitSprite.color = c;
            yield return new WaitForSeconds(0.03f);
        }

        while (c.a >= 0)
        {
            c.a -= 0.1f;
            hitSprite.color = c;
            yield return new WaitForSeconds(0.03f);
        }
    }

    private void SetCamera()
    {
        Vector3 targetPos = target.transform.position;
        subCam.fieldOfView = 93.8f;
        transform.position = new Vector3(targetPos.x - 0.3f, targetPos.y + 2.4f, targetPos.z - 5.3f);
        //transform.rotation *= Quaternion.Euler(new Vector3(21, 180, 0));
        transform.LookAt(targetPos);
    }
}
