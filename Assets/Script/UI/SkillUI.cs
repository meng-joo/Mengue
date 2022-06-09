using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class SkillUI : MonoBehaviour
{
    private Image skillUI;
    public Animator playerBehave;

    private bool _isAttaking = false;

    private void Start()
    {
        skillUI = GetComponent<Image>();
    }

    public void ViewSkillUI()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(skillUI.transform.DOMoveX(1700, 2));

        //�ٸ� ��ų�� �� ����ֱ� ���� ������ �����ؾ���
    }
    public void HideSkillUI()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(skillUI.transform.DOMoveX(2280, 2));
        //�ٸ� ��ų�� �� ����ֱ� ���� ������ �����ؾ���
    }

    public void AttackButton()
    {
        if (!_isAttaking)
        {
            playerBehave.SetTrigger("Attack");
            StartCoroutine(SetATKDelay());
        }
    }

    IEnumerator SetATKDelay()
    {
        _isAttaking = true;
        yield return new WaitForSeconds(1.7f);
        _isAttaking = false;
    }

    public void HealButton()
    {
        playerBehave.SetTrigger("Heal");
    }
}
