using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerBehave : Moving
{
    public Camera maincam;
    private GameObject _enemy = null;
    public Enemy _currentEnemy = null;
    public PassiveData _passiveData;

    public BattleCamera _battleCamera = null;
    private CharacterController characterController;
    [Range(1, 60f)]
    public float moveSpeed = 20;
    private TextMeshPro exclamationMark;
    public Animator ani;
    public SkillUI _skillUI;
    private BoxCollider _boxCollider;
    public StoreUI _storeUI;
    public StateUI _stateUI;

    public GameObject _hitEffect;
    public GameObject _healEffect;

    public static PlayerBehave instance;

    public BattleEffect _battleEffect;
    private bool[] isPassiveOn = new bool[20];

    public BackGround _backGround;
    private void Awake()
    {
        instance = this;

        Debug.Log("전에 백그라운드" + _backGround);
        if (_backGround == null) _backGround = FindObjectOfType<BackGround>();
        Debug.Log("후에 백그라운드" + _backGround);
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        exclamationMark = transform.Find("ExclamationMark").GetComponent<TextMeshPro>();
        _passiveData = transform.Find("PassiveData").GetComponent<PassiveData>();
        ani = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
        for (int i = 0; i < isPassiveOn.Length; i++)
        {
            isPassiveOn[i] = false;
        }
    }

    protected override void Move()
    {
        base.Move();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("aa");
        }

        if(_playerState == PlayerState.INSTORE && !_isGacha && !_isStoresetting)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                _storeUI.GetBackStoreUI();
                PassiveApply();
                _backGround.StartCoroutine("SpawnCoin");
            }
        }
    }

    void PassiveApply()
    {
        playerAddHealth = playerHealth;
        playerCurrentDefence = playerDefence;
        playerCurrentAttack = playerAttack;

        for (int i = 0; i < _passiveData.activeItem.Count; i++) {
            if(_passiveData.activeItem[i].isactive == true)
            {
                if (i == 0)
                {
                    playerCurrentAttack = Mathf.RoundToInt(playerAttack * 1.15f);
                    //isPassiveOn[i] = true;
                }
                else if(i == 1)
                {
                    int Ran = Random.Range(0, 3);
                    switch (Ran)
                    {
                        case 0:
                            playerAddHealth = Mathf.RoundToInt(playerAddHealth > playerHealth ? playerAddHealth * 2.5f : playerHealth * 2.5f);
                            break;
                        case 1:
                            playerCurrentAttack = playerCurrentAttack > playerAttack ? playerCurrentAttack * 3 : playerAttack * 3;
                            break;
                        case 2:
                            playerCurrentDefence = playerCurrentDefence > playerDefence ? playerCurrentDefence * 2 : playerDefence * 2;
                            break;
                    }
                }
                else if (i == 2)
                {
                    passive_Critical = true;
                }
                else if (i == 3)
                {
                    playerAddHealth = Mathf.RoundToInt(playerHealth * 1.2f);
                }
                else if (i == 4) // 
                {
                    _skillUI.runvalue = 5;
                }
                else if (i == 5) // 방어증가
                {
                    playerCurrentDefence = Mathf.RoundToInt(playerCurrentDefence > playerDefence ? playerCurrentDefence * 1.15f : playerDefence * 1.15f);
                }
                else if (i == 6) //가방
                {
                    _skillUI.skillLimite = 24;
                }
                else if (i == 7)
                {
                    //방울 방패
                    passive_Bouble = true;
                }
                else if (i == 8)
                {
                    maincam.orthographicSize = 7;
                }
                else if (i == 9)
                {
                    //미다스의 힘
                    passive_Midas = true;
                }
                else if (i == 10)
                {
                    //독
                    passive_Poison = true;
                }
                else if (i == 11)
                {
                    //가시방패
                    passive_Reflect = true;
                }
                else if (i == 12)
                {
                    _backGround.coinSpawnDeley = 8f;
                }
                else if (i == 13)
                {
                    passive_100m = 2;
                }
                else if (i == 14)
                {
                    //강강약약
                    passive_Reflect = true;
                }
                else if (i == 15)
                {
                    //피해 흡혈
                    passive_Boold = true;
                }
                else if (i == 16)
                {
                    playerAddHealth = Mathf.RoundToInt(playerAddHealth > playerHealth ? playerAddHealth * 3f : playerHealth * 3f);
                    playerCurrentAttack = playerCurrentAttack > playerAttack ? playerCurrentAttack * 5 : playerAttack * 5;
                    playerCurrentDefence = playerCurrentDefence > playerDefence ? playerCurrentDefence * 5 : playerDefence * 5;
                }
                else if (i == 17)
                {
                    _storeUI.passive_Sale = 2;
                }
                else if (i == 18)
                {

                }
                else if (i == 19)
                {

                }
            }
        }
    }

    protected override void InputPlayerMovingKey()
    {
        //ChangeStateToMove();
        int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));

        if (horizontal != 0 && vertical != 0)
        {
            return;
        }

        Debug.Log($"{vertical}, {horizontal}");

        Vector3 inputTransform = new Vector3(horizontal, 0, vertical);
        if (inputTransform != Vector3.zero) transform.rotation = Quaternion.LookRotation(inputTransform);

        //gravityVec = new Vector3(0, gravityScale, 0);
        inputTransform.y = 0;
        //Vector3 pos = inputTransform;

        //Vector3 finalpos = inputTransform * Time.deltaTime;

        if(transform.position.x + inputTransform.x >= BackGround.MaxX || transform.position.x + inputTransform.x <= BackGround.MinX || transform.position.z + inputTransform.z >= BackGround.MaxZ || transform.position.z + inputTransform.z <= BackGround.MinZ)
        {
            return;
        }

        //Vector3 pos = Vector3.Lerp(transform.position, inputTransform, 0.6f * Time.deltaTime);

        characterController.Move(inputTransform);
    }
    
    private void OnGUI()
    {
        var labelStyle1 = new GUIStyle();
        var labelStyle2 = new GUIStyle();
        labelStyle1.fontSize = 20;
        labelStyle2.fontSize = 15;
        labelStyle1.normal.textColor = Color.red;
        labelStyle2.normal.textColor = Color.yellow;
        GUILayout.Label("\n\n현재 플레이어의 공격력 : " + playerCurrentAttack, labelStyle1);
        GUILayout.Label("현재 플레이어의 방어력 : " + playerCurrentDefence, labelStyle1);
        GUILayout.Label("현재 플레이어의 체력 : " + playerCurrentHealth, labelStyle1);
        //캐릭터 현재 돈
        GUILayout.Label("현재 가진 돈 : " + currentMoney, labelStyle2);
        //현재 돈의 가치
        GUILayout.Label("현재 돈의 가치 : " + moneyValue, labelStyle2);
    }

    private void OnTriggerEnter(Collider collison)
    {
        if(collison.tag == "Coin")
        {
            Debug.Log("00000000000 : " + _backGround);
            _backGround.coinCount = _backGround.coinCount - 1;


            currentMoney += moneyValue;
            Destroy(collison.gameObject);
            _stateUI.UpdateStateText();
        }
        if (collison.tag == "Enemy")
        {
            SetBattle(collison.gameObject, 0);
        }
        if(collison.tag == "Boss")
        {
            SetBattle(collison.gameObject, 1);
        }
        if(collison.tag == "Store")
        {
            _backGround.StopCoroutine("SpawnCoin");
            _playerState = PlayerState.INSTORE;
            StartCoroutine(PlayerInStore());
            _stateUI.UpdateStateText();
        }
    }

    void SetBattle(GameObject g, int i)
    {
        _playerState = PlayerState.BATTLE;
        _stateUI.settingButton.enabled = false;
        _skillUI.SetEnemy(g.transform.parent.gameObject);
        _boxCollider.enabled = false;
        _enemy = g.gameObject;
        if (passive_Bouble) demageBlock = true;
        //_currentEnemy = _enemy.transform.parent.GetComponent<Enemy>();
        StartCoroutine(PlayerFindEnemyToBattle(i));
    }

    IEnumerator PlayerFindEnemyToBattle(int i)
    {
        exclamationMark.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        _battleEffect.SetProfile(i);

        yield return new WaitForSeconds(5f);

        characterController.enabled = false;
        exclamationMark.gameObject.SetActive(false);
        _battleCamera.gameObject.SetActive(true);
        StartBattle();
        ani.SetTrigger("Battle");
        _skillUI.ViewSkillUI();
    }

    IEnumerator PlayerInStore()
    {
        _storeUI.SetStoreUI();

        yield return null;
    }

    private void StartBattle()
    {
        //_battleCamera.SettingBattleCam();
        Debug.Log(transform.position);
        transform.position += new Vector3(0, 0.5f, 0);
        Quaternion quaternion = Quaternion.Euler(new Vector3(75f , 0f , 0f));
        //transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 1);
        //transform.localEulerAngles = new Vector3(75, 0, 0);
        transform.LookAt(_enemy.transform);
        transform.rotation *= quaternion;
        Debug.Log(transform.position);
        //ani.SetTrigger("Battle");
    }

    public void PlayerDead()
    {
        //아직 아무것도 없음
    }

    public void ChangeStateToMove()
    {
        if(_playerState == PlayerState.IDLE)
        {
            _playerState = PlayerState.MOVING;
            return;
        }
        _playerState = PlayerState.IDLE;
    }

    public void EndBattle()
    {
        StartCoroutine(EndBattleSettting());
    }

    IEnumerator EndBattleSettting()
    {
        _skillUI.HideSkillUI();
        //_battleCamera.SettingBattleCam();
        _battleCamera.gameObject.SetActive(false);
        //transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        Vector3 currentPos = new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z));
        ani.ResetTrigger("Battle");
        ani.SetTrigger("NoBattle");
        _boxCollider.enabled = true;

        _stateUI.settingButton.enabled = false;

        yield return new WaitForSeconds(1.1f);
        //for (int i = 0; i < _backGround._enemyList.Count; i++)
        //{
        //    _backGround._enemyList[i].gameObject.SetActive(true);
        //}
        transform.localPosition = currentPos;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        characterController.enabled = true;
    }
}
