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

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        exclamationMark = transform.Find("ExclamationMark").GetComponent<TextMeshPro>();
        ani = GetComponent<Animator>();
    }

    protected override void Move()
    {
        base.Move();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("aa");
        }
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

        //gravityVec = new Vector3(0, gravityScale, 0);
        inputTransform.y = 0;
        //Vector3 pos = inputTransform;

        //Vector3 finalpos = inputTransform * Time.deltaTime;

        characterController.Move(inputTransform);
    }

    private void OnTriggerEnter(Collider collison)
    {
        if (collison.tag == "Enemy")
        {
            _isBattle = true;
            _enemy = collison.gameObject;
            StartCoroutine(PlayerFindEnemyToBattle());
            //Camera.main.depth = -1;
            //transform.LookAt(collison.transform);
            //transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            //collison.transform.LookAt(transform);
            //collison.transform.position = new Vector3(collison.transform.position.x, 1, collison.transform.position.z);
        }
    }

    IEnumerator PlayerFindEnemyToBattle()
    {
        exclamationMark.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.7f);

        characterController.enabled = false;
        exclamationMark.gameObject.SetActive(false);
        _battleCamera.gameObject.SetActive(true);
        StartBattle();
        ani.SetTrigger("Battle");
        _skillUI.SendMessage("ViewSkillUI");    
    }

    private void StartBattle()
    {
        _battleCamera.SettingBattleCam();
        Debug.Log(transform.position);
        transform.position += new Vector3(0, 0.5f, 0);
        Quaternion quaternion = Quaternion.Euler(new Vector3(90f , 0f , 0f));
        //transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 1);
        //transform.localEulerAngles = new Vector3(75, 0, 0);
        transform.LookAt(_enemy.transform);
        transform.rotation *= quaternion;
        Debug.Log(transform.position);
        //ani.SetTrigger("Battle");
    }
}
