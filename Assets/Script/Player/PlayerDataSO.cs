using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PlayerDataSO")]
public class PlayerDataSO : ScriptableObject
{
    public int playerAttack = 3;
    public int playerCurrentAttack = 3;
    public int playerHealth = 20;
    public int playerAddHealth = 20;
    public int playerCurrentHealth = 20;
    public int playerDefence = 3;
    public int playerCurrentDefence = 3;

    public int currentMoney = 1000;
    public int moneyValue = 1;
}
