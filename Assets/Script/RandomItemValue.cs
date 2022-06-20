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
    public ItemRating _ItemRating;

    public RandomItemValue(RandomItemValue _item)
    {
        this.skillName = _item.skillName;
        this.passiveImage = _item.passiveImage;
        this._ItemRating = _item._ItemRating;
    }
}
