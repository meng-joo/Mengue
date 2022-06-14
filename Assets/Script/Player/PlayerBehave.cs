using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerBehave : Moving
{
    private GameObject _enemy = null;
    public BattleCamera _battleCamera = null;
    private CharacterController characterController;
    [Range(1, 60f)]
    public float moveSpeed = 20;
    private TextMeshPro exclamationMark;
    private Animator ani;
    public SkillUI _skillUI;
    private BoxCollider _boxCollider;
    public StoreUI _storeUI;

    public GameObject _healEffect;

    public static PlayerBehave instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        exclamationMark = transform.Find("ExclamationMark").GetComponent<TextMeshPro>();
        ani = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    protected override void Move()
    {
        base.Move();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("aa");
        }

        if(_playerState == PlayerState.INSTORE)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                _storeUI.GetBackStoreUI();
                _playerState = PlayerState.IDLE;
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
        var labelStyle = new GUIStyle();
        labelStyle.fontSize = 50;
        labelStyle.normal.textColor = Color.yellow;
        GUILayout.Label("\n\n현재 플레이어의 공격력 : " + playerAttack, labelStyle);
        //캐릭터 현재 돈
        GUILayout.Label("현재 가진 돈 : " + currentMoney, labelStyle);
        //현재 돈의 가치
        GUILayout.Label("현재 돈의 가치 : " + moneyValue, labelStyle);
    }

    private void OnTriggerEnter(Collider collison)
    {
        if(collison.tag == "Coin")
        {
            currentMoney += moneyValue;
            Destroy(collison.gameObject);
        }
        if (collison.tag == "Enemy")
        {
            _playerState = PlayerState.BATTLE;
            _boxCollider.enabled = false;
            _enemy = collison.gameObject;
            StartCoroutine(PlayerFindEnemyToBattle());
            //Camera.main.depth = -1;
            //transform.LookAt(collison.transform);
            //transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            //collison.transform.LookAt(transform);
            //collison.transform.position = new Vector3(collison.transform.position.x, 1, collison.transform.position.z);
        }
        if(collison.tag == "Store")
        {
            _playerState = PlayerState.INSTORE;
            StartCoroutine(PlayerInStore());
        }
    }

    IEnumerator PlayerFindEnemyToBattle()
    {
        exclamationMark.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.3f);

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
