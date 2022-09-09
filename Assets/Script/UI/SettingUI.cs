using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using DG.Tweening;

public class SettingUI : MonoBehaviour
{
    private Button exitButton;
    float sound;
    [Range(-40, 0)]
    [SerializeField] float currentBGMSound;
    [Range(-40, 0)]
    [SerializeField] float currentEFTSound;
    

    public AudioMixer masterMixer;

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
        if (PlayerBehave._playerState == PlayerBehave.PlayerState.INSETTING)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
            {
                OnClickExitButton();
            }
        }
    }

    public void OnClickExitButton()
    {
        PlayerBehave._playerState = PlayerBehave.PlayerState.IDLE;
        transform.DOLocalMoveX(1920, 0.2f);
        if(PlayerBehave.instance != null) PlayerBehave.instance._setting.StartCoroutine("SpawnCoin");
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
        if (sound > 0)
        {
            return;
        }
        else
        {
            _soundFillImage[0].fillAmount += 1 / 8f;
            sound = -40 + _soundFillImage[0].fillAmount * 40;
            masterMixer.SetFloat("Background", sound);
        }

        //_soundFillImage[0].fillAmount += 1 / 8f;

        //sound = -40 + _soundFillImage[0].fillAmount * 40;

        //if (masterMixer.SetFloat("Background", -80))
        //{
        //    masterMixer.SetFloat("Background", -40);
        //}

        //masterMixer.SetFloat("Background", sound);
    }

    public void DECBGBTN()
    {
        if (sound < -40)
        {
            masterMixer.SetFloat("Background", -80);
            return;
        }
        else
        {
            _soundFillImage[0].fillAmount -= 1 / 8f;
            sound = -40 + _soundFillImage[0].fillAmount * 40;
            masterMixer.SetFloat("Background", sound);
        }
    }

    public void AddEffectBTN()
    {
        if (sound > 0)
        {
            return;
        }
        else
        {
            _soundFillImage[1].fillAmount += 1 / 8f;
            sound = -40 + _soundFillImage[1].fillAmount * 40;
            masterMixer.SetFloat("Effect", sound);
        }
    }

    public void DECEffectBTN()
    {
        if (sound < -40)
        {
            masterMixer.SetFloat("Effect", -80);
            return;
        }
        else
        {
            _soundFillImage[1].fillAmount -= 1 / 8f;
            sound = -40 + _soundFillImage[1].fillAmount * 40;
            masterMixer.SetFloat("Effect", sound);
        }
    }
}
