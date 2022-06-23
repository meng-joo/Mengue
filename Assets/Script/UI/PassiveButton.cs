using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PassiveButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RandomItemValue itemData = null;
    RandomGacha randomGacha;


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemData == null) return;
        randomGacha.skillExplanText.gameObject.SetActive(true);
        randomGacha.skillExplanText.text = string.Format("{0}", itemData.itemText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        randomGacha.skillExplanText.gameObject.SetActive(false);
    }

    void Start()
    {
        randomGacha = transform.parent.GetComponent<RandomGacha>();
    }

}
