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
    private TextMeshPro _stateText;

    private bool _isAttaking = false;

    private void Start()
    {
        skillUI = GetComponent<Image>();
        _stateText = transform.Find("StateText/Text").GetComponent<TextMeshPro>();
    }

    public void ViewSkillUI()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(skillUI.transform.DOMoveY(120, 1));

        //다른 스킬을 더 띄어주기 위해 시퀀스 구현해야함
    }
    public void HideSkillUI()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(skillUI.transform.DOMoveY(-130, 1));
        //다른 스킬을 더 띄어주기 위해 시퀀스 구현해야함
    }

    public void AttackButton()
    {
        if (!_isAttaking)
        {
            playerBehave.SetTrigger("Attack");
            StartCoroutine(WriteText($"당신이 {Moving.playerAttack}의 공격력으로 상대방을 공격했습니다."));
            StartCoroutine(SetATKDelay());

        }
        //Dotext가 텍스트 메쉬 프로에 없어...
        else
        {
            StartCoroutine(WriteText("아직 상대 턴입니다."));
        }
    }

    IEnumerator WriteText(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            //Debug.Log("기ㅣㅁ필규");
            _stateText.text = string.Format("{0}", text.Substring(0, i));
            yield return new WaitForSeconds(0.07f);
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
