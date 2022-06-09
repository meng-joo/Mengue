using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public abstract class Moving : MonoBehaviour
{
    public static bool _isBattle = false;
    public static bool _isMoving = false;

    protected BackGround _backGround = null;

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
        if (!_isBattle && ! _isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                SendMessage("InputPlayerMovingKey");
                for (int i = 0; i < _backGround._enemyList.Count; i++)
                {
                    SendMessage("InputEnemyMovingKey");
                }
            }
        }
    }

    protected virtual void InputPlayerMovingKey()
    {
        //ÈåÇìÇì
    }

    protected virtual void InputEnemyMovingKey()
    {
        //ÈåÇìÇì
    }
}
