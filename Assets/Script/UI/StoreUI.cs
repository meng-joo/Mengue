using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StoreUI : MonoBehaviour
{
    public Button[] upgradePanel;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void SetStoreUI()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMoveY(7, 1f));
    }
}
