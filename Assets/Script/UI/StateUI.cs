using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class StateUI : MonoBehaviour
{
    private TextMeshProUGUI _stateText;
    int range = 232;

    private Button _settingButton;
    public Image settingBackGround;

    private void Start()
    {
        _stateText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _settingButton = transform.Find("SettingButton").GetComponent<Button>();
    }

    void Update()
    {
        if (Moving._playerState != Moving.PlayerState.INSTORE)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                range *= -1;
                transform.DOLocalMoveX(-960 - range, 0.2f);
                UpdateStateText();
            }
        }
    }

    public void OnClickSettingButton()
    {
        settingBackGround.transform.DOLocalMoveX(0, 0.2f);
    }

    public void UpdateStateText()
    {
        _stateText.text = string.Format($"�̸�: ���ֿ�\nü��: {Moving.playerCurrentHealth}/{Moving.playerHealth}\n���ݷ�: {Moving.playerAttack}\n����: {Moving.playerDefence}\n��: ${Moving.currentMoney}\n�� ��ġ: {Moving.moneyValue}");

    }
}
