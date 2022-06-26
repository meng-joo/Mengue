using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingUI : MonoBehaviour
{
    private Button exitButton;

    private void Start()
    {
        exitButton = transform.Find("BackButton").GetComponent<Button>();
    }

    public void OnClickExitButton()
    {
        transform.DOLocalMoveX(1920, 0.2f);
        if(PlayerBehave.instance != null) PlayerBehave.instance._backGround.StartCoroutine("SpawnCoin");
    }
}
