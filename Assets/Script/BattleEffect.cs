using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleEffect : MonoBehaviour
{
    public GameObject[] _battleEffectPanel;
    public TextMeshProUGUI[] _vs;
    public Image[] _profileImage;
    public Sprite[] _enemyprofile;

    public void SetProfile(int i)
    {
        _profileImage[1].sprite = _enemyprofile[i];
        SetBattleAni();
    }

    void SetBattleAni()
    {
        _battleEffectPanel[0].SetActive(true);
        _battleEffectPanel[1].SetActive(true);
        _profileImage[0].gameObject.SetActive(true);
        _profileImage[1].gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence();

        seq.Append(_battleEffectPanel[0].transform.DOLocalMoveX(120, 0.4f));
        seq.Append(_battleEffectPanel[1].transform.DOLocalMoveX(120, 0.4f));
        seq.Append(_profileImage[0].transform.DOLocalMoveX(-638, 0.4f));
        seq.Join(_profileImage[1].transform.DOLocalMoveX(638, 0.4f));

        seq.Append(_profileImage[0].transform.DOShakePosition(0.2f, 50f, 100));
        seq.Join(_profileImage[1].transform.DOShakePosition(0.2f, 50f, 100));
        seq.InsertCallback(1f, () => SoundClips.instance.EffectSound(5));


        seq.Append(_vs[0].transform.DOLocalMoveX(-50, 0.4f));
        seq.Append(_vs[0].transform.DOShakePosition(0.3f, 80f, 70));
        seq.Join(_vs[1].transform.DOLocalMoveX(73, 0.4f));
        seq.Append(_vs[1].transform.DOShakePosition(0.3f, 80f, 70));
        seq.InsertCallback(1f, () => { SoundClips.instance.EffectSound(5); });

        seq.AppendInterval(1.1f);

        seq.Append(_profileImage[0].transform.DOLocalMoveX(1300, 0.2f));
        seq.Join(_profileImage[1].transform.DOLocalMoveX(-1300, 0.2f));


        seq.Append(_battleEffectPanel[0].transform.DOLocalMoveX(2643.6f, 0.2f).SetEase(Ease.InQuart));
        seq.Append(_battleEffectPanel[1].transform.DOLocalMoveX(-2643.6f, 0.2f).SetEase(Ease.InQuart));
        seq.Append(_vs[0].transform.DOLocalMoveX(-1300, 0.2f));
        seq.Join(_vs[1].transform.DOLocalMoveX(1300, 0.2f));

        seq.AppendCallback(() =>
        {
            _battleEffectPanel[0].transform.position = new Vector3(-40f, _battleEffectPanel[0].transform.position.y, _battleEffectPanel[0].transform.position.z);
            _battleEffectPanel[1].transform.position = new Vector3(50f, _battleEffectPanel[1].transform.position.y, _battleEffectPanel[1].transform.position.z);
            _profileImage[0].transform.position = new Vector3(-18f, _profileImage[0].transform.position.y, _profileImage[0].transform.position.z);
            _profileImage[1].transform.position = new Vector3(30f, _profileImage[1].transform.position.y, _profileImage[1].transform.position.z);

            _battleEffectPanel[0].SetActive(false);
            _battleEffectPanel[1].SetActive(false);
            _profileImage[0].gameObject.SetActive(false);
            _profileImage[1].gameObject.SetActive(false);
        });
    }
}
