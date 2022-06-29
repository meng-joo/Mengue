using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

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

    public GameObject[] _hitEffect;
    public GameObject _healEffect;

    public static PlayerBehave instance;

    public BattleEffect _battleEffect;
    private bool[] isPassiveOn = new bool[20];

    public BackGround _backGround;

    public Canvas[] _canvas;
    public Camera _battleCam;

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
        if(_playerState == PlayerState.INSTORE && !_isGacha && !_isStoresetting)
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
            {
                ExitStoreButton();
            }
        }
    }

    public void ExitStoreButton()
    {
        _storeUI.GetBackStoreUI();
        PassiveApply();
        _backGround.StartCoroutine("SpawnCoin");
    }

    public void PassiveApply()
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
                }
                else if(i == 1)
                {
                    int Ran = Random.Range(0, 3);
                    switch (Ran)
                    {
                        case 0:
                            playerAddHealth = Mathf.RoundToInt(playerAddHealth > playerHealth ? playerAddHealth * 1.1f : playerHealth * 1.1f);
                            break;
                        case 1:
                            playerCurrentAttack = Mathf.RoundToInt(playerCurrentAttack > playerAttack ? playerCurrentAttack * 1.1f : playerAttack * 1.1f);
                            break;
                        case 2:
                            playerCurrentDefence = Mathf.RoundToInt(playerCurrentDefence > playerDefence ? playerCurrentDefence * 1.1f : playerDefence * 1.1f);
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
                    playerCurrentHealth += Mathf.RoundToInt(playerHealth * 0.2f);
                    playerCurrentHealth = playerCurrentHealth > playerAddHealth ? playerAddHealth : playerCurrentHealth;
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
                    _storeUI._skillCountText[0].text = string.Format("{0}/{1}", _skillUI._skillCount[0], _skillUI.skillLimite);
                    _storeUI._skillCountText[1].text = string.Format("{0}/{1}", _skillUI._skillCount[0], _skillUI.skillLimite);
                }
                else if (i == 7)
                {
                    //방울 방패
                    passive_Bouble = true;
                }
                else if (i == 8)
                {
                    maincam.orthographicSize = 7.9f;
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
                    _backGround.coinSpawnDeley = 2.4f;
                    _backGround.maxCoinCount = 200;
                }
                else if (i == 13)
                {
                    passive_100m = 2;
                }
                else if (i == 14)
                {
                    //강강약약
                    passive_David = true;
                }
                else if (i == 15)
                {
                    //피해 흡혈
                    passive_Boold = true;
                }
                else if (i == 16)
                {
                    playerAddHealth = Mathf.RoundToInt(playerAddHealth > playerHealth ? playerAddHealth * 4f : playerHealth * 4f);
                    playerCurrentAttack = playerCurrentAttack > playerAttack ? playerCurrentAttack * 5 : playerAttack * 5;
                    playerCurrentDefence = playerCurrentDefence > playerDefence ? playerCurrentDefence * 5 : playerDefence * 5;
                    playerCurrentHealth = playerCurrentHealth + playerAddHealth * 3 > playerAddHealth ? playerAddHealth : playerCurrentHealth;
                }
                else if (i == 17)
                {
                    _storeUI.passive_Sale = 2;
                }
                else if (i == 18)
                {
                    passive_DemiGod = true;
                }
                else if (i == 19)
                {
                    passive_TheKing = true;
                }
            }

        }
        _stateUI.UpdateStateText();
    }

    protected override void InputPlayerMovingKey()
    {
        int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));

        if (horizontal != 0 && vertical != 0)
        {
            return;
        }

        Debug.Log($"{vertical}, {horizontal}");

        Vector3 inputTransform = new Vector3(horizontal, 0, vertical);
        if (inputTransform != Vector3.zero) transform.rotation = Quaternion.LookRotation(inputTransform);

        inputTransform.y = 0;
        
        if(transform.position.x + inputTransform.x >= BackGround.MaxX || transform.position.x + inputTransform.x <= BackGround.MinX || transform.position.z + inputTransform.z >= BackGround.MaxZ || transform.position.z + inputTransform.z <= BackGround.MinZ)
        {
            return;
        }

        characterController.Move(inputTransform);
    }
    
    //private void OnGUI()
    //{
    //    var labelStyle1 = new GUIStyle();
    //    var labelStyle2 = new GUIStyle();
    //    labelStyle1.fontSize = 20;
    //    labelStyle2.fontSize = 15;
    //    labelStyle1.normal.textColor = Color.red;
    //    labelStyle2.normal.textColor = Color.yellow;
    //    GUILayout.Label("\n\n현재 플레이어의 공격력 : " + playerCurrentAttack, labelStyle1);
    //    GUILayout.Label("현재 플레이어의 방어력 : " + playerCurrentDefence, labelStyle1);
    //    GUILayout.Label("현재 플레이어의 체력 : " + playerCurrentHealth, labelStyle1);
    //    //캐릭터 현재 돈
    //    GUILayout.Label("현재 가진 돈 : " + currentMoney, labelStyle2);
    //    //현재 돈의 가치
    //    GUILayout.Label("현재 돈의 가치 : " + moneyValue, labelStyle2);
    //}

    private void OnTriggerEnter(Collider collison)
    {
        if(collison.tag == "Coin")
        {
            _backGround.coinCount = _backGround.coinCount - 1;
            SoundClips.instance.EffectSound(0);

            currentMoney += moneyValue;

            if (currentMoney >= 1000000) PlayerWin();

            Destroy(collison.gameObject);
            _stateUI.UpdateStateText();
        }
        if (collison.tag == "Enemy")
        {
            SetBattle(collison.gameObject, 0);
            collison.gameObject.transform.parent.GetComponent<Enemy>().StartBattle(gameObject);
        }
        else if(collison.tag == "Boss")
        {
            SetBattle(collison.gameObject, 1);
            collison.gameObject.transform.parent.GetComponent<Boss>().StartBattle(gameObject);
        }
        if(collison.tag == "Store")
        {
            _backGround.StopCoroutine("SpawnCoin");
            _playerState = PlayerState.INSTORE;
            SoundClips.instance.StartCoroutine("SetStoreSound");
            _storeUI._skillCountText[0].text = string.Format("{0}/{1}", _skillUI._skillCount[0], _skillUI.skillLimite);
            _storeUI._skillCountText[1].text = string.Format("{0}/{1}", _skillUI._skillCount[1], _skillUI.skillLimite);
            StartCoroutine(PlayerInStore());
            _stateUI.UpdateStateText();
        }
    }

    void SetBattle(GameObject g, int i)
    {
        _boxCollider.enabled = false;
        _playerState = PlayerState.BATTLE;
        _skillUI.SetEnemy(g.transform.parent.gameObject);
        _enemy = g.gameObject;
        if (passive_Bouble) demageBlock = true;
        //_currentEnemy = _enemy.transform.parent.GetComponent<Enemy>();
        StartCoroutine(PlayerFindEnemyToBattle(i));
    }

    IEnumerator PlayerFindEnemyToBattle(int i)
    {
        exclamationMark.gameObject.SetActive(true);
        SoundClips.instance.EffectSound(6);

        yield return new WaitForSeconds(1f);

        _battleEffect.SetProfile(i);
        if (i == 0) SoundClips.instance.StartCoroutine("SetBattleSound");
        else SoundClips.instance.StartCoroutine("SetBossBattleSound");

        yield return new WaitForSeconds(5f);

        characterController.enabled = false;
        exclamationMark.gameObject.SetActive(false);
        _battleCamera.gameObject.SetActive(true);
        SetBattleCanvasCamera();
        StartBattle(i);
        ani.SetTrigger("Battle");
        _skillUI.ViewSkillUI();
    }

    IEnumerator PlayerInStore()
    {
        _storeUI.SetStoreUI();

        yield return null;
    }

    private void StartBattle(int i)
    {
        Debug.Log(transform.position);
        transform.position += new Vector3(0, 0.5f, 0);
        Quaternion quaternion = Quaternion.Euler(new Vector3(75f , 0f , 0f));
        if (i == 0) transform.LookAt(_enemy.transform);
        else transform.LookAt(_enemy.transform.position - new Vector3(0, 0.8f, 0f));
        transform.rotation *= quaternion;
        Debug.Log(transform.position);
    }

    public void PlayerDead()
    {
        //아직 아무것도 없음
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        yield return new WaitForSeconds(1.8f);
        _battleCamera.gameObject.SetActive(false);
        SetMainCanvasCamera();
        Vector3 currentPos = new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z));
        ani.ResetTrigger("Battle");
        ani.SetTrigger("NoBattle");
        _boxCollider.enabled = true;
        SoundClips.instance.StartCoroutine("StartSound");

        if (passive_Bouble) demageBlock = true;

        yield return new WaitForSeconds(0.9f);
        transform.localPosition = currentPos;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        characterController.enabled = true;
        _stateUI.UpdateStateText();
    }

    public void PlayerWin()
    {
        SceneManager.LoadScene("END");
    }

    void SetBattleCanvasCamera()
    {
        for(int i=0;i<_canvas.Length;i++)
        {
            _canvas[i].worldCamera = _battleCam;
        }
    }

    void SetMainCanvasCamera()
    {
        for (int i = 0; i < _canvas.Length; i++)
        {
            _canvas[i].worldCamera = Camera.main;
        }
    }
}
