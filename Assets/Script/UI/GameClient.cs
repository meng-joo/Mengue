using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameClient : MonoBehaviour
{
    public TextMeshProUGUI _mengue;
    public Image consoleImage;
    public Button _startbutton;
    public Button _exitbutton;
    string _mengueText = "MENGUE ";

    public void StartGame()
    {
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuart);
        StartCoroutine(ActiveUI());
    }

    IEnumerator ActiveUI()
    {
        consoleImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < _mengueText.Length; i++)
        {
            _mengue.text = string.Format("{0}", _mengueText.Substring(0, i));
            yield return new WaitForSeconds(0.4f);
        }

        _startbutton.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _exitbutton.gameObject.SetActive(true);
    }

    public void ClickStart()
    {
        SceneManager.LoadScene("MainGame");
    }
}
