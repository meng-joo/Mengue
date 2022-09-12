using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static bool _isGacha = false, _isStoresetting = false;

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

    [SerializeField]
    public List<BlockInformation> blockInfo = new List<BlockInformation>();


    private void Awake()
    {
        instance = this;
    }

    public void SetSpecialBlock(Vector3 pos, string name)
    {
        BlockInformation _blockInfo = new BlockInformation();

        if (name == "Store") _blockInfo.isStore = true;
        else if (name == "Common_Enemy") _blockInfo.isCommon_Enemy = true;
        else if (name == "Boss") _blockInfo.is_Boss = true;
        else if (name == "Stair") _blockInfo.is_Stair = true;
        else if (name == "Setting") _blockInfo.is_Setting = true;

        _blockInfo._x = Mathf.RoundToInt(pos.x);
        _blockInfo._z = Mathf.RoundToInt(pos.z);
        
        blockInfo.Add(_blockInfo);
    }
}
