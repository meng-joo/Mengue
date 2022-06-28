using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingUI : MonoBehaviour
{
    private Button exitButton;

    public Image[] _soundFillImage;
    public AudioSource _backgroundAudio;
    public AudioSource _effectAudio;

    private GameObject _helpPanel;
    private GameObject _settingPanel;


    private void Start()
    {
        exitButton = transform.Find("BackButton").GetComponent<Button>();
        _helpPanel = transform.Find("HelpPanel").gameObject;
        _settingPanel = transform.Find("SettingPanel").gameObject;
    }

    private void Update()
    {
        if (Moving._playerState == Moving.PlayerState.INSETTING)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnClickExitButton();
            }
        }
    }

    public void OnClickExitButton()
    {
        Moving._playerState = Moving.PlayerState.IDLE;
        transform.DOLocalMoveX(1920, 0.2f);
        if(PlayerBehave.instance != null) PlayerBehave.instance._backGround.StartCoroutine("SpawnCoin");
    }

    public void OnClickSettingButton()
    {
        _helpPanel.SetActive(false);
        _settingPanel.SetActive(true);
    }

    public void OnClickHelpButton()
    {
        _settingPanel.SetActive(false);
        _helpPanel.SetActive(true);
    }

    public void AddBGBTN()
    {
        if (_backgroundAudio.volume >= 1) return;
        _backgroundAudio.volume += 0.1f;

        _soundFillImage[0].fillAmount += 0.1f;
    }

    public void DECBGBTN()
    {
        if (_backgroundAudio.volume <= 0) return;
        _backgroundAudio.volume -= 0.1f;

        _soundFillImage[0].fillAmount -= 0.1f;
    }

    public void AddEffectBTN()
    {
        if (_effectAudio.volume >= 1) return;
        _effectAudio.volume += 0.1f;

        _soundFillImage[1].fillAmount += 0.1f;
    }

    public void DECEffectBTN()
    {
        if (_effectAudio.volume <= 0) return;
        _effectAudio.volume -= 0.1f;

        _soundFillImage[1].fillAmount -= 0.1f;
    }
}
