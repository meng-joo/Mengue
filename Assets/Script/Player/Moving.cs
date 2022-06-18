using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public abstract class Moving : MonoBehaviour
{
    public static int currentMoney = 0;
    public static int moneyValue = 1;

    #region �÷��̾��� ���ݷ�, ��, ����
    public static int playerAttack = 5;
    public static int playerHealth = 100;
    public static int playerCurrentHealth = playerHealth;
    public static int playerDefence = 3;
    #endregion

    #region ���� ���ݷ�, ��, ����
    public static int enemyAttack = 3;
    public static int enemyHealth = 30;
    public static int enemycurrnetHealth = enemyHealth;
    public static int enemyDefence = 3;
    public static int enemyMoney = 3;
    #endregion

    public static bool _isPlayerTurn = true;

    protected BackGround _backGround = null;
    public enum PlayerState
    {
        IDLE,
        MOVING,
        BATTLE,
        INSTORE
    }

    public static PlayerState _playerState = PlayerState.IDLE;

    private void Awake()
    {
        _backGround = FindObjectOfType<BackGround>();
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        if (_playerState == PlayerState.IDLE)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                InputEnemyMovingKey();
                InputPlayerMovingKey();
                //Invoke("ChangeStateToMove", 0.3f);
            }
        }
    }

    protected virtual void InputPlayerMovingKey()
    {
        //������
    }

    protected virtual void InputEnemyMovingKey()
    {
        //������
    }
}
