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

        sequence.Append(skillUI.transform.DOMoveY(120, 1));

        //�ٸ� ��ų�� �� ����ֱ� ���� ������ �����ؾ���
    }
    public void HideSkillUI()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(skillUI.transform.DOMoveY(-130, 1));
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
        yield return new WaitForSeconds(1f);
        _isAttaking = false;
    }

    public void HealButton()
    {
        playerBehave.SetTrigger("Heal");
        GameObject effect = Instantiate(PlayerBehave.instance._healEffect);
        effect.transform.position = PlayerBehave.instance.transform.position;
    }
}
