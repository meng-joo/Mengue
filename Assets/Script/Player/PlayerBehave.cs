using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerBehave : MonoBehaviour
{
    public Camera maincam;
    private GameObject _enemy = null;
    public PassiveData _passiveData;

    public BattleCamera _battleCamera = null;
    [Range(1, 60f)]
    public float moveSpeed = 20;
    private TextMeshPro exclamationMark;
    public Animator ani;
    public SkillUI _skillUI;
    private BoxCollider _boxCollider;
    public StoreUI _storeUI;
    public StateUI _stateUI;

    public PlayerDataSO _playerDataSo;

    public UnityEvent _PlayerMove;
    public UnityEvent _PlayerBattle;

    public GameObject[] _hitEffect;
    public GameObject _healEffect;

    public static PlayerBehave instance;

    public BattleEffect _battleEffect;
    private bool[] isPassiveOn = new bool[20];

    public Setting _setting;

    public Canvas[] _canvas;
    public Camera _battleCam;

    public float moveDelay = 0.01f;
    public float currentDelay = 0.01f;

    public bool _isCave = false;

    public enum PlayerState
    {
        NONE,
        IDLE,
        MOVING,
        BATTLE,
        INSTORE,
        INSETTING
    }

    public static PlayerState _playerState = PlayerState.IDLE;

    private void Awake()
    {
        instance = this;

        if (_setting == null) _setting = FindObjectOfType<Setting>();
    }

    private void Start()
    {
        exclamationMark = transform.Find("ExclamationMark").GetComponent<TextMeshPro>();
        _passiveData = transform.Find("PassiveData").GetComponent<PassiveData>();
        ani = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
        for (int i = 0; i < isPassiveOn.Length; i++)
        {
            isPassiveOn[i] = false;
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_playerState == PlayerState.IDLE && _playerState != PlayerState.INSTORE && _playerState != PlayerState.INSETTING)
        {
            InputPlayerMovingKey();
        }

        if (_playerState == PlayerState.INSTORE && !GameManager._isGacha && !GameManager._isStoresetting)
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
            {
                ExitStoreButton();
            }
        }
    }

    public void ExitStoreButton()
    {
        //_storeUI.GetBackStoreUI();
        _setting.StartCoroutine("SpawnCoin");
        //_stateUI.UpdateStateText();
    }

    private void InputPlayerMovingKey()
    {
        if(currentDelay > 0)
        {
            currentDelay -= Time.deltaTime;
            return;
        }

        currentDelay = moveDelay;

        int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));

        if ((horizontal != 0 && vertical != 0) || (horizontal == 0 && vertical == 0))
        {
            return;
        }

        Vector3 inputTransform = new Vector3(horizontal, 0, vertical);
        if (inputTransform != Vector3.zero) transform.rotation = Quaternion.LookRotation(inputTransform);

        inputTransform.y = 0;

        if (!_isCave)
        {
            if (transform.position.x + inputTransform.x >= Setting.MaxX || transform.position.x + inputTransform.x <= Setting.MinX || transform.position.z + inputTransform.z >= Setting.MaxZ || transform.position.z + inputTransform.z <= Setting.MinZ)
            {
                return;
            }
        }
        else
        {
            if (transform.position.x + inputTransform.x >= Setting.CaveMaxX || transform.position.x + inputTransform.x <= Setting.CaveMinX || transform.position.z + inputTransform.z >= Setting.CaveMaxZ || transform.position.z + inputTransform.z <= Setting.CaveMinZ)
            {
                return;
            }
        }

        transform.position += inputTransform;

        IfSpecialButton();
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

    //private void OnTriggerEnter(Collider collison)
    //{
    //    if(collison.tag == "Coin")
    //    {
    //        _backGround.coinCount = _backGround.coinCount - 1;
    //        SoundClips.instance.EffectSound(0);

    //        currentMoney += moneyValue;

    //        if (currentMoney >= 1000000) PlayerWin();

    //        Destroy(collison.gameObject);
    //        _stateUI.UpdateStateText();
    //    }
    //    if (collison.tag == "Enemy")
    //    {
    //        SetBattle(collison.gameObject, 0);
    //        collison.gameObject.transform.parent.GetComponent<CommonEnemy>().StartBattle(gameObject);
    //    }
    //    else if(collison.tag == "Boss")
    //    {
    //        SetBattle(collison.gameObject, 1);
    //        collison.gameObject.transform.parent.GetComponent<Boss>().StartBattle(gameObject);
    //    }
    //    if(collison.tag == "Store")
    //    {
    //        _backGround.StopCoroutine("SpawnCoin");
    //        _playerState = PlayerState.INSTORE;
    //        SoundClips.instance.StartCoroutine("SetStoreSound");
    //        _storeUI._skillCountText[0].text = string.Format("{0}/{1}", _skillUI._skillCount[0], _skillUI.skillLimite);
    //        _storeUI._skillCountText[1].text = string.Format("{0}/{1}", _skillUI._skillCount[1], _skillUI.skillLimite);
    //        StartCoroutine(PlayerInStore());
    //        _stateUI.UpdateStateText();
    //    }
    //}

    void IfSpecialButton()
    {
        for (int i = 0; i < GameManager.instance.blockInfo.Count; i++)
        {
            if (GameManager.instance.blockInfo[i]._x == transform.position.x && GameManager.instance.blockInfo[i]._z == transform.position.z)
            {
                if(GameManager.instance.blockInfo[i].isStore)
                {
                    SoundClips.instance.StartCoroutine("SetStoreSound");
                    //_storeUI._skillCountText[0].text = string.Format("{0}/{1}", _skillUI._skillCount[0], _skillUI.skillLimite);
                    //_storeUI._skillCountText[1].text = string.Format("{0}/{1}", _skillUI._skillCount[1], _skillUI.skillLimite);
                    PlayerInStore();
                    //_stateUI.UpdateStateText();
                }
                else if (GameManager.instance.blockInfo[i].is_Setting)
                {
                    
                }
                else if (GameManager.instance.blockInfo[i].is_Stair)
                {
                    _PlayerMove.Invoke();
                }
                else if (GameManager.instance.blockInfo[i].isCommon_Enemy)
                {

                }
                else if (GameManager.instance.blockInfo[i].is_Boss)
                {

                }
                Debug.Log("상점입니다.");
            }
        }
    }

    void SetBattle(GameObject g, int i)
    {
        _boxCollider.enabled = false;
        _playerState = PlayerState.BATTLE;
        //_skillUI.SetEnemy(g.transform.parent.gameObject);
        _enemy = g.gameObject;
        //if (passive_Bouble) demageBlock = true;
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

        exclamationMark.gameObject.SetActive(false);
        _battleCamera.gameObject.SetActive(true);
        SetBattleCanvasCamera();
        StartBattle(i);
        ani.SetTrigger("Battle");
       // _skillUI.ViewSkillUI();
    }

    void PlayerInStore()
    {
        _playerState = PlayerState.INSTORE;
        _storeUI.SetStoreUI();
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

    //public void EndBattle()
    //{
    //    StartCoroutine(EndBattleSettting());
    //}

    //IEnumerator EndBattleSettting()
    //{
    //    _skillUI.HideSkillUI();
    //    yield return new WaitForSeconds(1.8f);
    //    _battleCamera.gameObject.SetActive(false);
    //    SetMainCanvasCamera();
    //    Vector3 currentPos = new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z));
    //    ani.ResetTrigger("Battle");
    //    ani.SetTrigger("NoBattle");
    //    _boxCollider.enabled = true;
    //    SoundClips.instance.StartCoroutine("StartSound");

    //    //if (passive_Bouble) demageBlock = true;

    //    yield return new WaitForSeconds(0.9f);
    //    transform.localPosition = currentPos;
    //    transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
    //    characterController.enabled = true;
    //    _stateUI.UpdateStateText();
    //}

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
