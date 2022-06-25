using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public abstract class Moving : MonoBehaviour
{
    public static int currentMoney = 50000;
    public static int moneyValue = 1;

    #region 플레이어의 공격력, 피, 방어력
    public static int playerAttack = 3;
    public static int playerCurrentAttack = playerAttack;
    public static int playerHealth = 20;
    public static int playerAddHealth = 20;
    public static int playerCurrentHealth = playerHealth;
    public static int playerDefence = 3;
    public static int playerCurrentDefence = playerDefence;
    #endregion

    #region 적의 공격력, 피, 방어력
    public static int enemyAttack = 3;
    public static int enemyHealth = 20;
    public static int enemycurrnetHealth = enemyHealth;
    public static int enemyDefence = 3;
    public static int enemyMoney = 3;
    #endregion

    public static bool _isPlayerTurn = true;

    public bool _canEnemyMove = true;
    public static bool _isGacha = false, _isStoresetting = false;

    protected int passive_100m = 1;

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
            if (Input.GetKeyDown(KeyCode.W) || 
                Input.GetKeyDown(KeyCode.A) || 
                Input.GetKeyDown(KeyCode.S) || 
                Input.GetKeyDown(KeyCode.D) || 
                Input.GetKeyDown(KeyCode.DownArrow) || 
                Input.GetKeyDown(KeyCode.UpArrow) || 
                Input.GetKeyDown(KeyCode.LeftArrow) || 
                Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (_canEnemyMove)
                {
                    _canEnemyMove = false;
                    InputEnemyMovingKey();

                }
                InputPlayerMovingKey();
                
            }
        }
    }

    protected virtual void InputPlayerMovingKey()
    {
        //흐헤헤
    }

    protected virtual void InputEnemyMovingKey()
    {
        //흐헤헤
    }
}
