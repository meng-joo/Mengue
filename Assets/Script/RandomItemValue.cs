using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemRating
{
    Bronze,
    Silver,
    Gold,
    Platinum,
    Meng
}

[System.Serializable]
public class RandomItemValue
{
    public string skillName;
    public Sprite passiveImage;
    public string itemText;
    public ItemRating _ItemRating;
    public int weight;

    public bool isactive;

    public RandomItemValue(RandomItemValue _item)
    {
        this.skillName = _item.skillName;
        this.passiveImage = _item.passiveImage;
        this.itemText = _item.itemText;
        this._ItemRating = _item._ItemRating;
        this.weight = _item.weight;
    }
}
