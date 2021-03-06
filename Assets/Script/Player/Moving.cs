using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public abstract class Moving : MonoBehaviour
{
    public static int currentMoney = 200000;
    public static int moneyValue = 1;

    #region 플레이어의 공격력, 피, 방어력
    public static int playerAttack = 3;
    public static int playerCurrentAttack = playerAttack;
    public static int playerHealth = 20;
    public static int playerAddHealth = playerHealth;
    public static int playerCurrentHealth = playerHealth;
    public static int playerDefence = 3;
    public static int playerCurrentDefence = playerDefence;
    #endregion

    #region 적의 공격력, 피, 방어력
    public static int enemyAttack = 3;
    public static int enemyHealth = 17;
    public static int enemycurrnetHealth = enemyHealth;
    public static int enemyDefence = 2;
    public static int enemyMoney = 3;
    #endregion

    #region 보스 공격력, 피, 방어력
    public static int bossAttack = 11;
    public static int bossHealth = 72;
    public static int bosscurrnetHealth = bossHealth;
    public static int bossDefence = 10;
    public static int bossMoney = 18;
    #endregion

    public static bool _isPlayerTurn = true;

    public bool _canEnemyMove = true;
    public static bool _isGacha = false, _isStoresetting = false;

    protected static int passive_100m = 1;

    
    protected static bool demageBlock = false;

    #region 패시브 아이템 불체크
    public static bool passive_Critical = false;
    public static bool passive_Bouble = false;
    public static bool passive_Midas = false;
    public static bool passive_Poison = false;
    public static bool passive_Reflect = false;
    public static bool passive_David = false;
    public static bool passive_Boold = false;
    public static bool passive_DemiGod = false;
    public static bool passive_TheKing = false;
    #endregion

    public enum PlayerState
    {
        IDLE,
        MOVING,
        BATTLE,
        INSTORE,
        INSETTING
    }

    public static PlayerState _playerState = PlayerState.INSETTING;

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        if (_playerState == PlayerState.IDLE && _playerState != PlayerState.INSTORE && _playerState != PlayerState.INSETTING)
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
                SoundClips.instance.MoveSound();
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
