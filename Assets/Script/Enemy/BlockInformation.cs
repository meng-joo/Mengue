using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BlockInformation
{
    public int _x;
    public int _z;

    public bool isStore = false;
    public bool isCommon_Enemy = false;
    public bool is_Boss = false;
    public bool is_Stair = false;
    public bool is_Setting = false;

    public BlockInformation(BlockInformation _b)
    {
        int x = _b._x;
        int z = _b._z;

        bool isStore = _b.isStore;
        bool isCommon_Enemy = _b.isCommon_Enemy;
        bool is_Boss = _b.is_Boss;
        bool is_Stair = _b.is_Stair;
        bool is_Setting = _b.is_Setting;
    }
    public BlockInformation()
    {
        int x = 0;
        int z = 0;

        bool isStore = false;
        bool isCommon_Enemy = false;
        bool is_Boss = false;
        bool is_Stair = false;
        bool is_Setting = false;
    }
}
