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
    public int[] price = new int[5];
    private int[] skillLevel = new int[5];
    private TextMeshProUGUI[] skillPricetext = new TextMeshProUGUI[4];

    public TextMeshProUGUI _playerCoin;

    public TextMeshProUGUI _storeBuyPanelText;

    public BackGround _backGround = null;

    public int[] skillPrice = new int[4];
    private bool _ischatting;

    public RandomGacha _randomGacha;

    public int passive_Sale = 1;
    public TextMeshProUGUI[] _skillCountText = new TextMeshProUGUI[2];

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
        skillPrice[2] = Mathf.RoundToInt(Mathf.Max(1200, Moving.moneyValue * Moving.moneyValue * 3.6f));
        skillPrice[3] = 150;

        _skillCountText[0] = skillPanel[0].transform.Find("SkillCount").GetComponent<TextMeshProUGUI>();
        _skillCountText[1] = skillPanel[1].transform.Find("SkillCount").GetComponent<TextMeshProUGUI>();
        _skillCountText[0].text = string.Format("{0}/{1}", 10, 14);
        _skillCountText[1].text = string.Format("{0}/{1}", 3, 14);
    }

    public void BuySkills(int num)
    {
        AblingButtons(false);
        if (Moving.currentMoney < skillPrice[num] / passive_Sale && !Moving.passive_TheKing) { SetText("돈이 부족합니다 "); return; }
        if (Moving.playerCurrentHealth == Moving.playerAddHealth && num == 3) { SetText("이미 최대체력입니다."); return; }
        if (_randomGacha.count > 4 && num == 2) { SetText("아이템이 가득찼습니다."); return; }

        if (!Moving.passive_TheKing)
        {
            Moving.currentMoney -= skillPrice[num] / passive_Sale;
            _playerCoin.text = string.Format("{0}＄", Moving.currentMoney);
            if (num == 3)
            {
                SetText($"체력을 {Mathf.Min(Moving.playerCurrentHealth + 15, Moving.playerAddHealth)}만큼 회복하였습니다.");
                Moving.playerCurrentHealth = Mathf.Min(Moving.playerCurrentHealth + 15, Moving.playerAddHealth);
            }

            else if (num == 2)
            {
                Moving._isGacha = true;
                _randomGacha.SetPassiveItem();

                SetText($"랜덤 뽑기!");
            }
        }

        else
        {
            if (num == 3)
            {
                SetText($"'왕은 돈을 내지 않습니다' ");
                Moving.playerCurrentHealth = Mathf.Min(Moving.playerCurrentHealth + 15, Moving.playerAddHealth);
            }

            else if (num == 2)
            {
                Moving._isGacha = true;
                _randomGacha.SetPassiveItem();

                SetText($"랜덤 뽑기!");
            }
        }
    }

    public void SetStoreUI()
    {
        Sequence seq = DOTween.Sequence();

        Moving._isStoresetting = true;
        _playerCoin.text = string.Format("{0}＄", Moving.currentMoney);

        skillPrice[2] = Mathf.RoundToInt(Mathf.Max(1200, Moving.moneyValue * Moving.moneyValue * 3.6f));
        skillPricetext[2].text = string.Format($"$ {skillPrice[2] / passive_Sale}");

        seq.Append(transform.DOLocalMoveY(14, 0.4f));
        for (int i = 0; i < upgradePanel.Length; i++)
        {
            seq.Append(upgradePanel[i].transform.DOLocalMoveY(180, 0.1f));
        }

        seq.Append(skillPanel[0].transform.DOLocalMoveX(-540, 0.14f));
        seq.Append(skillPanel[1].transform.DOLocalMoveX(-540, 0.14f));

        seq.Append(skillPanel[3].transform.DOLocalMoveX(90, 0.14f));
        seq.Append(skillPanel[2].transform.DOLocalMoveX(90, 0.14f));

        seq.AppendCallback(() => Moving._isStoresetting = false);
        UpdatePriceText();

        SetText($"어서오세요~~~!");
    }

    public void GetBackStoreUI()
    {
        Sequence seq = DOTween.Sequence();

        SoundClips.instance.StartCoroutine("StartSound");

        for (int i = 0; i < upgradePanel.Length; i++)
        {
            seq.Append(upgradePanel[i].transform.DOLocalMoveY(830, 0.1f));
        }

        seq.Append(skillPanel[0].transform.DOLocalMoveX(-1240, 0.1f));
        seq.Append(skillPanel[1].transform.DOLocalMoveX(-1240, 0.1f));

        seq.Append(skillPanel[3].transform.DOLocalMoveX(716, 0.09f));
        seq.Append(skillPanel[2].transform.DOLocalMoveX(716, 0.08f));

        seq.Append(transform.DOLocalMoveY(960, 0.3f));
        seq.AppendCallback(() => Moving._playerState = Moving.PlayerState.IDLE);
    }

    public void SelectUpgradePanel(int num)
    {
        AblingButtons(false);
        if (Moving.currentMoney < price[num] / passive_Sale && !Moving.passive_TheKing)
        {
            SetText("돈이 모자랍니다!!");
            return;
        }

        if (skillLevel[num] > 150) { SetText("최대 레벨입니다."); return; }

        if(!Moving.passive_TheKing)
        {
            Moving.currentMoney -= price[num] / passive_Sale;
            price[num] += Mathf.RoundToInt(price[num] / passive_Sale * 0.45f);
            skillLevel[num]++;
            lvText[num].text = string.Format($"Lv.{skillLevel[num]}");
            priceText[num].text = string.Format($"$ {price[num]/ passive_Sale}");
            skillPrice[2] = Mathf.RoundToInt(Mathf.Max(1200, Moving.moneyValue * Moving.moneyValue * 3.6f));
            skillPricetext[2].text = string.Format($"$ {skillPrice[2] / passive_Sale}");
            _playerCoin.text = string.Format("{0}＄", Moving.currentMoney);
        }
        else
        {
            SetText($"'왕은 돈을 내지 않습니다' ");
            skillLevel[num]++;
            lvText[num].text = string.Format($"Lv.{skillLevel[num]}");
        }
        AblingButtons(true);
        UpgradeState(num);
    }

    public void UpgradeState(int num)
    {
        if (num == 0) { PlayerBehave.playerAttack += 2; SetText("공격력이 2올라갔습니다."); }
        else if (num == 1) { PlayerBehave.playerHealth += 10; PlayerBehave.playerCurrentHealth += 10; SetText("체력이 10올라갔습니다."); }
        else if (num == 2) { PlayerBehave.moneyValue += 1; SetText("돈가치가 1올라갔습니다."); }
        else if (num == 3) { PlayerBehave.playerDefence += 2; SetText("방어력이 2올라갔습니다."); }
        else if (num == 4)
        {
            Moving.enemyHealth += Mathf.RoundToInt(Moving.enemyHealth * 0.2f);
            Moving.enemyDefence += Mathf.RoundToInt(Moving.enemyDefence * 0.17f);
            Moving.enemyAttack += Mathf.RoundToInt(Moving.enemyAttack * 0.17f);
            Moving.enemyMoney += 1;

            Moving.bossHealth += Mathf.RoundToInt(Moving.enemyHealth * 0.38f);
            Moving.bossDefence += Mathf.RoundToInt(Moving.enemyDefence * 0.3f);
            Moving.bossAttack += Mathf.RoundToInt(Moving.enemyAttack * 0.4f);
            Moving.bossMoney += 3;

            SetText("적이 강화되었습니다.");
            if (skillLevel[num] % 11 == 10)
            {
                _backGround.CreateEnemy();
            }
            if(skillLevel[num] % 25 == 24)
            {
                _backGround.CreateBoss();
            }
        }

        StartCoroutine(SetIdle(2.5f));
        UpdatePriceText();
    }

    IEnumerator SetIdle(float value)
    {
        yield return new WaitForSeconds(value);
        if (!_ischatting)
        {
            if (!Moving.passive_TheKing) SetText("어떤게 좋으신가요? ");
            else SetText("'왕은 돈을 내지 않습니다' ");
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

    public void SetText(string text)
    {
        StopCoroutine("ShowStoreBehave");
        StartCoroutine("ShowStoreBehave", text);
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

    public void UpdatePriceText()
    {
        skillPrice[2] = Mathf.RoundToInt(Mathf.Max(1200, Moving.moneyValue * Moving.moneyValue * 3.6f));
        for (int i = 0; i < priceText.Count; i++)
        {
            priceText[i].text = string.Format($"$ {price[i] / passive_Sale}");
        }

        for(int i = 0;i< skillPanel.Length;i++)
        {
            skillPricetext[i].text = string.Format($"$ {skillPrice[i]/passive_Sale}");
        }
    }
}