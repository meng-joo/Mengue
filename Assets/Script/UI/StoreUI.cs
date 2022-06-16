using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class StoreUI : MonoBehaviour
{
    public Button[] upgradePanel;
    private TextMeshProUGUI[] lvText = new TextMeshProUGUI[5];
    private List<Text> priceText = new List<Text>();
    //private int[] skillLv;
    private int[] price = new int[5];
    private int[] skillLevel = new int[5];
    
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
    }

    
    void Update()
    {
        
    }

    public void SetStoreUI()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMoveY(520, 0.4f));
        for (int i = 0; i < upgradePanel.Length; i++)
        {
            seq.Append(upgradePanel[i].transform.DOMoveY(750, 0.2f));
        }
    }

    public void GetBackStoreUI()
    {
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < upgradePanel.Length; i++)
        {
            seq.Append(upgradePanel[i].transform.DOMoveY(1400, 0.2f));
        }

        seq.Append(transform.DOMoveY(1600, 0.4f));
    }

    public void SelectUpgradePanel(int num)
    {
        if(Moving.currentMoney < price[num])
        {
            return;
        }

        Moving.currentMoney -= price[num];
        price[num] += Mathf.RoundToInt(price[num] * 0.42f);
        skillLevel[num]++;
        lvText[num].text = string.Format($"Lv.{skillLevel}");
        priceText[num].text = string.Format($"$ {price[num]}");

        UpgradeState(num);
    }

    public void UpgradeState(int num)
    {
        if (num == 0) PlayerBehave.playerAttack += 2;
        else if (num == 1) PlayerBehave.playerCurrentHealth += 10;
        else if (num == 2) PlayerBehave.moneyValue += 1;
        else if (num == 3) PlayerBehave.playerDefence += 2;
        else if (num == 4)
        {
            Moving.enemycurrnetHealth += Mathf.RoundToInt(Moving.enemycurrnetHealth * 0.33f);
            Moving.enemyDefence += Mathf.RoundToInt(Moving.enemyDefence * 0.25f);
            Moving.enemyAttack += Mathf.RoundToInt(Moving.enemyAttack * 0.25f);
        }
    }
}