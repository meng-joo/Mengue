using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss : Moving
{
    public GameObject enemyView;
    private SkillUI _skillUI;
    private StateUI _stateUI;

    public PlayerBehave pPos;
    public ParticleSystem[] _particleSystem;

    private Animator _animator;

    public Vector3 tempVec;

    int posisonReducedDamage = 0;

    protected BackGround _backGround;
    int healCount = 0;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _stateUI = FindObjectOfType<StateUI>();
        pPos = GameObject.Find("Player").GetComponent<PlayerBehave>();
        _skillUI = FindObjectOfType<SkillUI>();
        tempVec = transform.position;

        if (_backGround == null) _backGround = FindObjectOfType<BackGround>();
    }
    protected override void InputEnemyMovingKey()
    {
        //for (int c = 0; c < _backGround.enemycount; c++)
        //{
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

        for (int i = 0; i < _backGround._bossList.Count; i++)
        {
            int Ex = Mathf.CeilToInt(_backGround._bossList[i].transform.position.x);
            int Ez = Mathf.CeilToInt(_backGround._bossList[i].transform.position.z);

            if (Mathf.Abs(enemyX - Ex) <= 3 && Mathf.Abs(enemyZ - Ez) <= 3)
            {
                isOverlap = true;
            }
        }

        for (int i = 0; i < _backGround._enemyList.Count; i++)
        {
            int Ex = Mathf.CeilToInt(_backGround._enemyList[i].transform.position.x);
            int Ez = Mathf.CeilToInt(_backGround._enemyList[i].transform.position.z);

            if (Mathf.Abs(enemyX - Ex) <= 3 && Mathf.Abs(enemyZ - Ez) <= 3)
            {
                isOverlap = true;
            }
        }

        if ((enemyX >= BackGround.MaxX - 2) || (enemyX <= BackGround.MinX + 2) || (enemyZ <= BackGround.MinZ + 2) || (enemyZ >= BackGround.MaxZ - 3) || isOverlap || isOverlapToPlayer)
        {
            Debug.Log("안돼 돌아가");
            _canEnemyMove = true;
            return;
        }
        else
        {
            Debug.Log("이게 왜....");
            transform.DOKill();
            int goX = Mathf.CeilToInt(transform.position.x);
            int goZ = Mathf.CeilToInt(transform.position.z);

            transform.position = new Vector3(goX, transform.position.y, goZ);

            transform.DOLocalMove(transform.position + randomTransform, 0.13f).OnComplete(() => _canEnemyMove = true);
        }
    }

    public void StartBattle(GameObject player)
    {
        StartCoroutine(BattleBoss(player));
    }

    IEnumerator BattleBoss(GameObject player)
    {

        enemyView.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Quaternion quaternion = Quaternion.Euler(60, 0, 0);
        transform.position = player.transform.position + new Vector3(1, 1.5f, 1);
        //transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 1);
        bosscurrnetHealth = bossHealth;
        transform.LookAt(player.transform);
        transform.rotation *= quaternion;
    }

    public void GetAttack()
    {
        bool _isCri = false;
        int realDamage = playerCurrentAttack - bossDefence;
        int midasExtraDamage;

        midasExtraDamage = passive_Midas ? Mathf.RoundToInt(Mathf.Min(Mathf.Max(2, realDamage), Mathf.Max(2, realDamage) * (currentMoney * 0.000025f))) : 0;

        //realDamage += midasExtraDamage;

        if (passive_David) realDamage *= playerAddHealth < bossHealth ? 2 : 1;

        posisonReducedDamage = Random.Range(1, 11);

        SoundClips.instance.EffectSound(2);

        if (passive_Critical) { if (posisonReducedDamage <= 1) { bosscurrnetHealth -= Mathf.RoundToInt(Mathf.Max(2, (realDamage + midasExtraDamage) * 1.7f)); _isCri = true; } }

        else bosscurrnetHealth -= Mathf.Max(2, (realDamage + midasExtraDamage));

        playerCurrentHealth += passive_Boold ? Mathf.RoundToInt(Mathf.Max(2, (realDamage + midasExtraDamage) * 1.7f)) / 2 : 0;
        playerCurrentHealth = playerCurrentHealth > playerAddHealth ? playerAddHealth : playerCurrentHealth;
        _stateUI.UpdateStateText();

        realDamage = Mathf.Max(2, realDamage);

        if (bosscurrnetHealth <= 0)
        {
            StartCoroutine(EnemyDead(realDamage / 2, _isCri));
        }

        else
        {
            if(!_isCri) _skillUI.SendMessage("OtherWriteText", $"당신은 뽀스의 피를 {realDamage}(+{midasExtraDamage}) 만큼 깎았습니다. ");
            else _skillUI.SendMessage("OtherWriteText", $"크리티컬! 당신은 뽀스의 피를 {Mathf.RoundToInt(realDamage * 1.7f)}(+{Mathf.RoundToInt(Mathf.Max(2, (realDamage + midasExtraDamage) * 1.7f)) - Mathf.RoundToInt(realDamage * 1.7f)}) 만큼 깎았습니다. ");
            EnemyTurn(realDamage / 2);
        }
    }

    IEnumerator EnemyDead(int a = 0, bool critical = false)
    {
        StartCoroutine(EnemyHitParticle());

        if (passive_Boold) { yield return new WaitForSeconds(2f); _skillUI.SendMessage("OtherWriteText", $"당신은 +{a}만큼 피를 빨았습니다. "); }
        if (critical) { yield return new WaitForSeconds(2f); _skillUI.SendMessage("OtherWriteText", $"치명적인 공격!!!"); }

        yield return new WaitForSeconds(2f);

        _skillUI.SendMessage("OtherWriteText", $"당신은 뽀스을 죽이고 {moneyValue * bossMoney}원을 얻었습니다! ");
        currentMoney += moneyValue * bossMoney;

        if (currentMoney >= 1000000)
            PlayerBehave.instance.PlayerWin();

        _stateUI.UpdateStateText();

        yield return new WaitForSeconds(3f);
        _skillUI._fightingBoss = null;
        _isPlayerTurn = true;
        _backGround._bossList.Remove(this);
        Destroy(gameObject);
        Moving._playerState = PlayerState.IDLE;
        pPos.EndBattle();

        bosscurrnetHealth = bossHealth;

        _backGround.CreateBoss();

        _skillUI.StartCoroutine("ShowButtons");
    }

    IEnumerator EnemyHitParticle()
    {
        yield return new WaitForSeconds(0.8f);

        Debug.Log("이재엽이재엽이재엽");
        SoundClips.instance.EffectSound(2);
        Instantiate(_particleSystem[0], transform.position + Vector3.forward, Quaternion.identity);
    }

    public void EnemyTurn(int a)
    {
        if (bosscurrnetHealth < bossHealth / 3 && healCount < 2)
        {
            StartCoroutine(BossHeal(a));
        }
        else EnemyAttack(a);

        StartCoroutine(EnemyHitParticle());
    }

    IEnumerator BossHeal(int a)
    {
        _isPlayerTurn = false;

        if (passive_Boold)
        {
            yield return new WaitForSeconds(2f);
            _skillUI.SendMessage("OtherWriteText", $"당신은 +{a}만큼 피를 빨았습니다. ");
        }

        yield return new WaitForSeconds(1.8f);

        _skillUI.SendMessage("OtherWriteText", $"뽀스는 위기를 느껴 피를 {Mathf.RoundToInt(bossAttack * 2 - bossAttack / 3)}회복하였습니다!");
        bosscurrnetHealth += bossAttack * 2 - bossAttack / 3;

        //pPos._battleCamera.EnemyAttack();
        pPos._battleCamera.BossHeal();

        if (bosscurrnetHealth > bossHealth) bosscurrnetHealth = bossHealth;

        yield return new WaitForSeconds(1f);
        Instantiate(_particleSystem[2], transform.position + Vector3.forward, Quaternion.identity);

        _stateUI.UpdateStateText();
        healCount++;
        yield return new WaitForSeconds(2.2f);
        _skillUI.StartCoroutine("ShowButtons");
        yield return new WaitForSeconds(0.3f);
        _isPlayerTurn = true;
        _skillUI.SendMessage("OtherWriteText", $"무엇을 하시겠습니까? ");
    }

    void EnemyAttack(int a)
    {
        StartCoroutine(Attacking(a));
        healCount = 0;
    }

    IEnumerator Attacking(int a)
    {
        int canAttack = Random.Range(0, passive_100m);
        bool ispoison = false;

        if (passive_Boold)
        {
            yield return new WaitForSeconds(2f);
            _skillUI.SendMessage("OtherWriteText", $"당신은 +{a}만큼 피를 빨았습니다. ");
        }

        if (demageBlock)
        {
            _isPlayerTurn = false;
            yield return new WaitForSeconds(2f);
            _skillUI.SendMessage("OtherWriteText", $"당신은 비눗방울로 공격을 막았습니다! 비눗방울은 터졌습니다.");
            yield return new WaitForSeconds(4.3f);
            _skillUI.StartCoroutine("ShowButtons");
            _stateUI.UpdateStateText();
            demageBlock = false;
            _isPlayerTurn = true;
        }

        else
        {
            if (canAttack == 0)
            {
                _isPlayerTurn = false;
                yield return new WaitForSeconds(3f);
                pPos._battleCamera.EnemyAttack();
                _skillUI.SendMessage("OtherWriteText", $"뽀스(의)에 전기찌릿!! ");
                yield return new WaitForSeconds(2.2f);

                SoundClips.instance.EffectSound(4);
                int enemyPower = Random.Range(bossAttack - 3, Mathf.RoundToInt(bossAttack * 1.7f));
                int damage = Mathf.Max(3, enemyPower - playerCurrentDefence);

                posisonReducedDamage = Random.Range(1, 11);
                if (posisonReducedDamage < 4) ispoison = true;
                damage = ispoison ? Mathf.RoundToInt(damage * 0.54f) : damage;

                //damage = passive_Poison?posisonReducedDamage : 
                playerCurrentHealth -= damage;
                Instantiate(PlayerBehave.instance._hitEffect[1], transform);
                
                PlayerBehave.instance.ani.SetTrigger("GetHit");
                yield return new WaitForSeconds(1.2f);

                if (!passive_Poison) _skillUI.SendMessage("OtherWriteText", $"뽀스(이)가 당신의 피를 {damage}만큼 깎았습니다.");
                else _skillUI.SendMessage("OtherWriteText", $"[중독된] 뽀스(이)가 당신의 피를 {damage}만큼 깎았습니다.");

                if (passive_Reflect) _skillUI.SendMessage("OtherWriteText", $"뽀스은 당신은 때리다가 가시에 찔려 {Mathf.Max(damage / 4, 2)}의 데미지를 받았습니다.");
                bosscurrnetHealth -= Mathf.Max(damage / 4, 2);
                if (bosscurrnetHealth <= 0) { StartCoroutine(EnemyDead()); yield return null; }


                yield return new WaitForSeconds(2.1f);
                _skillUI.StartCoroutine("ShowButtons");

                if (playerCurrentHealth <= 0)
                {
                    if (!passive_DemiGod)
                    {
                        _skillUI.SendMessage("OtherWriteText", $"크아아아악...!! 당신은 뽀스의 전기에 맞고 타죽어버렸습니다!!");
                        yield return new WaitForSeconds(3.4f);
                        _skillUI.SendMessage("OtherWriteText", $"안타깝지만 당신은 모든 것을 잃고 처음으로 돌아갑니다.");
                        yield return new WaitForSeconds(2.2f);
                        PlayerBehave.instance.PlayerDead();
                        yield return null;
                    }
                    else
                    {
                        _skillUI.SendMessage("OtherWriteText", $"'신은 죽지 않는다'");
                        playerCurrentHealth = 1;
                    }
                }

                else
                {
                    _stateUI.UpdateStateText();
                    _isPlayerTurn = true;
                }
            }

            else if (canAttack == 1)
            {
                _isPlayerTurn = false;
                yield return new WaitForSeconds(3f);
                _skillUI.SendMessage("OtherWriteText", $"뽀스는 당신에게 손이 닿지 않습니다! ");
                yield return new WaitForSeconds(2f);

                _skillUI.StartCoroutine("ShowButtons");
                _stateUI.UpdateStateText();
                _isPlayerTurn = true;
            }
        }

        yield return new WaitForSeconds(0.5f);
        _skillUI.SendMessage("OtherWriteText", $"무엇을 하시겠습니까? ");
    }
}
