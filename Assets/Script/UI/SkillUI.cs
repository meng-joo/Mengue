using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class SkillUI : MonoBehaviour
{
    //private Image skillUI;
    //private Image skillPanel;
    //public Animator playerBehave;
    //private TextMeshProUGUI _stateText;
    //private TextMeshProUGUI _runAwayCost;
    //private TextMeshProUGUI[] _skillCountText = new TextMeshProUGUI[2];
    //public CommonEnemy _fightingEnemy = null;
    //public Boss _fightingBoss = null;
    //public Setting _backGround;
    //public StoreUI _storeUI;
    //public StateUI _stateUI;

    

    //public AudioSource _audioSource;
    //public SoundClips _soundClips;

    //public GameObject[] buttons;

    //public int[] _skillCount = new int[2];

    //private bool _isAttaking = false;

    //public int runvalue;

    //public int skillLimite;

    //private void Start()
    //{
    //    skillUI = GetComponent<Image>();
    //    _stateText = transform.GetChild(0).Find("Text").GetComponent<TextMeshProUGUI>();
    //    _runAwayCost = transform.GetChild(3).Find("PenaltyText").GetComponent<TextMeshProUGUI>();
    //    skillPanel = transform.Find("SkillPanel").GetComponent<Image>();
    //    _skillCountText[0] = transform.Find("SkillPanel/AttackSkill/CountSkillText").GetComponent<TextMeshProUGUI>();
    //    _skillCountText[1] = transform.GetChild(4).Find("HealSkill/CountSkillText").GetComponent<TextMeshProUGUI>();
    //    _skillCount[0] = 10;
    //    _skillCount[1] = 3;
    //    skillLimite = 14;
    //    runvalue = 2;
    //}

    //public void SetEnemy(GameObject _enemy)
    //{
    //    _fightingEnemy = _enemy.GetComponent<CommonEnemy>();
    //    if (_fightingEnemy == null) _fightingBoss = _enemy.GetComponent<Boss>();
    //    StartCoroutine("ShowButtons");
    //}

    //public void ViewSkillUI()
    //{
    //    Sequence sequence = DOTween.Sequence();
    //    _runAwayCost.text = string.Format($"-{Mathf.RoundToInt(Mathf.Max(GameManager.currentMoney / runvalue, 1))}$");
    //    sequence.Append(skillUI.transform.DOLocalMoveY(-354f, 1.3f));
    //    StopCoroutine("WriteText");
    //    StartCoroutine("WriteText", "당신은 적(을)를 만났습니다!");
    //    //다른 스킬을 더 띄어주기 위해 시퀀스 구현해야함
    //}

    //public void HideSkillUI()
    //{
    //    Sequence sequence = DOTween.Sequence();

    //    sequence.AppendInterval(0.8f);
    //    sequence.Append(skillUI.transform.DOLocalMoveY(-800, 0.9f));
    //    //다른 스킬을 더 띄어주기 위해 시퀀스 구현해야함
    //}

    //public void AttackButton()
    //{
    //    if (!_isAttaking && _skillCount[0] > 0)
    //    {
    //        StartCoroutine(HideButtons());
    //        playerBehave.SetTrigger("Attack");
    //        if (_fightingEnemy != null) _fightingEnemy.GetAttack();
    //        else _fightingBoss.GetAttack();
    //        EnemyTurn();
    //        _skillCount[0]--;
    //        SetCountText();
    //    }
    //    //Dotext가 텍스트 메쉬 프로에 없어...
    //    else
    //    {
    //        if (_skillCount[0] <= 0)
    //        {
    //            StartCoroutine("WriteText");
    //            StartCoroutine("WriteText", "스킬을 다 소모하였습니다.");
    //        }
    //        else if(!GameManager._isPlayerTurn)
    //        {
    //            StopCoroutine("WriteText");
    //            StartCoroutine("WriteText", "아직 상대 턴입니다.");
    //        }
    //    }
    //}

    //public void BackButton()
    //{
    //    skillPanel.transform.DOLocalMoveX(1250, 0.27f);
    //}

    //public void EnemyStateButton()
    //{
    //    if (_fightingEnemy != null) { StopCoroutine("WriteText"); StartCoroutine(WriteText($"이름: 흥행행\n체력: {_fightingEnemy.enemycurrnetHealth}/{_fightingEnemy.commonEnemyData.enemyHealth}\n공격력: {_fightingEnemy.commonEnemyData.enemyAttack}\n방어력: {_fightingEnemy.commonEnemyData.enemyDefence}\n특징: 평범하게 생겼다. ")); }
    //    else { StopCoroutine("WriteText"); StartCoroutine("WriteText", $"이름: 뽀스\n체력: {_fightingBoss.bosscurrnetHealth}/{_fightingBoss.bossData.enemyHealth}\n공격력: {_fightingBoss.bossData.enemyAttack}\n방어력: {_fightingBoss.bossData.enemyDefence}\n특징: 꽤 쎄다 "); }
    //}

    //public void InputSkillButton()
    //{
    //    if(!GameManager._isPlayerTurn)
    //    {
    //        StopCoroutine("WriteText");
    //        StartCoroutine("WriteText", "아직 상대 턴입니다.");
    //    }
    //    else
    //    {
    //        SetCountText();
    //        StopCoroutine("WriteText");
    //        StartCoroutine("WriteText", "무엇을 하시겠습니까? ");
    //        skillPanel.transform.DOLocalMoveX(670, 0.4f);
    //    }
    //}

    //private void EnemyTurn()
    //{
    //    BackButton();
    //}

    //public void OtherWriteText(string text)
    //{
    //    StopCoroutine("WriteText");
    //    StartCoroutine("WriteText", text);
    //}

    //public IEnumerator WriteText(string text)
    //{
    //    for (int i = 0; i < text.Length; i++)
    //    {
    //        _stateText.text = string.Format("{0}", text.Substring(0, i));
    //        yield return new WaitForSeconds(0.037f);
    //    }
    //}

    //public void HealButton()
    //{
    //    if (!_isAttaking && _skillCount[1] > 0)
    //    {
    //        StartCoroutine(HideButtons());
    //        playerBehave.SetTrigger("Heal");
    //        GameObject effect = Instantiate(PlayerBehave.instance._healEffect);
    //        effect.transform.position = PlayerBehave.instance.transform.position;
    //        StopCoroutine("WriteText");
    //        StartCoroutine("WriteText", $"체력을 {Mathf.Min(7, GameManager.playerAddHealth - GameManager.playerCurrentHealth)}회복 하였습니다.");
    //        GameManager.playerCurrentHealth = Mathf.Min(GameManager.playerCurrentHealth + 7, GameManager.playerAddHealth);
    //        _stateUI.UpdateStateText();
    //        if (_fightingEnemy != null) _fightingEnemy.StartCoroutine("EnemyAttack", 0);
    //        else _fightingBoss.StartCoroutine("EnemyAttack", 0);
    //        EnemyTurn();
    //        _skillCount[1]--;
    //        SetCountText();
    //    }
    //    else
    //    {
    //        if (_skillCount[1] <= 0)
    //        {
    //            StopCoroutine("WriteText");
    //            StartCoroutine("WriteText", "스킬을 다 소모하였습니다.");
    //        }
    //        else if (!GameManager._isPlayerTurn)
    //        {
    //            StopCoroutine("WriteText");
    //            StartCoroutine("WriteText", "아직 상대 턴입니다.");
    //        }
    //    }
    //}

    //public void RunAwayButton()
    //{
    //    if (GameManager._isPlayerTurn)
    //    {
    //        if (_fightingEnemy != null)
    //        {
    //            StopCoroutine("WriteText");
    //            StartCoroutine("WriteText", "도망가~~! ");
    //            GameManager.currentMoney -= Mathf.RoundToInt(Mathf.Max(GameManager.currentMoney / runvalue, 0));
    //            _backGround.CreateEnemy();
    //            GameManager._playerState = GameManager.PlayerState.IDLE;
    //            PlayerBehave.instance.EndBattle();
    //            Destroy(_fightingEnemy.gameObject);
    //            _fightingEnemy = null;
    //        }
    //        else
    //        {
    //            StopCoroutine("WriteText");
    //            StartCoroutine("WriteText", "도망가~~! ");
    //            GameManager.currentMoney -= Mathf.RoundToInt(Mathf.Max(GameManager.currentMoney / runvalue, 0));
    //            _backGround.CreateBoss();
    //            GameManager._playerState = GameManager.PlayerState.IDLE;
    //            PlayerBehave.instance.EndBattle();
    //            Destroy(_fightingBoss.gameObject);
    //            _fightingBoss = null;
    //        }
    //    }
    //    else
    //    {
    //        StartCoroutine(WriteText("아직 상대 턴입니다."));
    //    }
    //    _stateUI.UpdateStateText();
    //}

    //public void BuySkills(int num)
    //{
    //    _storeUI.AblingButtons(false);

    //    if (GameManager.currentMoney < _storeUI.skillPrice[num] / _storeUI.passive_Sale && !GameManager.passive_TheKing) { _storeUI.StartCoroutine("ShowStoreBehave", "돈이 부족해요."); return; }
    //    if (_skillCount[num] >= skillLimite) { _storeUI.StartCoroutine("ShowStoreBehave", $"스킬은 최대 {skillLimite}개까지 살수 있습니다."); return; }


    //    if (!GameManager.passive_TheKing)
    //    {
    //        GameManager.currentMoney -= _storeUI.skillPrice[num] / _storeUI.passive_Sale;
    //        _storeUI.StartCoroutine("ShowStoreBehave", "스킬을 구매하였습니다.");
    //    }
    //    else
    //    {
    //        _storeUI.StartCoroutine("ShowStoreBehave", "'왕은 돈을 내지 않습니다' ");
    //    }

    //    _skillCount[num] += 1;
    //    _storeUI._skillCountText[num].text = string.Format("{0}/{1}", _skillCount[num], skillLimite);
    //    _storeUI._playerCoin.text = string.Format("{0}＄", GameManager.currentMoney);
    //}

    //private void SetCountText()
    //{
    //    _skillCountText[0].text = string.Format($"{_skillCount[0]} X");
    //    _skillCountText[1].text = string.Format($"{_skillCount[1]} X");
    //}

    //public IEnumerator HideButtons()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    for (int i = 0; i < buttons.Length; i++)
    //    {
    //        buttons[i].transform.DOLocalMoveX(1800, 0.18f);
    //        yield return new WaitForSeconds(0.04f);
    //    }
    //}

    //public IEnumerator ShowButtons()
    //{
    //    yield return new WaitForSeconds(0.2f);
        
    //    for (int i = 0; i < buttons.Length; i++)
    //    {
    //        buttons[i].transform.DOLocalMoveX(680, 0.12f);
    //        yield return new WaitForSeconds(0.08f);
    //    }
    //}
}
