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
    public Enemy _fightingEnemy;
    public BackGround _backGround;
    public StoreUI _storeUI;
    public StateUI _stateUI;

    public GameObject[] buttons;

    private int[] _skillCount = new int[2];

    private bool _isAttaking = false;

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
    }

    public void SetEnemy(GameObject _enemy)
    {
        _fightingEnemy = _enemy.GetComponent<Enemy>();
    }

    public void ViewSkillUI()
    {
        Sequence sequence = DOTween.Sequence();
        _runAwayCost.text = string.Format("-{0}$", Mathf.RoundToInt(Mathf.Clamp(Moving.currentMoney / 10, 0, Moving.currentMoney)));
        sequence.Append(skillUI.transform.DOLocalMoveY(-354f, 1));
        StartCoroutine(WriteText("����� ������(��)�� �������ϴ�!"));
        //�ٸ� ��ų�� �� ����ֱ� ���� ������ �����ؾ���
    }

    public void HideSkillUI()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(0.4f);
        sequence.Append(skillUI.transform.DOLocalMoveY(-730, 1));
        //�ٸ� ��ų�� �� ����ֱ� ���� ������ �����ؾ���
    }

    public void AttackButton()
    {
        if (!_isAttaking && _skillCount[0] > 0)
        {
            StartCoroutine(HideButtons());
            int realDamage = Moving.playerAttack - Moving.enemyDefence;
            playerBehave.SetTrigger("Attack");
            StartCoroutine(WriteText($"����� {Mathf.Max(2, realDamage)}�� ���ݷ����� ���� �����߽��ϴ�."));
            _fightingEnemy.GetAttack();
            EnemyTurn();
            _skillCount[0]--;
            SetCountText();
        }
        //Dotext�� �ؽ�Ʈ �޽� ���ο� ����...
        else
        {
            if (_skillCount[0] <= 0)
            {
                StartCoroutine(WriteText("��ų�� �� �Ҹ��Ͽ����ϴ�."));
            }
            else if(!Moving._isPlayerTurn)
            {
                StartCoroutine(WriteText("���� ��� ���Դϴ�."));
            }
        }
    }

    public void BackButton()
    {
        skillPanel.transform.DOLocalMoveX(1250, 0.27f);
    }

    public void EnemyStateButton()
    {
        StartCoroutine(WriteText($"�̸�: ������\nü��: {Moving.enemycurrnetHealth}/{Moving.enemyHealth}\n���ݷ�: {Moving.enemyAttack}\n����: {Moving.enemyDefence}\nƯ¡: ��¿Ƽ�� "));
    }

    public void InputSkillButton()
    {
        if(!Moving._isPlayerTurn)
        {
            StartCoroutine(WriteText("���� ��� ���Դϴ�."));
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
            //Debug.Log("��Ӥ��ʱ�");
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
            StartCoroutine(WriteText($"ü���� {Mathf.Min(7, Moving.playerHealth - Moving.playerCurrentHealth)}ȸ�� �Ͽ����ϴ�."));
            Moving.playerCurrentHealth = Mathf.Min(Moving.playerCurrentHealth + 7, Moving.playerHealth);
            _stateUI.UpdateStateText();
            _fightingEnemy.StartCoroutine("EnemyAttack");
            EnemyTurn();
            _skillCount[1]--;
            SetCountText();
        }
        else
        {
            if (_skillCount[1] <= 0)
            {
                StartCoroutine(WriteText("��ų�� �� �Ҹ��Ͽ����ϴ�."));
            }
            else if(!Moving._isPlayerTurn)
            {
                StartCoroutine(WriteText("���� ��� ���Դϴ�."));
            }
        }
    }

    public void RunAwayButton()
    {
        if (Moving._isPlayerTurn)
        {
            StartCoroutine(WriteText("������~~!"));
            Enemy.currentMoney -= Mathf.RoundToInt(Mathf.Clamp(Moving.currentMoney / 10, 0, Moving.currentMoney));
            _backGround.CreateEnemy();
            Moving.enemycurrnetHealth = Moving.enemyHealth;
            Moving._playerState = Moving.PlayerState.IDLE;
            PlayerBehave.instance.EndBattle();
            Destroy(_fightingEnemy.gameObject);
        }
        else
        {
            StartCoroutine(WriteText("���� ��� ���Դϴ�."));
        }
    }

    public void BuySkills(int num)
    {
        _storeUI.AblingButtons(false);

        if (Moving.currentMoney < _storeUI.skillPrice[num]) { _storeUI.StartCoroutine("ShowStoreBehave", "���� �����ؿ�."); return; }
        if (_skillCount[num] > 10) { _storeUI.StartCoroutine("ShowStoreBehave", "��ų�� �ִ� 10������ ��� �ֽ��ϴ�."); return; }

        Moving.currentMoney -= _storeUI.skillPrice[num];
        _skillCount[num] += 1;

        
        _storeUI.StartCoroutine("ShowStoreBehave", "��ų�� �����Ͽ����ϴ�.");
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
