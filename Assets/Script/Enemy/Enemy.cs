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

    int posisonReducedDamage = 0;

    protected BackGround _backGround;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _stateUI = FindObjectOfType<StateUI>();
        pPos = GameObject.Find("Player").GetComponent<PlayerBehave>();
        _skillUI = FindObjectOfType<SkillUI>();
        tempVec = transform.position;

        Debug.Log("전에 백그라운드" + _backGround);
        if (_backGround == null) _backGround = FindObjectOfType<BackGround>();
        Debug.Log("후에 백그라운드" + _backGround);
    }

    protected override void InputEnemyMovingKey()
    {
        //Debug.Log("serhejwkyetkexulrle5lele5l5ek7l5");
        //for (int c = 0; c < _backGround.enemycount; c++)
        //{
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

            if (Mathf.Abs(enemyX - Ex) <= 3 && Mathf.Abs(enemyZ - Ez) <= 3)
            {
                isOverlap = true;
            }
        }

        for (int i = 0; i < _backGround._bossList.Count; i++)
        {
            int Ex = Mathf.CeilToInt(_backGround._enemyList[i].transform.position.x);
            int Ez = Mathf.CeilToInt(_backGround._enemyList[i].transform.position.z);

            if (Mathf.Abs(enemyX - Ex) <= 3 && Mathf.Abs(enemyZ - Ez) <= 3)
            {
                isOverlap = true;
            }
        }

        if ((enemyX >= BackGround.MaxX - 1) || (enemyX <= BackGround.MinX + 1) || (enemyZ <= BackGround.MinZ + 1) || (enemyZ >= BackGround.MaxZ - 2) || isOverlap || isOverlapToPlayer)
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
        StartCoroutine(BattleEnemy(player));
    }

    IEnumerator BattleEnemy(GameObject player)
    {
        enemyView.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Quaternion quaternion = Quaternion.Euler(75, 0, 0);
        transform.position = player.transform.position + new Vector3(1, 0.5f, 1);
        enemycurrnetHealth = enemyHealth;
        transform.LookAt(player.transform);
        transform.rotation *= quaternion;
    }

    public void GetAttack()
    {
        bool _isCri = false;
        int realDamage = playerCurrentAttack - enemyDefence;
        int midasExtraDamage;

        midasExtraDamage = passive_Midas ? Mathf.RoundToInt(Mathf.Min(Mathf.Max(2, realDamage), Mathf.Max(2, realDamage) * (currentMoney * 0.000025f))) : 0;

        //realDamage += midasExtraDamage;

        if (passive_David) realDamage *= playerAddHealth < enemyHealth ? 2 : 1;

        posisonReducedDamage = Random.Range(1, 11);
        
        if (passive_Critical) { if (posisonReducedDamage <= 1) enemycurrnetHealth -= Mathf.RoundToInt(Mathf.Max(2, (realDamage + midasExtraDamage) * 1.7f)); _isCri = true; }

        else enemycurrnetHealth -= Mathf.Max(2, realDamage + midasExtraDamage);
        
        playerCurrentHealth += passive_Boold ? Mathf.RoundToInt(Mathf.Max(2, (realDamage + midasExtraDamage) * 1.7f)) / 2 : 0;
        playerCurrentHealth = playerCurrentHealth > playerAddHealth ? playerAddHealth : playerCurrentHealth;
        _stateUI.UpdateStateText();

        realDamage = Mathf.Max(2, realDamage);

        if (enemycurrnetHealth <= 0)
        {           
            StartCoroutine(EnemyDead(realDamage / 2, _isCri));
        }

        else
        {
            if (!_isCri) _skillUI.SendMessage("OtherWriteText", $"당신은 상대방의 피를 {realDamage}(+{midasExtraDamage}) 만큼 깎았습니다. ");
            else _skillUI.SendMessage("OtherWriteText", $"당신은 상대방의 피를 {Mathf.RoundToInt(Mathf.Max(2, (realDamage) * 1.7f))}(+{midasExtraDamage}) 만큼 깎았습니다. ");
            EnemyTurn(realDamage / 2);
        }
    }

    IEnumerator EnemyDead(int a = 0, bool critical = false)
    {
        StartCoroutine(EnemyHitParticle());

        if (passive_Boold) { yield return new WaitForSeconds(2f); _skillUI.SendMessage("OtherWriteText", $"당신은 +{a}만큼 피를 빨았습니다. "); }
        if (critical) { yield return new WaitForSeconds(2f); _skillUI.SendMessage("OtherWriteText", $"치명적인 공격!!!"); }
        //if(critical)

        yield return new WaitForSeconds(2f);

        _skillUI.SendMessage("OtherWriteText", $"당신은 흥헹헹을 죽이고 {moneyValue * enemyMoney}원을 얻었습니다! ");
        currentMoney += moneyValue * enemyMoney;
        if (currentMoney >= 1000000)
            PlayerBehave.instance.PlayerWin();
        _stateUI.UpdateStateText();

        yield return new WaitForSeconds(3f);
        _skillUI._fightingEnemy = null;
        _isPlayerTurn = true;
        _backGround._enemyList.Remove(this);
        Destroy(gameObject);
        Moving._playerState = PlayerState.IDLE;
        pPos.EndBattle();
        enemycurrnetHealth = enemyHealth;

        _backGround.CreateEnemy();
        yield return new WaitForSeconds(1.3f);
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
        EnemyAttack(a);
        StartCoroutine(EnemyHitParticle());
    }

    void EnemyAttack(int a)
    {
        StartCoroutine(Attacking(a));
    }

    IEnumerator Attacking(int a)
    {
        int canAttack = Random.Range(0, passive_100m);
        bool ispoison = false;

        yield return new WaitForSeconds(2f);

        if (passive_Boold) { _skillUI.SendMessage("OtherWriteText", $"당신은 +{a}만큼 피를 빨았습니다. "); _stateUI.UpdateStateText(); }

        if (demageBlock)
        {
            _isPlayerTurn = false;
            yield return new WaitForSeconds(3f);
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
                yield return new WaitForSeconds(2f);
                pPos._battleCamera.EnemyAttack();
                _skillUI.SendMessage("OtherWriteText", $"흐헹헹(의)에 폭발펀치!! ");
                yield return new WaitForSeconds(2.2f);

                SoundClips.instance.EffectSound(3);
                int enemyPower = Random.Range(enemyAttack - 3, Mathf.RoundToInt(enemyAttack * 1.4f));
                int damage = Mathf.Max(1, enemyPower - playerCurrentDefence);

                if (passive_Poison)
                {
                    posisonReducedDamage = Random.Range(1, 11);
                    if (posisonReducedDamage <= 4) ispoison = true;
                    damage = ispoison ? Mathf.RoundToInt(damage * 0.54f) : damage;
                }
                //damage = passive_Poison?posisonReducedDamage : 
                playerCurrentHealth -= damage;
                Instantiate(PlayerBehave.instance._hitEffect[0], transform);
                PlayerBehave.instance.ani.SetTrigger("GetHit");
                
                yield return new WaitForSeconds(1.2f);

                if (!ispoison) _skillUI.SendMessage("OtherWriteText", $"흐헹헹(이)가 당신의 피를 {damage}만큼 깎았습니다.");
                else _skillUI.SendMessage("OtherWriteText", $"[중독된] 흐헹헹(이)가 당신의 피를 {damage}만큼 깎았습니다.");


                if (passive_Reflect) { yield return new WaitForSeconds(2f); Instantiate(_particleSystem[0], transform); _skillUI.SendMessage("OtherWriteText", $"흐헹헹은 당신을 때리다가 가시에 찔려 {Mathf.Max(damage / 4, 1)}의 데미지를 받았습니다."); }
                enemycurrnetHealth -= Mathf.Max(damage / 4, 1);
                if (enemycurrnetHealth <= 0) { StartCoroutine(EnemyDead()); yield return null; }

                if (playerCurrentHealth <= 0)
                {
                    if (!passive_DemiGod)
                    {
                        yield return new WaitForSeconds(0.6f);
                        _skillUI.SendMessage("OtherWriteText", $"헐... 당신은 흐헹헹에게 죽고 말았습니다!! ");
                        yield return new WaitForSeconds(3.3f);
                        _skillUI.SendMessage("OtherWriteText", $"풉.. 창피하지도 않으신가요? 당신은 모든 것을 잃었습니다. 처음으로 돌아갑니다");
                        yield return new WaitForSeconds(4f);

                        PlayerBehave.instance.PlayerDead();
                        yield return null;
                    }
                    else
                    {
                        _skillUI.SendMessage("OtherWriteText", $"'신은 죽지 않는다'");
                        playerCurrentHealth = 1;

                        _skillUI.StartCoroutine("ShowButtons");

                        yield return new WaitForSeconds(0.7f);
                    }
                }

                else
                {
                    _stateUI.UpdateStateText();
                    _isPlayerTurn = true;

                    _skillUI.StartCoroutine("ShowButtons");

                    yield return new WaitForSeconds(0.7f);
                }
            }

            else if (canAttack == 1)
            {
                _isPlayerTurn = false;
                yield return new WaitForSeconds(3f);
                _skillUI.SendMessage("OtherWriteText", $"적은 당신에게 손이 닿지 않습니다! ");
                yield return new WaitForSeconds(2f);

                _skillUI.StartCoroutine("ShowButtons");
                _stateUI.UpdateStateText();
                _isPlayerTurn = true;
            }
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(0.5f);
        _skillUI.SendMessage("OtherWriteText", $"무엇을 하시겠습니까? ");
    }
}