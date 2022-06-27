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

    public void BossHeal()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(subCam.transform.DOMove(target.transform.position, 0.3f));
        seq.AppendCallback(() => transform.LookAt(target.transform.position + new Vector3(1, 0, 1)));
        seq.AppendCallback(() => subCam.fieldOfView = 100);
        seq.AppendInterval(2f);
        seq.AppendCallback(() => StartCoroutine(ChangeFieldOfView()));
        seq.Join(subCam.transform.DOShakePosition(1.5f, 0.05f, 90));

        seq.AppendInterval(1f);
        //seq.AppendCallback(() => subCam.fieldOfView = 93.8f);
        seq.AppendCallback(() => SetCamera());
    }

    public void EnemyAttack()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(subCam.transform.DOMove(target.transform.position, 0.3f));
        seq.AppendCallback(() => transform.LookAt(target.transform.position + new Vector3(1, 0, 1)));
        seq.AppendCallback(() => subCam.fieldOfView = 120);
        seq.AppendInterval(2f);
        //seq.Append(subCam.transform.DOShakePosition(0.2f, 10f));
        seq.AppendCallback(() => hitSprite.gameObject.SetActive(true));
        seq.AppendCallback(() => StartCoroutine(FadeHitImage())).OnComplete(() => hitSprite.gameObject.SetActive(false));
        seq.Join(subCam.transform.DOShakePosition(0.5f, 2, 30));


        //seq.AppendCallback(() => hitSprite.gameObject.SetActive(false));
        seq.AppendInterval(1f);
        seq.AppendCallback(() => subCam.fieldOfView = 93.8f);
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

    IEnumerator ChangeFieldOfView()
    {
        float a = subCam.fieldOfView;

        while (a >= 45.3f)
        {
            a -= 2f;
            subCam.fieldOfView = a;
            yield return new WaitForSeconds(0.02f);
        }

        while (a <= 93.8f)
        {
            a += 4.5f;
            subCam.fieldOfView = a;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void SetCamera()
    {
        Vector3 targetPos = target.transform.position;
        transform.position = new Vector3(targetPos.x - 0.3f, targetPos.y + 2.4f, targetPos.z - 5.3f);
        //transform.rotation *= Quaternion.Euler(new Vector3(21, 180, 0));
        transform.LookAt(targetPos);
    }
}
