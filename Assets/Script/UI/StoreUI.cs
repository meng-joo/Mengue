using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class StoreUI : MonoBehaviour
{
    public Button[] upgradePanel;
    public Button[] skillPanel;
    private TextMeshProUGUI[] lvText = new TextMeshProUGUI[5];
    private List<Text> priceText = new List<Text>();
    //private int[] skillLv;
    private int[] price = new int[5];
    private int[] skillLevel = new int[5];
    private TextMeshProUGUI[] skillPricetext = new TextMeshProUGUI[4];

    public TextMeshProUGUI _storeBuyPanelText;

    public BackGround _backGround = null;

    public int[] skillPrice = new int[4];
    private bool _ischatting;

    public RandomGacha _randomGacha;

    public int passive_Sale = 1;

    void Start()
    {
        SetPrice();
        SetLevel();
    }

    public void SetLevel()
    {
        for (int i = 0; i < upgradePanel.Length; i++)
        {
            lvText[i] = upgradePanel[i].transform.Find("LvText").GetComponent<TextMeshProUGUI>();
            skillLevel[i] = 0;
        }
    }

    public void SetPrice()
    {
        for (int i = 0; i < upgradePanel.Length; i++)
        {
            //Debug.Log(upgradePanel[i].transform.Find("GoldText").GetComponent<Text>().text);
            priceText.Add(upgradePanel[i].transform.Find("GoldText").GetComponent<Text>());
            price[i] = 10;
        }

        for (int i = 0; i < skillPanel.Length; i++)
        {
            skillPricetext[i] = skillPanel[i].transform.Find("PriceText").GetComponent<TextMeshProUGUI>();
        }
        skillPrice[0] = 5;
        skillPrice[1] = 7;
        skillPrice[2] = Mathf.RoundToInt(Mathf.Max(4000, Moving.moneyValue * 400f));
        skillPrice[3] = 150;
    }

    public void BuySkills(int num)
    {
        AblingButtons(false);
        if (Moving.currentMoney < skillPrice[num] / passive_Sale) { StartCoroutine(ShowStoreBehave("���� �����մϴ�")); return; }
        if (Moving.playerHealth == Moving.playerCurrentHealth && num == 3) { StartCoroutine(ShowStoreBehave("�̹� �ִ�ü���Դϴ�.")); return; }
        if (_randomGacha.count > 4 && num == 2) { StartCoroutine(ShowStoreBehave("�������� ����á���ϴ�.")); return; }

        Moving.currentMoney -= skillPrice[num] / passive_Sale;

        if(num == 3)
        {
            StartCoroutine(ShowStoreBehave($"ü���� {Mathf.Min(Moving.playerCurrentHealth + 15, Moving.playerHealth)}��ŭ ȸ���Ͽ����ϴ�."));
            Moving.playerCurrentHealth = Mathf.Min(Moving.playerCurrentHealth + 15, Moving.playerHealth);
        }

        else if(num == 2)
        {
            Moving._isGacha = true;
            
            
            _randomGacha.SetPassiveItem();

            StartCoroutine(ShowStoreBehave($"���� �̱�!"));
        }
    }

    public void SetStoreUI()
    {
        Sequence seq = DOTween.Sequence();

        Moving._isStoresetting = true;

        skillPrice[2] = Mathf.RoundToInt(Mathf.Max(4000, Moving.moneyValue * 400f));
        skillPricetext[2].text = string.Format($"$ {skillPrice[2] / passive_Sale}");

        seq.Append(transform.DOMoveY(520, 0.4f));
        for (int i = 0; i < upgradePanel.Length; i++)
        {
            seq.Append(upgradePanel[i].transform.DOMoveY(750, 0.1f));
        }

        seq.Append(skillPanel[0].transform.DOLocalMoveX(-640, 0.14f));
        seq.Append(skillPanel[1].transform.DOLocalMoveX(-640, 0.14f));

        seq.Append(skillPanel[3].transform.DOLocalMoveX(90, 0.14f));
        seq.Append(skillPanel[2].transform.DOLocalMoveX(90, 0.14f));

        seq.AppendCallback(() => Moving._isStoresetting = false);

        StartCoroutine(ShowStoreBehave($"�������~~~!"));
    }

    public void GetBackStoreUI()
    {
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < upgradePanel.Length; i++)
        {
            seq.Append(upgradePanel[i].transform.DOMoveY(1400, 0.14f));
        }

        seq.Append(skillPanel[0].transform.DOLocalMoveX(-1270, 0.2f));
        seq.Append(skillPanel[1].transform.DOLocalMoveX(-1270, 0.2f));

        seq.Append(skillPanel[3].transform.DOLocalMoveX(716, 0.14f));
        seq.Append(skillPanel[2].transform.DOLocalMoveX(716, 0.14f));

        seq.Append(transform.DOMoveY(1600, 0.4f));
        seq.AppendCallback(() => Moving._playerState = Moving.PlayerState.IDLE);
    }

    public void SelectUpgradePanel(int num)
    {
        AblingButtons(false);
        if(Moving.currentMoney < price[num])
        {
            StartCoroutine(ShowStoreBehave("���� ���ڶ��ϴ�!!"));
            return;
        }

        Moving.currentMoney -= price[num];
        price[num] += Mathf.RoundToInt(price[num] * 0.62f);
        skillLevel[num]++;
        lvText[num].text = string.Format($"Lv.{skillLevel[num]}");
        priceText[num].text = string.Format($"$ {price[num]}");
        skillPrice[2] = Mathf.RoundToInt(Mathf.Max(4000, Moving.moneyValue * 400f));
        skillPricetext[2].text = string.Format($"$ {skillPrice[2] / passive_Sale}");
        UpgradeState(num);
    }

    public void UpgradeState(int num)
    {
        if (num == 0) { PlayerBehave.playerAttack += 2; StartCoroutine(ShowStoreBehave("���ݷ��� 2�ö󰬽��ϴ�.")); }
        else if (num == 1) { PlayerBehave.playerHealth += 10; PlayerBehave.playerCurrentHealth += 10; StartCoroutine(ShowStoreBehave("ü���� 10�ö󰬽��ϴ�.")); }
        else if (num == 2) { PlayerBehave.moneyValue += 1; StartCoroutine(ShowStoreBehave("����ġ�� 1�ö󰬽��ϴ�.")); }
        else if (num == 3) { PlayerBehave.playerDefence += 2; StartCoroutine(ShowStoreBehave("������ 2�ö󰬽��ϴ�.")); }
        else if (num == 4)
        {
            Moving.enemyHealth += Mathf.RoundToInt(Moving.enemyHealth * 0.15f);
            Moving.enemyDefence += Mathf.RoundToInt(Moving.enemyDefence * 0.17f);
            Moving.enemyAttack += Mathf.RoundToInt(Moving.enemyAttack * 0.17f);
            Moving.enemyMoney += 1;
            StartCoroutine(ShowStoreBehave("���� ��ȭ�Ǿ����ϴ�."));
            if (skillLevel[num] % 11 == 10)
            {
                _backGround.CreateEnemy();
            }
        }

        StartCoroutine(SetIdle(1.1f));
    }

    IEnumerator SetIdle(float value)
    {
        yield return new WaitForSeconds(value);
        if (!_ischatting)
        {
            StartCoroutine(ShowStoreBehave("��� �����Ű���? "));
        }
    }

    public void AblingButtons(bool _is)
    {
        for (int i = 0; i < upgradePanel.Length; i++)
        {
            upgradePanel[i].enabled = _is;
        }

        for (int i = 0; i < skillPanel.Length; i++)
        {
            skillPanel[i].enabled = _is;
        }
    }


    IEnumerator ShowStoreBehave(string text)
    {
        _ischatting = true;
        for (int i = 0; i < text.Length; i++)
        {
            _storeBuyPanelText.text = string.Format(text.Substring(0, i));
            yield return new WaitForSeconds(0.05f);
        }
        _ischatting = false;

        AblingButtons(true);
    }
}