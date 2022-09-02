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

//        Debug.Log("���� ��׶���" + _backGround);
//        if (_backGround == null) _backGround = FindObjectOfType<Setting>();
//        Debug.Log("�Ŀ� ��׶���" + _backGround);

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
//            if (!_isCri) _skillUI.SendMessage("OtherWriteText", $"����� ������ �Ǹ� {realDamage}(+{midasExtraDamage}) ��ŭ ��ҽ��ϴ�. ");
//            else _skillUI.SendMessage("OtherWriteText", $"����� ������ �Ǹ� {Mathf.RoundToInt(Mathf.Max(2, (realDamage) * 1.7f))}(+{midasExtraDamage}) ��ŭ ��ҽ��ϴ�. ");
//            EnemyTurn(realDamage / 2);
//        }
//    }

//    IEnumerator EnemyDead(int a = 0, bool critical = false)
//    {
//        StartCoroutine(EnemyHitParticle());

//        if (passive_Boold) { yield return new WaitForSeconds(2f); _skillUI.SendMessage("OtherWriteText", $"����� +{a}��ŭ �Ǹ� ���ҽ��ϴ�. "); }
//        if (critical) { yield return new WaitForSeconds(2f); _skillUI.SendMessage("OtherWriteText", $"ġ������ ����!!!"); }
//        //if(critical)

//        yield return new WaitForSeconds(2f);

//        _skillUI.SendMessage("OtherWriteText", $"����� �������� ���̰� {moneyValue * commonEnemyData.enemyMoney}���� ������ϴ�! ");
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
//        Debug.Log("���翱���翱���翱");
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

//        if (passive_Boold) { _skillUI.SendMessage("OtherWriteText", $"����� +{a}��ŭ �Ǹ� ���ҽ��ϴ�. "); _stateUI.UpdateStateText(); }

//        if (demageBlock)
//        {
//            _isPlayerTurn = false;
//            yield return new WaitForSeconds(3f);
//            _skillUI.SendMessage("OtherWriteText", $"����� �񴰹��� ������ ���ҽ��ϴ�! �񴰹���� �������ϴ�.");
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
//                _skillUI.SendMessage("OtherWriteText", $"������(��)�� ������ġ!! ");
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

//                if (!ispoison) _skillUI.SendMessage("OtherWriteText", $"������(��)�� ����� �Ǹ� {damage}��ŭ ��ҽ��ϴ�.");
//                else _skillUI.SendMessage("OtherWriteText", $"[�ߵ���] ������(��)�� ����� �Ǹ� {damage}��ŭ ��ҽ��ϴ�.");


//                if (passive_Reflect) { yield return new WaitForSeconds(2f); Instantiate(_particleSystem[0], transform); _skillUI.SendMessage("OtherWriteText", $"�������� ����� �����ٰ� ���ÿ� ��� {Mathf.Max(damage / 4, 1)}�� �������� �޾ҽ��ϴ�."); }
//                enemycurrnetHealth -= Mathf.Max(damage / 4, 1);
//                if (enemycurrnetHealth <= 0) { StartCoroutine(EnemyDead()); yield return null; }

//                if (playerCurrentHealth <= 0)
//                {
//                    if (!passive_DemiGod)
//                    {
//                        yield return new WaitForSeconds(0.6f);
//                        _skillUI.SendMessage("OtherWriteText", $"��... ����� �����󿡰� �װ� ���ҽ��ϴ�!! ");
//                        yield return new WaitForSeconds(3.3f);
//                        _skillUI.SendMessage("OtherWriteText", $"Ǳ.. â�������� �����Ű���? ����� ��� ���� �Ҿ����ϴ�. ó������ ���ư��ϴ�");
//                        yield return new WaitForSeconds(4f);

//                        PlayerBehave.instance.PlayerDead();
//                        yield return null;
//                    }
//                    else
//                    {
//                        _skillUI.SendMessage("OtherWriteText", $"'���� ���� �ʴ´�'");
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
//                _skillUI.SendMessage("OtherWriteText", $"���� ��ſ��� ���� ���� �ʽ��ϴ�! ");
//                yield return new WaitForSeconds(2f);

//                _skillUI.StartCoroutine("ShowButtons");
//                _stateUI.UpdateStateText();
//                _isPlayerTurn = true;
//            }
//            yield return new WaitForSeconds(0.5f);
//        }

//        yield return new WaitForSeconds(0.5f);
//        _skillUI.SendMessage("OtherWriteText", $"������ �Ͻðڽ��ϱ�? ");
//    }
//}