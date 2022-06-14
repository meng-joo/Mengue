using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StoreUI : MonoBehaviour
{
    public Button[] upgradePanel;
    private List<Text> priceText = new List<Text>();
    private int[] skillLv;
    private int[] price = new int[4];
    
    void Start()
    {
        SetPrice();
    }

    public void SetPrice()
    {
        for (int i = 0; i < upgradePanel.Length; i++)
        {
            Debug.Log(upgradePanel[i].transform.Find("GoldText").GetComponent<Text>().text);
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
        price[num] += Mathf.RoundToInt(price[num] * 0.25f);
        priceText[num].text = string.Format($"$ {price[num]}");
    }
}