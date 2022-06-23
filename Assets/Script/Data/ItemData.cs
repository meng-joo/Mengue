using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PassiveItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;

    public Sprite skillImage;
    
    public float defenceUp;
    public float attackUp;
    public float healthUp;

    public int view;
    public int gold;
}
