using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : Moving
{
    public GameObject enemyView;
    private SkillUI _skillUI;
    private StateUI _stateUI;

    public PlayerBehave pPos;
    public ParticleSystem[] _particleSystem;

    private Animator _animator;

    public Vector3 tempVec;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _stateUI = FindObjectOfType<StateUI>();
        pPos = GameObject.Find("Player").GetComponent<PlayerBehave>();
        _skillUI = FindObjectOfType<SkillUI>();
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
                Debug.Log("æ»µ≈ µπæ∆∞°");
                //transform.position -= Vector3.Lerp();
                //transform.DOKill();
                //transform.DOMove(transform.position - randomTransform, 0.3f);
                return;
            }
            else
            {
                Debug.Log("¿Ã∞‘ ø÷....");
                transform.DOLocalMove(transform.position + randomTransform, 0.13f);
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
        enemycurrnetHealth = enemyHealth;

        //for (int i = 0; i < _backGround.enemycount; i++)
        //{
        //    _backGround._enemyList[i].gameObject.SetActive(false);
        //    gameObject.SetActive(true);
        //}
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Sword")
    //    {
    //        Debug.Log("¿˚¿Ã ∏¬¿Ω!!!!!!¿˚¿Ã ∏¬¿Ω!!!!!");
    //        GetAttack();
    //    }
    //}

    public void GetAttack()
    {
        int realDamage = playerAttack - enemyDefence;
        enemycurrnetHealth -= Mathf.Clamp(realDamage, 2, realDamage);

        if (enemycurrnetHealth <= 0)
        {
            StartCoroutine(EnemyDead());
            //Instantiate(_particleSystem[1], transform.position, Quaternion.identity);

        }

        else
        {
            //_animator.SetTrigger("GetHit");
            EnemyTurn();
        }
    }

    IEnumerator EnemyDead()
    {
        _skillUI.SendMessage("OtherWriteText", $"¥ÁΩ≈¿∫ »Ô«Û«Û¿ª ¡◊¿Ã∞Ì {moneyValue * enemyMoney}ø¯¿ª æÚæ˙Ω¿¥œ¥Ÿ!");
        currentMoney += moneyValue * enemyMoney;
        _stateUI.UpdateStateText();

        yield return new WaitForSeconds(5f);
        _backGround._enemyList.Remove(this);
        Destroy(gameObject);
        Moving._playerState = PlayerState.IDLE;
        pPos.EndBattle();

        _backGround.CreateEnemy();
    }

    IEnumerator EnemyHitParticle()
    {
        yield return new WaitForSeconds(0.65f);
        Debug.Log("¿Ã¿Áø±¿Ã¿Áø±¿Ã¿Áø±");
        Instantiate(_particleSystem[0], transform.position + Vector3.forward, Quaternion.identity);
    }

    public void EnemyTurn()
    {
        StartCoroutine(EnemyAttack());
        StartCoroutine(EnemyHitParticle());
    }

    IEnumerator EnemyAttack()
    {
        _isPlayerTurn = false;
        yield return new WaitForSeconds(2f);

        _skillUI.SendMessage("OtherWriteText", $"»Â«Û«Û(¿«)ø° ∆¯∆»∆›ƒ°!! ");
        yield return new WaitForSeconds(1f);

        int enemyPower = Random.Range(enemyAttack - 3, Mathf.RoundToInt(enemyAttack * 1.4f));

        int damage = Mathf.Max(1, enemyPower - playerDefence);
        playerCurrentHealth -= damage;
        Instantiate(PlayerBehave.instance._hitEffect, PlayerBehave.instance.transform);
        PlayerBehave.instance.ani.SetTrigger("GetHit");
        yield return new WaitForSeconds(1.2f);

        _skillUI.SendMessage("OtherWriteText", $"»Â«Û«Û(¿Ã)∞° ¥ÁΩ≈¿« ««∏¶ {damage}∏∏≈≠ ±æ“Ω¿¥œ¥Ÿ.");

        if(playerCurrentHealth <= 0)
        {
            PlayerBehave.instance.PlayerDead();
            yield return null;
        }

        else
        {
            _stateUI.UpdateStateText();
            _isPlayerTurn = true;
        }
    }
}