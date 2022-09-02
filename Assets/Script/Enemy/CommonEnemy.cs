//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;

//public class CommonEnemy : Enemy
//{
//    public EnemyDataSO commonEnemyData;
//    public GameObject enemyView;
//    private SkillUI _skillUI;
//    private StateUI _stateUI;

//    public PlayerBehave pPos;
//    public ParticleSystem[] _particleSystem;

//    private Animator _animator;

//    public Vector3 tempVec;

//    int posisonReducedDamage = 0;

//    protected Setting _backGround;

//    public int enemycurrnetHealth;

//    private void Start()
//    {
//        _animator = GetComponent<Animator>();
//        _stateUI = FindObjectOfType<StateUI>();
//        pPos = GameObject.Find("Player").GetComponent<PlayerBehave>();
//        _skillUI = FindObjectOfType<SkillUI>();
//        tempVec = transform.position;

//        Debug.Log("전에 백그라운드" + _backGround);
//        if (_backGround == null) _backGround = FindObjectOfType<Setting>();
//        Debug.Log("후에 백그라운드" + _backGround);

//        enemycurrnetHealth = commonEnemyData.enemyHealth;
//    }


//    public void StartBattle(GameObject player)
//    {
//        StartCoroutine(BattleEnemy(player));
//    }

//    IEnumerator BattleEnemy(GameObject player)
//    {
//        enemyView.SetActive(false);
//        yield return new WaitForSeconds(0.2f);
//        Quaternion quaternion = Quaternion.Euler(75, 0, 0);
//        transform.position = player.transform.position + new Vector3(1, 0.5f, 1);
//        enemycurrnetHealth = commonEnemyData.enemyHealth;
//        transform.LookAt(player.transform);
//        transform.rotation *= quaternion;
//    }

//    public void GetAttack()
//    {
//        bool _isCri = false;
//        int realDamage = playerCurrentAttack - commonEnemyData.enemyDefence;
//        int midasExtraDamage;

//        midasExtraDamage = passive_Midas ? Mathf.RoundToInt(Mathf.Min(Mathf.Max(2, realDamage), Mathf.Max(2, realDamage) * (currentMoney * 0.000025f))) : 0;

//        //realDamage += midasExtraDamage;

//        if (passive_David) realDamage *= playerAddHealth < commonEnemyData.enemyHealth ? 2 : 1;

//        posisonReducedDamage = Random.Range(1, 11);
        
//        if (passive_Critical) { if (posisonReducedDamage <= 1) enemycurrnetHealth -= Mathf.RoundToInt(Mathf.Max(2, (realDamage + midasExtraDamage) * 1.7f)); _isCri = true; }

//        else enemycurrnetHealth -= Mathf.Max(2, realDamage + midasExtraDamage);
        
//        playerCurrentHealth += passive_Boold ? Mathf.RoundToInt(Mathf.Max(2, (realDamage + midasExtraDamage) * 1.7f)) / 2 : 0;
//        playerCurrentHealth = playerCurrentHealth > playerAddHealth ? playerAddHealth : playerCurrentHealth;
//        _stateUI.UpdateStateText();

//        realDamage = Mathf.Max(2, realDamage);

//        if (enemycurrnetHealth <= 0)
//        {           
//            StartCoroutine(EnemyDead(realDamage / 2, _isCri));
//        }

//        else
//        {
//            if (!_isCri) _skillUI.SendMessage("OtherWriteText", $"당신은 상대방의 피를 {realDamage}(+{midasExtraDamage}) 만큼 깎았습니다. ");
//            else _skillUI.SendMessage("OtherWriteText", $"당신은 상대방의 피를 {Mathf.RoundToInt(Mathf.Max(2, (realDamage) * 1.7f))}(+{midasExtraDamage}) 만큼 깎았습니다. ");
//            EnemyTurn(realDamage / 2);
//        }
//    }

//    IEnumerator EnemyDead(int a = 0, bool critical = false)
//    {
//        StartCoroutine(EnemyHitParticle());

//        if (passive_Boold) { yield return new WaitForSeconds(2f); _skillUI.SendMessage("OtherWriteText", $"당신은 +{a}만큼 피를 빨았습니다. "); }
//        if (critical) { yield return new WaitForSeconds(2f); _skillUI.SendMessage("OtherWriteText", $"치명적인 공격!!!"); }
//        //if(critical)

//        yield return new WaitForSeconds(2f);

//        _skillUI.SendMessage("OtherWriteText", $"당신은 흥헹헹을 죽이고 {moneyValue * commonEnemyData.enemyMoney}원을 얻었습니다! ");
//        currentMoney += moneyValue * commonEnemyData.enemyMoney;
//        if (currentMoney >= 1000000)
//            PlayerBehave.instance.PlayerWin();
//        _stateUI.UpdateStateText();

//        yield return new WaitForSeconds(3f);
//        _skillUI._fightingEnemy = null;
//        _isPlayerTurn = true;
//        _backGround._enemyList.Remove(this);
//        Destroy(gameObject);
//        GameManager._playerState = PlayerState.IDLE;
//        pPos.EndBattle();
//        enemycurrnetHealth = commonEnemyData.enemyHealth;

//        _backGround.CreateEnemy();
//        yield return new WaitForSeconds(1.3f);
//        _skillUI.StartCoroutine("ShowButtons");
//    }

//    IEnumerator EnemyHitParticle()
//    {
//        yield return new WaitForSeconds(0.8f);
//        Debug.Log("이재엽이재엽이재엽");
//        SoundClips.instance.EffectSound(2);
//        Instantiate(_particleSystem[0], transform.position + Vector3.forward, Quaternion.identity);
//    }

//    public void EnemyTurn(int a)
//    {
//        EnemyAttack(a);
//        StartCoroutine(EnemyHitParticle());
//    }

//    void EnemyAttack(int a)
//    {
//        StartCoroutine(Attacking(a));
//    }

//    IEnumerator Attacking(int a)
//    {
//        int canAttack = Random.Range(0, passive_100m);
//        bool ispoison = false;

//        yield return new WaitForSeconds(2f);

//        if (passive_Boold) { _skillUI.SendMessage("OtherWriteText", $"당신은 +{a}만큼 피를 빨았습니다. "); _stateUI.UpdateStateText(); }

//        if (demageBlock)
//        {
//            _isPlayerTurn = false;
//            yield return new WaitForSeconds(3f);
//            _skillUI.SendMessage("OtherWriteText", $"당신은 비눗방울로 공격을 막았습니다! 비눗방울은 터졌습니다.");
//            yield return new WaitForSeconds(4.3f);

//            _skillUI.StartCoroutine("ShowButtons");
//            _stateUI.UpdateStateText();
//            demageBlock = false;
//            _isPlayerTurn = true;
//        }

//        else
//        {
//            if (canAttack == 0)
//            {
//                _isPlayerTurn = false;
//                yield return new WaitForSeconds(2f);
//                pPos._battleCamera.EnemyAttack();
//                _skillUI.SendMessage("OtherWriteText", $"흐헹헹(의)에 폭발펀치!! ");
//                yield return new WaitForSeconds(2.2f);

//                SoundClips.instance.EffectSound(3);
//                int enemyPower = Random.Range(commonEnemyData.enemyAttack - 3, Mathf.RoundToInt(commonEnemyData.enemyAttack * 1.4f));
//                int damage = Mathf.Max(1, enemyPower - playerCurrentDefence);

//                if (passive_Poison)
//                {
//                    posisonReducedDamage = Random.Range(1, 11);
//                    if (posisonReducedDamage <= 4) ispoison = true;
//                    damage = ispoison ? Mathf.RoundToInt(damage * 0.54f) : damage;
//                }
//                //damage = passive_Poison?posisonReducedDamage : 
//                playerCurrentHealth -= damage;
//                Instantiate(PlayerBehave.instance._hitEffect[0], transform);
//                PlayerBehave.instance.ani.SetTrigger("GetHit");
                
//                yield return new WaitForSeconds(1.2f);

//                if (!ispoison) _skillUI.SendMessage("OtherWriteText", $"흐헹헹(이)가 당신의 피를 {damage}만큼 깎았습니다.");
//                else _skillUI.SendMessage("OtherWriteText", $"[중독된] 흐헹헹(이)가 당신의 피를 {damage}만큼 깎았습니다.");


//                if (passive_Reflect) { yield return new WaitForSeconds(2f); Instantiate(_particleSystem[0], transform); _skillUI.SendMessage("OtherWriteText", $"흐헹헹은 당신을 때리다가 가시에 찔려 {Mathf.Max(damage / 4, 1)}의 데미지를 받았습니다."); }
//                enemycurrnetHealth -= Mathf.Max(damage / 4, 1);
//                if (enemycurrnetHealth <= 0) { StartCoroutine(EnemyDead()); yield return null; }

//                if (playerCurrentHealth <= 0)
//                {
//                    if (!passive_DemiGod)
//                    {
//                        yield return new WaitForSeconds(0.6f);
//                        _skillUI.SendMessage("OtherWriteText", $"헐... 당신은 흐헹헹에게 죽고 말았습니다!! ");
//                        yield return new WaitForSeconds(3.3f);
//                        _skillUI.SendMessage("OtherWriteText", $"풉.. 창피하지도 않으신가요? 당신은 모든 것을 잃었습니다. 처음으로 돌아갑니다");
//                        yield return new WaitForSeconds(4f);

//                        PlayerBehave.instance.PlayerDead();
//                        yield return null;
//                    }
//                    else
//                    {
//                        _skillUI.SendMessage("OtherWriteText", $"'신은 죽지 않는다'");
//                        playerCurrentHealth = 1;

//                        _skillUI.StartCoroutine("ShowButtons");

//                        yield return new WaitForSeconds(0.7f);
//                    }
//                }

//                else
//                {
//                    _stateUI.UpdateStateText();
//                    _isPlayerTurn = true;

//                    _skillUI.StartCoroutine("ShowButtons");

//                    yield return new WaitForSeconds(0.7f);
//                }
//            }

//            else if (canAttack == 1)
//            {
//                _isPlayerTurn = false;
//                yield return new WaitForSeconds(3f);
//                _skillUI.SendMessage("OtherWriteText", $"적은 당신에게 손이 닿지 않습니다! ");
//                yield return new WaitForSeconds(2f);

//                _skillUI.StartCoroutine("ShowButtons");
//                _stateUI.UpdateStateText();
//                _isPlayerTurn = true;
//            }
//            yield return new WaitForSeconds(0.5f);
//        }

//        yield return new WaitForSeconds(0.5f);
//        _skillUI.SendMessage("OtherWriteText", $"무엇을 하시겠습니까? ");
//    }
//}