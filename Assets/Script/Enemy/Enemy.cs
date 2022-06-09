using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : Moving
{
    public GameObject enemyView;

    public PlayerBehave pPos;
    public ParticleSystem[] _particleSystem;

    private Animator _animator;

    private int enemyHP = 30;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        pPos = GameObject.Find("Player").GetComponent<PlayerBehave>();
    }

    protected override void InputEnemyMovingKey()
    {
        int x = Random.Range(-1, 2);
        int z = Random.Range(-1, 2);
        bool isOverlap = false;
        bool isOverlapToPlayer = false;

        if (x == 1 || x == -1)
        {
            z = 0;
        }

        Vector3 randomTransform = new Vector3(x, 0, z);

        float enemyX = transform.position.x + randomTransform.x;
        float enemyZ = transform.position.z + randomTransform.z;

        int Px = Mathf.CeilToInt(pPos.transform.position.x);
        int Pz = Mathf.CeilToInt(pPos.transform.position.z);

        if (enemyX == Px && enemyZ == Pz)
        {
            isOverlapToPlayer = true;
        }

        for (int i = 0; i < _backGround._enemyList.Count; i++)
        {
            int Ex = Mathf.CeilToInt(_backGround._enemyList[i].transform.position.x);
            int Ez = Mathf.CeilToInt(_backGround._enemyList[i].transform.position.z);

            if(enemyX == Ex && enemyZ == Ez)
            {
                isOverlap = true;
            }
        }

        if ((enemyX >= BackGround.MaxX) || (enemyX <= BackGround.MinX) || (enemyZ <= BackGround.MinZ) || (enemyZ >= BackGround.MaxZ) || isOverlap || isOverlapToPlayer)
        {
            Debug.Log("안돼 돌아가");
            //transform.position -= Vector3.Lerp();
            //transform.DOKill();
            //transform.DOMove(transform.position - randomTransform, 0.3f);
            return;
        }
        else
        {
            Debug.Log("이게 왜....");
            transform.DOKill();
            transform.DOMove(transform.position + randomTransform, 0.1f);
        }
    }

    private void StartBattle(GameObject player)
    {
        StartCoroutine(BattleEnemy(player));
    }

    IEnumerator BattleEnemy(GameObject player)
    {
        enemyView.SetActive(false);
        yield return new WaitForSeconds(2);
        Quaternion quaternion = Quaternion.Euler(90, 0, 0);
        transform.position += new Vector3(0, 0.5f, 0);
        //transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 1);
        transform.LookAt(player.transform);
        transform.rotation *= quaternion;

        for (int i = 0; i < _backGround.enemycount; i++)
        {
            _backGround._enemyList[i].gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sword")
        {
            Debug.Log("적이 맞음!!!!!!적이 맞음!!!!!");
            GetAttack();
        }
    }

    public void GetAttack()
    {
        if(enemyHP <= 0)
        {
            _backGround._enemyList.Remove(this);
            Destroy(gameObject);
            Moving._isBattle = false;
            pPos.EndBattle();
            Instantiate(_particleSystem[1], transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_particleSystem[0], transform.position, Quaternion.identity);
            enemyHP -= 10;
        }
        //_animator.SetTrigger("GetHit");
    }
}
