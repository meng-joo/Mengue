//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;

//public class Boss : Enemy
//{
//    public EnemyDataSO bossData;
//    public GameObject enemyView;
//    private SkillUI _skillUI;
//    private StateUI _stateUI;

//    public PlayerBehave pPos;
//    public ParticleSystem[] _particleSystem;

//    private Animator _animator;

//    public Vector3 tempVec;

//    int posisonReducedDamage = 0;

//    protected Setting _backGround;
//    int healCount = 0;

//    public int bosscurrnetHealth;

//    private void Start()
//    {
//        _animator = GetComponent<Animator>();
//        _stateUI = FindObjectOfType<StateUI>();
//        pPos = GameObject.Find("Player").GetComponent<PlayerBehave>();
//        _skillUI = FindObjectOfType<SkillUI>();
//        tempVec = transform.position;
//        bosscurrnetHealth = bossData.enemyHealth;
//        if (_backGround == null) _backGround = FindObjectOfType<Setting>();
//    }
    

//    public void StartBattle(GameObject player)
//    {
//        StartCoroutine(BattleBoss(player));
//    }

//    IEnumerator BattleBoss(GameObject player)
//    {

//        enemyView.SetActive(false);
//        yield return new WaitForSeconds(0.2f);
//        Quaternion quaternion = Quaternion.Euler(60, 0, 0);
//        transform.position = player.transform.position + new Vector3(1, 1.5f, 1);
//        //transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 1);
//        bosscurrnetHealth = bossData.enemyHealth;
//        transform.LookAt(player.transform);
//        transform.rotation *= quaternion;
//    }

//    public void GetAttack()
//    {
//        bool _isCri = false;
//        int realDamage = PlayerBehave.instance._playerDataSo.playerCurrentAttack - bossData.enemyDefence;
//        int midasExtraDamage;

//        midasExtraDamage = passive_Midas ? Mathf.RoundToInt(Mathf.Min(Mathf.Max(2, realDamage), Mathf.Max(2, realDamage) * (currentMoney * 0.000025f))) : 0;

//        //realDamage += midasExtraDamage;

//        if (passive_David) realDamage *= playerAddHealth < bossData.enemyHealth ? 2 : 1;

//        posisonReducedDamage = Random.Range(1, 11);

//        SoundClips.instance.EffectSound(2);

//        if (passive_Critical) { if (posisonReducedDamage <= 1) { bosscurrnetHealth -= Mathf.RoundToInt(Mathf.Max(2, (realDamage + midasExtraDamage) * 1.7f)); _isCri = true; } }

//        else bosscurrnetHealth -= Mathf.Max(2, (realDamage + midasExtraDamage));

//        playerCurrentHealth += passive_Boold ? Mathf.RoundToInt(Mathf.Max(2, (realDamage + midasExtraDamage) * 1.7f)) / 2 : 0;
//        PlayerBehave.instance._playerDataSo.playerCurrentHealth = PlayerBehave.instance._playerDataSo.playerCurrentHealth > PlayerBehave.instance._playerDataSo.playerAddHealth ? PlayerBehave.instance._playerDataSo.playerAddHealth : PlayerBehave.instance._playerDataSo.playerCurrentHealth;
//        _stateUI.UpdateStateText();

//        realDamage = Mathf.Max(2, realDamage);

//        if (bosscurrnetHealth <= 0)
//        {
//            StartCoroutine(EnemyDead(realDamage / 2, _isCri));
//        }

//        else
//        {
//            if(!_isCri) _skillUI.SendMessage("OtherWriteText", $"����� �ǽ��� �Ǹ� {realDamage}(+{midasExtraDamage}) ��ŭ ��ҽ��ϴ�. ");
//            else _skillUI.SendMessage("OtherWriteText", $"ũ��Ƽ��! ����� �ǽ��� �Ǹ� {Mathf.RoundToInt(realDamage * 1.7f)}(+{Mathf.RoundToInt(Mathf.Max(2, (realDamage + midasExtraDamage) * 1.7f)) - Mathf.RoundToInt(realDamage * 1.7f)}) ��ŭ ��ҽ��ϴ�. ");
//            EnemyTurn(realDamage / 2);
//        }
//    }

//    IEnumerator EnemyDead(int a = 0, bool critical = false)
//    {
//        StartCoroutine(EnemyHitParticle());

//        if (passive_Boold) { yield return new WaitForSeconds(2f); _skillUI.SendMessage("OtherWriteText", $"����� +{a}��ŭ �Ǹ� ���ҽ��ϴ�. "); }
//        if (critical) { yield return new WaitForSeconds(2f); _skillUI.SendMessage("OtherWriteText", $"ġ������ ����!!!"); }

//        yield return new WaitForSeconds(2f);

//        _skillUI.SendMessage("OtherWriteText", $"����� �ǽ��� ���̰� {moneyValue * bossData.enemyMoney}���� ������ϴ�! ");
//        currentMoney += moneyValue * bossData.enemyMoney;

//        if (currentMoney >= 1000000)
//            PlayerBehave.instance.PlayerWin();

//        _stateUI.UpdateStateText();

//        yield return new WaitForSeconds(3f);
//        _skillUI._fightingBoss = null;
//        _isPlayerTurn = true;
//        _backGround._bossList.Remove(this);
//        Destroy(gameObject);
//        GameManager._playerState = PlayerState.IDLE;
//        pPos.EndBattle();

//        bosscurrnetHealth = bossData.enemyHealth;

//        _backGround.CreateBoss();

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
//        if (bosscurrnetHealth < bossData.enemyHealth / 3 && healCount < 2)
//        {
//            StartCoroutine(BossHeal(a));
//        }
//        else EnemyAttack(a);

//        StartCoroutine(EnemyHitParticle());
//    }

//    IEnumerator BossHeal(int a)
//    {
//        _isPlayerTurn = false;

//        if (passive_Boold)
//        {
//            yield return new WaitForSeconds(2f);
//            _skillUI.SendMessage("OtherWriteText", $"����� +{a}��ŭ �Ǹ� ���ҽ��ϴ�. ");
//        }

//        yield return new WaitForSeconds(1.8f);

//        _skillUI.SendMessage("OtherWriteText", $"�ǽ��� ���⸦ ���� �Ǹ� {Mathf.RoundToInt(bossData.enemyAttack * 2 - bossData.enemyAttack / 3)}ȸ���Ͽ����ϴ�!");
//        bosscurrnetHealth += bossData.enemyAttack * 2 - bossData.enemyAttack / 3;

//        //pPos._battleCamera.EnemyAttack();
//        pPos._battleCamera.BossHeal();

//        if (bosscurrnetHealth > bossData.enemyHealth) bosscurrnetHealth = bossData.enemyHealth;

//        yield return new WaitForSeconds(1f);
//        Instantiate(_particleSystem[2], transform.position + Vector3.forward, Quaternion.identity);

//        _stateUI.UpdateStateText();
//        healCount++;
//        yield return new WaitForSeconds(2.2f);
//        _skillUI.StartCoroutine("ShowButtons");
//        yield return new WaitForSeconds(0.3f);
//        _isPlayerTurn = true;
//        _skillUI.SendMessage("OtherWriteText", $"������ �Ͻðڽ��ϱ�? ");
//    }

//    void EnemyAttack(int a)
//    {
//        StartCoroutine(Attacking(a));
//        healCount = 0;
//    }

//    IEnumerator Attacking(int a)
//    {
//        int canAttack = Random.Range(0, passive_100m);
//        bool ispoison = false;

//        if (passive_Boold)
//        {
//            yield return new WaitForSeconds(2f);
//            _skillUI.SendMessage("OtherWriteText", $"����� +{a}��ŭ �Ǹ� ���ҽ��ϴ�. ");
//        }

//        if (demageBlock)
//        {
//            _isPlayerTurn = false;
//            yield return new WaitForSeconds(2f);
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
//                yield return new WaitForSeconds(3f);
//                pPos._battleCamera.EnemyAttack();
//                _skillUI.SendMessage("OtherWriteText", $"�ǽ�(��)�� �����!! ");
//                yield return new WaitForSeconds(2.2f);

//                SoundClips.instance.EffectSound(4);
//                int enemyPower = Random.Range(bossData.enemyAttack - 3, Mathf.RoundToInt(bossData.enemyAttack * 1.7f));
//                int damage = Mathf.Max(3, enemyPower - playerCurrentDefence);

//                posisonReducedDamage = Random.Range(1, 11);
//                if (posisonReducedDamage < 4) ispoison = true;
//                damage = ispoison ? Mathf.RoundToInt(damage * 0.54f) : damage;

//                //damage = passive_Poison?posisonReducedDamage : 
//                playerCurrentHealth -= damage;
//                Instantiate(PlayerBehave.instance._hitEffect[1], transform);
                
//                PlayerBehave.instance.ani.SetTrigger("GetHit");
//                yield return new WaitForSeconds(1.2f);

//                if (!passive_Poison) _skillUI.SendMessage("OtherWriteText", $"�ǽ�(��)�� ����� �Ǹ� {damage}��ŭ ��ҽ��ϴ�.");
//                else _skillUI.SendMessage("OtherWriteText", $"[�ߵ���] �ǽ�(��)�� ����� �Ǹ� {damage}��ŭ ��ҽ��ϴ�.");

//                if (passive_Reflect) _skillUI.SendMessage("OtherWriteText", $"�ǽ��� ����� �����ٰ� ���ÿ� ��� {Mathf.Max(damage / 4, 2)}�� �������� �޾ҽ��ϴ�.");
//                bosscurrnetHealth -= Mathf.Max(damage / 4, 2);
//                if (bosscurrnetHealth <= 0) { StartCoroutine(EnemyDead()); yield return null; }


//                yield return new WaitForSeconds(2.1f);
//                _skillUI.StartCoroutine("ShowButtons");

//                if (playerCurrentHealth <= 0)
//                {
//                    if (!passive_DemiGod)
//                    {
//                        _skillUI.SendMessage("OtherWriteText", $"ũ�ƾƾƾ�...!! ����� �ǽ��� ���⿡ �°� Ÿ�׾���Ƚ��ϴ�!!");
//                        yield return new WaitForSeconds(3.4f);
//                        _skillUI.SendMessage("OtherWriteText", $"��Ÿ������ ����� ��� ���� �Ұ� ó������ ���ư��ϴ�.");
//                        yield return new WaitForSeconds(2.2f);
//                        PlayerBehave.instance.PlayerDead();
//                        yield return null;
//                    }
//                    else
//                    {
//                        _skillUI.SendMessage("OtherWriteText", $"'���� ���� �ʴ´�'");
//                        playerCurrentHealth = 1;
//                    }
//                }

//                else
//                {
//                    _stateUI.UpdateStateText();
//                    _isPlayerTurn = true;
//                }
//            }

//            else if (canAttack == 1)
//            {
//                _isPlayerTurn = false;
//                yield return new WaitForSeconds(3f);
//                _skillUI.SendMessage("OtherWriteText", $"�ǽ��� ��ſ��� ���� ���� �ʽ��ϴ�! ");
//                yield return new WaitForSeconds(2f);

//                _skillUI.StartCoroutine("ShowButtons");
//                _stateUI.UpdateStateText();
//                _isPlayerTurn = true;
//            }
//        }

//        yield return new WaitForSeconds(0.5f);
//        _skillUI.SendMessage("OtherWriteText", $"������ �Ͻðڽ��ϱ�? ");
//    }
//}
