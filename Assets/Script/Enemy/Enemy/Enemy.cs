using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Moving
{
    protected int pos_X;
    protected int pos_Y;

    protected Vector3 currentPos = Vector3.zero;

    [Range(3, 7)]
    [SerializeField]
    private int enemyMoveTimeDeley;

    private void Start()
    {
        StartCoroutine("EnemyMove");
    }

    protected virtual IEnumerator EnemyMove()
    {
        yield return new WaitForSeconds(enemyMoveTimeDeley);



    }
}
