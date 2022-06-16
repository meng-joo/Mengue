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

    private int[] _skillCount = new int[2];

    private bool _isAttaking = false;

    private void Start()
    {
        skillUI = GetComponent<Image>();
        _stateText = transform.GetChild(0).Find("Text").GetComponent<TextMeshProUGUI>();
        _runAwayCost = transform.GetChild(2).Find("PenaltyText").GetComponent<TextMeshProUGUI>();
        skillPanel = transform.Find("SkillPanel").GetComponent<Image>();
        _skillCountText[0] = transform.Find("SkillPanel/AttackSkill/CountSkillText").GetComponent<TextMeshProUGUI>();
        _skillCountText[1] = transform.GetChild(3).Find("HealSkill/CountSkillText").GetComponent<TextMeshProUGUI>();
        _skillCount[0] = 10;
        _skillCount[0] = 10;
    }

    public void ViewSkillUI()
    {
        Sequence sequence = DOTween.Sequence();
        _runAwayCost.text = string.Format("-{0}$", Mathf.RoundToInt(Mathf.Clamp(Moving.currentMoney / 10, 0, Moving.currentMoney)));
        sequence.Append(skillUI.transform.DOLocalMoveY(-354f, 1));
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
            StartCoroutine(SetATKDelay());
            int realDamage = Moving.playerAttack - Moving.enemyDefence;
            playerBehave.SetTrigger("Attack");
            StartCoroutine(WriteText($"����� {Mathf.Clamp(realDamage, 2, realDamage)}�� ���ݷ����� ������ �����߽��ϴ�."));
            EnemyTurn();
            _skillCount[0]--;
            SetCountText();
        }
        //Dotext�� �ؽ�Ʈ �޽� ���ο� ����...
        else
        {
            StartCoroutine(WriteText("���� ��� ���Դϴ�."));
        }
    }

    public void EnemyStateButton()
    {
        StartCoroutine(WriteText($"�̸�: ��¿Ƽ��\nü��: {Moving.enemycurrnetHealth}/{Moving.enemyHealth}\n���ݷ�: {Moving.enemyAttack}\n����: {Moving.enemyDefence}\nƯ¡: ��¿Ƽ��"));
    }

    public void InputSkillButton()
    {
        if (!_isAttaking)
        {
            skillPanel.transform.DOLocalMoveX(670, 0.4f);
        }
        else
        {
            StartCoroutine(WriteText("���� ��� ���Դϴ�."));
        }
    }

    private void EnemyTurn()
    {
        skillPanel.transform.DOLocalMoveX(1250, 0.27f);
    }

    IEnumerator WriteText(string text)
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

    IEnumerator SetATKDelay()
    {
        _isAttaking = true;
        yield return new WaitForSeconds(1f);
        _isAttaking = false;
    }

    public void HealButton()
    {
        if (!_isAttaking && _skillCount[1] > 0)
        {
            playerBehave.SetTrigger("Heal");
            GameObject effect = Instantiate(PlayerBehave.instance._healEffect);
            effect.transform.position = PlayerBehave.instance.transform.position;
            EnemyTurn();
            _skillCount[1]--;
            SetCountText();
        }
        else
        {
            StartCoroutine(WriteText("���� ��� ���Դϴ�."));
        }
    }


    private void SetCountText()
    {
        _skillCountText[0].text = string.Format($"{_skillCount[0]} X");
        _skillCountText[1].text = string.Format($"{_skillCount[1]} X");
    }
}
