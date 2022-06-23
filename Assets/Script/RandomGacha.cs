using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomGacha : MonoBehaviour
{
    public TextMeshProUGUI skillExplanText;
    public PassiveButton[] passiveButtons;


    public List<RandomItemValue> items = new List<RandomItemValue>();
    public int total = 0;

    public Image fadeImage;
    public Image[] passiveItem;
    public int count = 0;

    public Button _showGachaItem;

    public Button _exitButton;
    
    public Sprite idleImage;
    public Image whiteImage;

    public string _name, _grade;

    public TextMeshProUGUI _itemName;
    public TextMeshProUGUI _itemGrade;

    private Color[] textColor = new Color[3];

    private void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            total += items[i].weight;
        }
    }

    public void SetPassiveItem()
    {
        StartCoroutine(StartGachaEffect());
    }

    IEnumerator StartGachaEffect()
    {
        fadeImage.gameObject.SetActive(true);

        Color alphha = fadeImage.color;
        while(alphha.a <= 1)
        {
            alphha.a += 0.02f;
            fadeImage.color = alphha;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1.2f);

        RandomItemValue temp = StartGacha();


        passiveButtons[count].itemData = temp;
        passiveItem[count].sprite = temp.passiveImage;
        _name = temp.skillName;
        _grade = temp._ItemRating.ToString().Trim();
        
        count++;

        _showGachaItem.image.sprite = idleImage;
        _showGachaItem.transform.DOLocalMoveY(0, 0.4f);
    }

    public RandomItemValue StartGacha()
    {
        int weight = 0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(total * Random.Range(0, 1f));

        for (int i = 0; i < items.Count; i++)
        {
            weight += items[i].weight;

            if (selectNum <= weight)
            {
                RandomItemValue temp = new RandomItemValue(items[i]);
                return temp;
            }
        }
        return null;
    }

    public void GachaEffect()
    {
        Color pal = whiteImage.color;
        pal.a = 0;

        Sequence seq = DOTween.Sequence();
        _showGachaItem.enabled = false;
        seq.Append(_showGachaItem.transform.DOShakePosition(2.3f, 30, 30));
        seq.Join(_showGachaItem.transform.DOScale(1.4f, 2));
        StartCoroutine(WhiteImage());
        seq.AppendCallback(() => 
        {
            _showGachaItem.image.sprite = passiveItem[count - 1].sprite;
            whiteImage.color = pal;

            whiteImage.gameObject.SetActive(false);
        });
        
        seq.Append(_showGachaItem.transform.DOScale(1, 0.1f));

        seq.AppendInterval(1f);
        seq.Append(_showGachaItem.transform.DOLocalMoveY(200, 0.3f));
        seq.AppendCallback(() => 
        {
            _itemName.gameObject.SetActive(true);
            _itemName.text = string.Format(_name + " ");
        });

        seq.AppendCallback(() =>
        {
            VertexGradient ver = _itemGrade.colorGradient;
            _itemGrade.gameObject.SetActive(true);

            if (_grade == "Bronze") 
            {
                ColorUtility.TryParseHtmlString("#4F2B15FF", out textColor[0]);
                ColorUtility.TryParseHtmlString("#BE6D2AFF", out textColor[1]);
                ColorUtility.TryParseHtmlString("#713115FF", out textColor[2]);
            }

            else if(_grade == "Silver")
            {
                ColorUtility.TryParseHtmlString("#6A6A6AFF", out textColor[0]);
                ColorUtility.TryParseHtmlString("#484848FF", out textColor[1]);
                ColorUtility.TryParseHtmlString("#1A1A1AFF", out textColor[2]);
            }

            else if(_grade == "Gold")
            {
                ColorUtility.TryParseHtmlString("#FFDA68FF", out textColor[0]);
                ColorUtility.TryParseHtmlString("#B08502FF", out textColor[1]);
                ColorUtility.TryParseHtmlString("#F1C53EFF", out textColor[2]);
            }

            else if(_grade == "Platinum")
            {
                ColorUtility.TryParseHtmlString("#2AEBFFFF", out textColor[0]);
                ColorUtility.TryParseHtmlString("#11AD89FF", out textColor[1]);
                ColorUtility.TryParseHtmlString("#40FF7FFF", out textColor[2]);
            }

            else if (_grade == "Meng")
            {
                ColorUtility.TryParseHtmlString("#FF5623FF", out textColor[0]);
                ColorUtility.TryParseHtmlString("#FF1B00FF", out textColor[1]);
                ColorUtility.TryParseHtmlString("#FF4800FF", out textColor[2]);
            }

            ver.topLeft = textColor[0];
            ver.bottomLeft = textColor[1];
            ver.bottomRight = textColor[2];

            _itemGrade.colorGradient = ver;

            _itemGrade.text = string.Format(_grade + " ");
        });

        seq.Append(_exitButton.transform.DOLocalMoveY(-403, 0.1f));
    }

    IEnumerator WhiteImage()
    {
        Color alphaa = whiteImage.color;
        whiteImage.gameObject.SetActive(true);
        while (alphaa.a <= 1)
        {
            alphaa.a += 0.01f;
            whiteImage.color = alphaa;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void GachaExitButton()
    {
        StartCoroutine(Exit());
    }

    IEnumerator Exit()
    {
        _showGachaItem.transform.DOLocalMoveY(-830, 0.18f);
        _exitButton.transform.DOLocalMoveY(-760, 0.1f);

        yield return new WaitForSeconds(1f);

        Color alphha = fadeImage.color;
        while (alphha.a >= 0)
        {
            alphha.a -= 0.02f;
            fadeImage.color = alphha;
            yield return new WaitForSeconds(0.01f);
        }
        Moving._isGacha = false;

        _itemName.text = string.Format(" ");
        _itemGrade.text = string.Format(" ");
        _showGachaItem.enabled = true;
        fadeImage.gameObject.SetActive(false);
    }
}
