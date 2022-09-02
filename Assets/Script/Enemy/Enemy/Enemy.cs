using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    protected int pos_X;
    protected int pos_Y;

    protected Vector3 currentPos = Vector3.zero;

    [Range(3, 7)]
    [SerializeField]
    private int enemyMoveTimeDeley;

    //private void Start()
    //{
    //    StartCoroutine("EnemyMove");
    //}

    //protected virtual IEnumerator EnemyMove()
    //{
    //    int randomTime = Random.Range(-3, 3);
    //    yield return new WaitForSeconds(enemyMoveTimeDeley + randomTime);

    //    int RandomX = Random.Range(-1, 2);
    //    int RandomZ = Random.Range(-1, 2);

    //    RandomZ = Mathf.Abs(RandomX) > 1 ? 0 : RandomZ;

    //    Vector3 randomPos = new Vector3(RandomX, 0, RandomZ);
        
    //    transform.DOMove(transform.position + randomPos, 0.1f);
    //}
}
