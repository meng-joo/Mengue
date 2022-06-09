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
    private GameObject _enemy;

    private void Start()
    {
        skillUI = GetComponent<Image>();
    }

    public void ViewSkillUI(GameObject enemy)
    {
        _enemy = enemy;
        Sequence sequence = DOTween.Sequence();

        sequence.Append(skillUI.transform.DOMoveX(1700, 2));
    }

    public void AttackButton()
    {
        playerBehave.SetTrigger("Attack");
    }
    public void HealButton()
    {
        playerBehave.SetTrigger("Heal");
    }
}
