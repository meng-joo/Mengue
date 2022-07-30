using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    public int enemyHealth;
    public int enemyAttack;
    public int enemyDefence;
    public int enemyMoney;
    public float enemyMoveDeley;
}
