using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image whiteXImage = null;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(EnterX());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(ExitX());
    }

    IEnumerator EnterX()
    {
        Color al = whiteXImage.color;

        while (al.a <= 1)
        {
            al.a += 0.2f;
            whiteXImage.color = al;
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator ExitX()
    {
        Color al = whiteXImage.color;

        while (al.a >= 0)
        {
            al.a -= 0.2f;
            whiteXImage.color = al;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
