using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class StartMengueTitle : MonoBehaviour
{
    public TextMeshProUGUI[] titleText;
    public GameClient _gameClient;

    public GameObject windowPanel;
    float _isWindowOpen = 3f;

    private void Start()
    {
        for (int i = 0; i < titleText.Length; i++)
        {
            titleText[i] = transform.GetChild(i).GetComponent<TextMeshProUGUI>();
        }
    }

    public void InputFileButton()
    {

    }

    public void InputWindowButton()
    {
        
        windowPanel.transform.DOMoveY(-5 + _isWindowOpen, 0.4f);
        _isWindowOpen *= -1;
    }

    public void StartGame()
    {
        StartCoroutine(SetMengTitle());
    }

    IEnumerator SetMengTitle()
    {
        Sequence seq = DOTween.Sequence();

        yield return new WaitForSeconds(0.7f);

        for (int i = 0; i < titleText.Length; i++)
        {
            seq.Append(titleText[i].transform.DOMoveY(0, 0.7f)).SetEase(Ease.InCirc);
            seq.Append(titleText[i].transform.DOShakeScale(1f, 0.26f));
            
            //seq.Append(titleText[i].transform.DOShakeScale(1f, 0.26f));
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < titleText.Length; i++)
        {
            titleText[i].gameObject.SetActive(false);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.7f);

        _gameClient.StartGame();
    }
}
