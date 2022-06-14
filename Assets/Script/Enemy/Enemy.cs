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

    private int enemyHP = 15;
    public Vector3 tempVec;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        pPos = GameObject.Find("Player").GetComponent<PlayerBehave>();
        tempVec = transform.position;
    }

    protected override void InputEnemyMovingKey()
    {
        Debug.Log("serhejwkyetkexulrle5lele5l5ek7l5");
        for (int c = 0; c < _backGround.enemycount; c++)
        {
            Sequence seq = DOTween.Sequence();

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

                if (enemyX == Ex && enemyZ == Ez)
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
                transform.DOMove(transform.position + randomTransform, 0.13f);
            }
        }
    }

    private void StartBattle(GameObject player)
    {
        StartCoroutine(BattleEnemy(player));
    }

    IEnumerator BattleEnemy(GameObject player)
    {
        enemyView.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Quaternion quaternion = Quaternion.Euler(75, 0, 0);
        transform.position = player.transform.position + new Vector3(1, 0.5f, 1);
        //transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 1);
        transform.LookAt(player.transform);
        transform.rotation *= quaternion;

        //for (int i = 0; i < _backGround.enemycount; i++)
        //{
        //    _backGround._enemyList[i].gameObject.SetActive(false);
        //    gameObject.SetActive(true);
        //}
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
        enemyHP -= playerAttack;

        if (enemyHP <= 0)
        {
            Instantiate(_particleSystem[1], transform.position, Quaternion.identity);
            _backGround._enemyList.Remove(this);
            Destroy(gameObject);
            Moving._playerState = PlayerState.IDLE;
            pPos.EndBattle();

            _backGround.CreateEnemy();
        }
        else
        {
            Instantiate(_particleSystem[0], transform.position, Quaternion.identity);
            //_animator.SetTrigger("GetHit");
        }
    }
}