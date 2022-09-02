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
    //    StartCoroutine("WriteText", "����� ��(��)�� �������ϴ�!");
    //    //�ٸ� ��ų�� �� ����ֱ� ���� ������ �����ؾ���
    //}

    //public void HideSkillUI()
    //{
    //    Sequence sequence = DOTween.Sequence();

    //    sequence.AppendInterval(0.8f);
    //    sequence.Append(skillUI.transform.DOLocalMoveY(-800, 0.9f));
    //    //�ٸ� ��ų�� �� ����ֱ� ���� ������ �����ؾ���
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
    //    //Dotext�� �ؽ�Ʈ �޽� ���ο� ����...
    //    else
    //    {
    //        if (_skillCount[0] <= 0)
    //        {
    //            StartCoroutine("WriteText");
    //            StartCoroutine("WriteText", "��ų�� �� �Ҹ��Ͽ����ϴ�.");
    //        }
    //        else if(!GameManager._isPlayerTurn)
    //        {
    //            StopCoroutine("WriteText");
    //            StartCoroutine("WriteText", "���� ��� ���Դϴ�.");
    //        }
    //    }
    //}

    //public void BackButton()
    //{
    //    skillPanel.transform.DOLocalMoveX(1250, 0.27f);
    //}

    //public void EnemyStateButton()
    //{
    //    if (_fightingEnemy != null) { StopCoroutine("WriteText"); StartCoroutine(WriteText($"�̸�: ������\nü��: {_fightingEnemy.enemycurrnetHealth}/{_fightingEnemy.commonEnemyData.enemyHealth}\n���ݷ�: {_fightingEnemy.commonEnemyData.enemyAttack}\n����: {_fightingEnemy.commonEnemyData.enemyDefence}\nƯ¡: ����ϰ� �����. ")); }
    //    else { StopCoroutine("WriteText"); StartCoroutine("WriteText", $"�̸�: �ǽ�\nü��: {_fightingBoss.bosscurrnetHealth}/{_fightingBoss.bossData.enemyHealth}\n���ݷ�: {_fightingBoss.bossData.enemyAttack}\n����: {_fightingBoss.bossData.enemyDefence}\nƯ¡: �� ��� "); }
    //}

    //public void InputSkillButton()
    //{
    //    if(!GameManager._isPlayerTurn)
    //    {
    //        StopCoroutine("WriteText");
    //        StartCoroutine("WriteText", "���� ��� ���Դϴ�.");
    //    }
    //    else
    //    {
    //        SetCountText();
    //        StopCoroutine("WriteText");
    //        StartCoroutine("WriteText", "������ �Ͻðڽ��ϱ�? ");
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
    //        StartCoroutine("WriteText", $"ü���� {Mathf.Min(7, GameManager.playerAddHealth - GameManager.playerCurrentHealth)}ȸ�� �Ͽ����ϴ�.");
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
    //            StartCoroutine("WriteText", "��ų�� �� �Ҹ��Ͽ����ϴ�.");
    //        }
    //        else if (!GameManager._isPlayerTurn)
    //        {
    //            StopCoroutine("WriteText");
    //            StartCoroutine("WriteText", "���� ��� ���Դϴ�.");
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
    //            StartCoroutine("WriteText", "������~~! ");
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
    //            StartCoroutine("WriteText", "������~~! ");
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
    //        StartCoroutine(WriteText("���� ��� ���Դϴ�."));
    //    }
    //    _stateUI.UpdateStateText();
    //}

    //public void BuySkills(int num)
    //{
    //    _storeUI.AblingButtons(false);

    //    if (GameManager.currentMoney < _storeUI.skillPrice[num] / _storeUI.passive_Sale && !GameManager.passive_TheKing) { _storeUI.StartCoroutine("ShowStoreBehave", "���� �����ؿ�."); return; }
    //    if (_skillCount[num] >= skillLimite) { _storeUI.StartCoroutine("ShowStoreBehave", $"��ų�� �ִ� {skillLimite}������ ��� �ֽ��ϴ�."); return; }


    //    if (!GameManager.passive_TheKing)
    //    {
    //        GameManager.currentMoney -= _storeUI.skillPrice[num] / _storeUI.passive_Sale;
    //        _storeUI.StartCoroutine("ShowStoreBehave", "��ų�� �����Ͽ����ϴ�.");
    //    }
    //    else
    //    {
    //        _storeUI.StartCoroutine("ShowStoreBehave", "'���� ���� ���� �ʽ��ϴ�' ");
    //    }

    //    _skillCount[num] += 1;
    //    _storeUI._skillCountText[num].text = string.Format("{0}/{1}", _skillCount[num], skillLimite);
    //    _storeUI._playerCoin.text = string.Format("{0}��", GameManager.currentMoney);
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
