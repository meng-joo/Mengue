using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class SkillUI : MonoBehaviour
{
    private Image skillUI;
    private Image skillPanel;
    public Animator playerBehave;
    private TextMeshProUGUI _stateText;
    private TextMeshProUGUI _runAwayCost;
    private TextMeshProUGUI[] _skillCountText = new TextMeshProUGUI[2];
    public Enemy _fightingEnemy = null;
    public Boss _fightingBoss = null;
    public BackGround _backGround;
    public StoreUI _storeUI;
    public StateUI _stateUI;

    public GameObject[] buttons;

    private int[] _skillCount = new int[2];

    private bool _isAttaking = false;

    public int runvalue;

    public int skillLimite = 14;

    private void Start()
    {
        skillUI = GetComponent<Image>();
        _stateText = transform.GetChild(0).Find("Text").GetComponent<TextMeshProUGUI>();
        _runAwayCost = transform.GetChild(3).Find("PenaltyText").GetComponent<TextMeshProUGUI>();
        skillPanel = transform.Find("SkillPanel").GetComponent<Image>();
        _skillCountText[0] = transform.Find("SkillPanel/AttackSkill/CountSkillText").GetComponent<TextMeshProUGUI>();
        _skillCountText[1] = transform.GetChild(4).Find("HealSkill/CountSkillText").GetComponent<TextMeshProUGUI>();
        _skillCount[0] = 10;
        _skillCount[1] = 3;
        runvalue = 2;
    }

    public void SetEnemy(GameObject _enemy)
    {
        _fightingEnemy = _enemy.GetComponent<Enemy>();
        if (_fightingEnemy == null) _fightingBoss = _enemy.GetComponent<Boss>();
    }

    public void ViewSkillUI()
    {
        Sequence sequence = DOTween.Sequence();
        _runAwayCost.text = string.Format($"-{Mathf.RoundToInt(Mathf.Max(Moving.currentMoney / runvalue, 1))}$");
        sequence.Append(skillUI.transform.DOLocalMoveY(-354f, 1));
        StartCoroutine(WriteText("당신은 적(을)를 만났습니다!"));
        //다른 스킬을 더 띄어주기 위해 시퀀스 구현해야함
    }

    public void HideSkillUI()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(0.4f);
        sequence.Append(skillUI.transform.DOLocalMoveY(-730, 1));
        //다른 스킬을 더 띄어주기 위해 시퀀스 구현해야함
    }

    public void AttackButton()
    {
        if (!_isAttaking && _skillCount[0] > 0)
        {
            StartCoroutine(HideButtons());
            playerBehave.SetTrigger("Attack");
            if (_fightingEnemy != null) _fightingEnemy.GetAttack();
            else _fightingBoss.GetAttack();
            EnemyTurn();
            _skillCount[0]--;
            SetCountText();
        }
        //Dotext가 텍스트 메쉬 프로에 없어...
        else
        {
            if (_skillCount[0] <= 0)
            {
                StartCoroutine(WriteText("스킬을 다 소모하였습니다."));
            }
            else if(!Moving._isPlayerTurn)
            {
                StartCoroutine(WriteText("아직 상대 턴입니다."));
            }
        }
    }

    public void BackButton()
    {
        skillPanel.transform.DOLocalMoveX(1250, 0.27f);
    }

    public void EnemyStateButton()
    {
        if (_fightingEnemy != null) StartCoroutine(WriteText($"이름: 흥행행\n체력: {Moving.enemycurrnetHealth}/{Moving.enemyHealth}\n공격력: {Moving.enemyAttack}\n방어력: {Moving.enemyDefence}\n특징: 어쩔티비 "));
        else StartCoroutine(WriteText($"이름: 뽀스\n체력: {Moving.bosscurrnetHealth}/{Moving.bossHealth}\n공격력: {Moving.bossAttack}\n방어력: {Moving.enemyDefence}\n특징: 꽤 쎄다 "));
    }

    public void InputSkillButton()
    {
        if(!Moving._isPlayerTurn)
        {
            StartCoroutine(WriteText("아직 상대 턴입니다."));
        }
        else
        {
            SetCountText();
            skillPanel.transform.DOLocalMoveX(670, 0.4f);
        }
    }

    private void EnemyTurn()
    {
        BackButton();
    }

    public void OtherWriteText(string text)
    {
        StartCoroutine(WriteText(text));
    }

    public IEnumerator WriteText(string text)
    {
        //_stateText.text = string.Format(text);
        //yield return null;
        for (int i = 0; i < text.Length; i++)
        {
            //Debug.Log("기ㅣㅁ필규");
            _stateText.text = string.Format("{0}", text.Substring(0, i));
            yield return new WaitForSeconds(0.037f);
        }
    }


    public void HealButton()
    {
        if (!_isAttaking && _skillCount[1] > 0)
        {
            StartCoroutine(HideButtons());
            playerBehave.SetTrigger("Heal");
            GameObject effect = Instantiate(PlayerBehave.instance._healEffect);
            effect.transform.position = PlayerBehave.instance.transform.position;
            StartCoroutine(WriteText($"체력을 {Mathf.Min(7, Moving.playerAddHealth - Moving.playerCurrentHealth)}회복 하였습니다."));
            Moving.playerCurrentHealth = Mathf.Min(Moving.playerCurrentHealth + 7, Moving.playerAddHealth);
            _stateUI.UpdateStateText();
            if (_fightingEnemy != null) _fightingEnemy.StartCoroutine("EnemyAttack");
            else _fightingBoss.StartCoroutine("EnemyAttack");
            EnemyTurn();
            _skillCount[1]--;
            SetCountText();
        }
        else
        {
            if (_skillCount[1] <= 0)
            {
                StartCoroutine(WriteText("스킬을 다 소모하였습니다."));
            }
            else if (!Moving._isPlayerTurn)
            {
                StartCoroutine(WriteText("아직 상대 턴입니다."));
            }
        }
    }

    public void RunAwayButton()
    {
        if (Moving._isPlayerTurn)
        {
            if (_fightingEnemy != null)
            {
                StartCoroutine(WriteText("도망가~~! "));
                Moving.currentMoney -= Mathf.RoundToInt(Mathf.Max(Moving.currentMoney / runvalue, 0));
                _backGround.CreateEnemy();
                Moving.enemycurrnetHealth = Moving.enemyHealth;
                Moving._playerState = Moving.PlayerState.IDLE;
                PlayerBehave.instance.EndBattle();
                Destroy(_fightingEnemy.gameObject);
                _fightingEnemy = null;
            }
            else
            {
                StartCoroutine(WriteText("도망가~~! "));
                Moving.currentMoney -= Mathf.RoundToInt(Mathf.Max(Moving.currentMoney / runvalue, 0));
                _backGround.CreateBoss();
                Moving.bosscurrnetHealth = Moving.bossHealth;
                Moving._playerState = Moving.PlayerState.IDLE;
                PlayerBehave.instance.EndBattle();
                Destroy(_fightingBoss.gameObject);
                _fightingBoss = null;
            }
        }
        else
        {
            StartCoroutine(WriteText("아직 상대 턴입니다."));
        }
    }

    public void BuySkills(int num)
    {
        _storeUI.AblingButtons(false);

        if (Moving.currentMoney < _storeUI.skillPrice[num] / _storeUI.passive_Sale) { _storeUI.StartCoroutine("ShowStoreBehave", "돈이 부족해요."); return; }
        if (_skillCount[num] > skillLimite) { _storeUI.StartCoroutine("ShowStoreBehave", $"스킬은 최대 {skillLimite}개까지 살수 있습니다."); return; }

        Moving.currentMoney -= _storeUI.skillPrice[num] / _storeUI.passive_Sale;
        _skillCount[num] += 1;

        
        _storeUI.StartCoroutine("ShowStoreBehave", "스킬을 구매하였습니다.");
    }

    private void SetCountText()
    {
        _skillCountText[0].text = string.Format($"{_skillCount[0]} X");
        _skillCountText[1].text = string.Format($"{_skillCount[1]} X");
    }

    public IEnumerator HideButtons()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.DOMoveX(2190, 0.14f);
            yield return new WaitForSeconds(0.08f);
        }
    }

    public IEnumerator ShowButtons()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.DOMoveX(1920 - 230, 0.12f);
            yield return new WaitForSeconds(0.08f);
        }
    }
}
