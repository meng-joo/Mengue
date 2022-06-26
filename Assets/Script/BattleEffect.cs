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
        Sequence seq = DOTween.Sequence();
        seq.Append(_battleEffectPanel[0].transform.DOMoveX(960, 0.5f).SetEase(Ease.OutQuart));
        seq.Append(_battleEffectPanel[1].transform.DOMoveX(960, 0.5f).SetEase(Ease.OutQuart));
        seq.Append(_profileImage[0].transform.DOLocalMoveX(-638, 0.7f));
        seq.Join(_profileImage[1].transform.DOLocalMoveX(638, 0.7f));


        seq.Append(_vs[0].transform.DOLocalMoveX(-50, 0.5f));
        seq.Append(_vs[0].transform.DOShakePosition(0.3f, 100f, 60));
        seq.Join(_vs[1].transform.DOLocalMoveX(73, 0.5f));
        seq.Append(_vs[1].transform.DOShakePosition(0.3f, 100f, 60));

        seq.AppendInterval(1.8f);

        seq.Append(_profileImage[0].transform.DOLocalMoveX(1300, 0.2f));
        seq.Join(_profileImage[1].transform.DOLocalMoveX(-1300, 0.23f));
        seq.Append(_battleEffectPanel[0].transform.DOLocalMoveX(2643.6f, 0.2f).SetEase(Ease.InQuart));
        seq.Append(_battleEffectPanel[1].transform.DOLocalMoveX(-2643.6f, 0.2f).SetEase(Ease.InQuart));
        seq.Append(_vs[0].transform.DOLocalMoveX(-1300, 0.2f));
        seq.Join(_vs[1].transform.DOLocalMoveX(1300, 0.23f));

        seq.AppendCallback(() =>
        {
            _battleEffectPanel[0].transform.position = new Vector3(-2000.6f, _battleEffectPanel[0].transform.position.y, _battleEffectPanel[0].transform.position.z);
            _battleEffectPanel[1].transform.position = new Vector3(3700, _battleEffectPanel[1].transform.position.y, _battleEffectPanel[1].transform.position.z);
            _profileImage[0].transform.position = new Vector3(-2551, _profileImage[0].transform.position.y, _profileImage[0].transform.position.z);
            _profileImage[1].transform.position = new Vector3(2551, _profileImage[1].transform.position.y, _profileImage[1].transform.position.z);
            //_battleEffectPanel[1].transform.position = new Vector3(2643.6f, _battleEffectPanel[1].transform.position.y, _battleEffectPanel[1].transform.position.z);
        });
    }
}