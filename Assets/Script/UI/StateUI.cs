using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class StateUI : MonoBehaviour
{
    //private TextMeshProUGUI _stateText;
    //int range = 232;

    ////public Button _settingButton;
    //public Button[] passiveSkillButton;
    //public Image settingBackGround;

    ////public Button settingButton;
    //private void Start()
    //{
    //    _stateText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    //    //_settingButton = transform.Find("SettingButton").GetComponent<Button>();
    //    SetPassiveButton();
    //}

    //void Update()
    //{
    //    if (GameManager._playerState != GameManager.PlayerState.INSTORE)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Tab))
    //        {
    //            range *= -1;
    //            transform.DOLocalMoveX(-960 - range, 0.2f);
    //            UpdateStateText();
    //        }
    //    }
    //}

    //public void OnClickSettingButton()
    //{
    //    GameManager._playerState = GameManager.PlayerState.INSETTING;
    //    settingBackGround.transform.DOLocalMoveX(0, 0.2f);
    //    PlayerBehave.instance._backGround.StopCoroutine("SpawnCoin");
    //}

    //public void UpdateStateText()
    //{
    //    _stateText.text = string.Format($"�̸�: ���ֿ�\nü��: {GameManager.playerCurrentHealth}/{GameManager.playerAddHealth}\n���ݷ�: {GameManager.playerCurrentAttack}\n����: {GameManager.playerCurrentDefence}\n��: ${GameManager.currentMoney}\n�� ��ġ: {GameManager.moneyValue}");

    //}

    //public void SetPassiveButton()
    //{
    //    for(int i=0;i<passiveSkillButton.Length;i++)
    //    {
    //        passiveSkillButton[i] = transform.Find("PassiveBackground").GetChild(i).GetComponent<Button>();
    //    }
    //}
}
