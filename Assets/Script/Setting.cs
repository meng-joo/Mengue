using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public const int MinX = -5;
    public const int MaxX = 5;
    public const int MaxZ = 5;
    public const int MinZ = -5;

    public const int CaveMinX = -12;
    public const int CaveMaxX = 12;
    public const int CaveMaxZ = 12;
    public const int CaveMinZ = -12;

    public float coinSpawnDeley = 7.5f;
    public int coinCount = 0;

    public GameObject backGroundPrefab = null;
    public GameObject Wall = null;
    public GameObject Player = null;
    public GameObject storeBlock = null;
    public GameObject coin = null;
    public GameObject stairBlock = null;
    public GameObject settingBlock = null;

    public GameObject[] cave_Block = new GameObject[1000];
    public GameObject[] home_Block = new GameObject[200];

    public int maxCoinCount = 100;

    [Range(1, 30)]
    public int enemycount;
    
    [Range(1, 15)]
    public int bosscount;

    void Start()
    {
        coinCount = 0;
        if (backGroundPrefab != null)
        {
            CreateCaveBlock();
            CreateStartBlock();
            //StartCoroutine("SpawnCoin");   // <--------------------이거 꼭 넣어야함
            //StartCoroutine(CreateEnemyForCount());
        }
        maxCoinCount = 100;
        coinSpawnDeley = 6f;

        StartCoroutine(ActiveStartBlock());
    }

    void CreateStartBlock()
    {
        GameObject _groundprefab = backGroundPrefab;
        GameObject _wall = Wall;
        GameObject _storeBlock = storeBlock;
        GameObject _stairBlock = stairBlock;
        GameObject _settingBlock = settingBlock;

        int total = 0;

        for (int i = MaxZ; i >= MinZ; i--)
        {
            for (int j = MinX; j <= MaxX; j++)
            {
                Vector3 pos = new Vector3(j, 0, i);

                if (j == MaxX || j == MinX || i == MinZ || i == MaxZ)
                {
                    home_Block[total] = Instantiate(_wall, pos, Quaternion.identity);
                }
                else if (i == MaxZ - 1 && j == MinX + 1)
                {
                    home_Block[total] = Instantiate(_settingBlock, pos, Quaternion.identity);
                }
                else if (i >= MaxZ - 1 && (j == 0))
                {
                    home_Block[total] = Instantiate(_storeBlock, pos, Quaternion.identity);
                }
                else if (i <= MinZ + 1 && (j == 0))
                {
                    home_Block[total] = Instantiate(_stairBlock, pos, Quaternion.identity);
                }
                else
                {
                    home_Block[total] = Instantiate(_groundprefab, pos, Quaternion.identity);
                }

                home_Block[total].SetActive(false);
                total++;
            }
        }

        //CreatePlayer();
    }

    void CreateCaveBlock()
    {
        GameObject _groundprefab = backGroundPrefab;
        GameObject _wall = Wall;
        GameObject _stairBlock = stairBlock;

        int total = 0;

        for (int i = CaveMaxZ; i >= CaveMinZ; i--)
        {
            for (int j = CaveMinX; j <= CaveMaxX; j++)
            {
                Vector3 pos = new Vector3(j, 0, i);

                if (j == CaveMaxX || j == CaveMinX || i == CaveMinZ || i == CaveMaxZ)
                {
                    cave_Block[total] = Instantiate(_wall, pos, Quaternion.identity);
                }
                else if (i == -2 && j == 0)
                {
                    cave_Block[total] = Instantiate(_stairBlock, pos, Quaternion.identity);
                }
                else
                {
                    cave_Block[total] = Instantiate(_groundprefab, pos, Quaternion.identity);
                }

                cave_Block[total].SetActive(false);
                total++;
            }
        }
        //SetEnemy();
    }

    public void CreatePlayer()
    {
        Player.SetActive(true);
    }

    //IEnumerator CreateEnemyForCount()
    //{
    //    int random = Random.Range(1, 11);
    //    for (int i = 0; i < enemycount; i++)
    //    {
    //        CreateEnemy();
    //        yield return new WaitForSeconds(0.002f);
    //    }

    //    CreateBoss();
    //}


    //public void CreateBoss(int Px = 0, int Pz = 0)
    //{
    //    Boss boss = _boss;

    //    int z1 = Random.Range(MinZ + 2, -5);
    //    int z2 = Random.Range(5, MaxZ - 2);
    //    int x1 = Random.Range(MinX + 2, -5);
    //    int x2 = Random.Range(5, MaxX - 2);

    //    int a = Random.Range(0, 2);

    //    int x, z;
    //    x = a == 1 ? x1 : x2;
    //    z = a == 1 ? z1 : z2;

    //    for (int j = 0; j < _bossList.Count; j++)
    //    {
    //        if (x == Mathf.RoundToInt(_bossList[j].transform.position.x) && z == Mathf.RoundToInt(_bossList[j].transform.position.z))
    //        {
    //            x += Random.Range(-3, 3);
    //            z += Random.Range(-3, 3);
    //        }
    //    }

    //    _bossList.Add(boss);
    //    Instantiate(boss, new Vector3(x, 0, z), Quaternion.Euler(0, 180, 0));
    //    boss.gameObject.SetActive(true);
    //}

    //public void CreateEnemy(int Px = 0, int Pz = 0)
    //{
    //    CommonEnemy enemy = _enemy;
    //    int z1 = Random.Range(MinZ + 2, -3);
    //    int z2 = Random.Range(3, MaxZ - 2);
    //    int x1 = Random.Range(MinX + 2, -3);
    //    int x2 = Random.Range(3, MaxX - 2);

    //    int a = Random.Range(0, 2);

    //    int x, z;
    //    x = a == 1 ? x1 : x2;
    //    z = a == 1 ? z1 : z2;

    //    for (int j = 0; j < _enemyList.Count; j++)
    //    {
    //        if (x == Mathf.RoundToInt(_enemyList[j].transform.position.x) && z == Mathf.RoundToInt(_enemyList[j].transform.position.z))
    //        {
    //            x += Random.Range(-3, 3);
    //            z += Random.Range(-3, 3);
    //        }
    //    }

        
    //    _enemyList.Add(enemy);
    //    Instantiate(enemy, new Vector3(x, 0, z), Quaternion.Euler(0, 180, 0));
    //    enemy.gameObject.SetActive(true);
    //}

    public IEnumerator SpawnCoin()
    {
        yield return new WaitForSeconds(7f);

        while (true)
        {
            if (coinCount < maxCoinCount)
            {
                yield return new WaitForSeconds(coinSpawnDeley);
                int x = Random.Range(CaveMinX + 1, CaveMaxX);
                int z = Random.Range(CaveMinZ + 1, CaveMaxZ - 2);

                Instantiate(coin, new Vector3(x, 0, z), Quaternion.Euler(0, 180, 0));
                coinCount++;
            }

            else yield return new WaitForSeconds(coinSpawnDeley * coinSpawnDeley - 7);
        }
    }

    public void SetEnemy()
    {
        int z = 0;
        int x = 0;

        for (int i = 0; i <= enemycount; i++)
        {
            z = Random.Range(CaveMinZ + 2, CaveMaxZ - 1);
            x = Random.Range(CaveMinX + 2, CaveMaxX - 1);

            for (int j = 0; j < GameManager.instance.blockInfo.Count; j++) {
                if (GameManager.instance.blockInfo[j]._x == x && GameManager.instance.blockInfo[j]._z == z)
                {
                    z += Random.Range(-2, 2);
                    x += Random.Range(-2, 2);
                }
            }

            GameManager.instance.SetSpecialBlock(new Vector3(x, 0, z), "Common_Enemy");
        }

        for (int i = 0; i <= bosscount; i++)
        {
            z = Random.Range(CaveMinZ + 1, CaveMaxZ - 1);
            x = Random.Range(CaveMinX + 1, CaveMaxX - 1);

            GameManager.instance.SetSpecialBlock(new Vector3(x, 0, z), "Boss");
        }
    }

    public void IsPlayerInCave()
    {
        if(PlayerBehave.instance._isCave)
        {
            ActivitingStartBlock();
        }

        else
        {
            ActivitingCaveBlock();
        }
    }

    public void ActivitingCaveBlock()
    {
        StartCoroutine(ActiveCaveBlock());
        PlayerBehave.instance._isCave = true;
    }

    public void ActivitingStartBlock()
    {
        StartCoroutine(ActiveStartBlock());
        PlayerBehave.instance._isCave = false;
    }

    public IEnumerator ActiveCaveBlock()
    {
        GameManager.instance.blockInfo.Clear();
        PlayerBehave._playerState = PlayerBehave.PlayerState.NONE;
        Vector3 pos;

        yield return new WaitForSecondsRealtime(0.2f);

        Player.SetActive(false);

        yield return new WaitForSecondsRealtime(0.2f);

        int total = 0;

        for (int i = MinZ; i <= MaxZ; i++)
        {
            for (int j = MaxX; j >= MinX; j--)
            {
                home_Block[total].SetActive(false);
                total++;
            }
            yield return new WaitForSecondsRealtime(0.01f);
        }

        yield return new WaitForSecondsRealtime(2f);

        total = 0;

        for (int i = CaveMaxZ; i >= CaveMinZ; i--)
        {
            for (int j = CaveMinX; j <= CaveMaxX; j++)
            {
                pos = new Vector3(j, 0, i);

                if (i == -2 && j == 0)
                {
                    GameManager.instance.SetSpecialBlock(pos, "Stair");
                }

                cave_Block[total].SetActive(true);
                total++;
            }
            yield return new WaitForSecondsRealtime(0.01f);
        }

        SetEnemy();

        yield return new WaitForSecondsRealtime(0.6f);

        Player.SetActive(true);
        PlayerBehave._playerState = PlayerBehave.PlayerState.IDLE;
    }

    public IEnumerator ActiveStartBlock()
    {
        GameManager.instance.blockInfo.Clear();
        PlayerBehave._playerState = PlayerBehave.PlayerState.NONE;

        yield return new WaitForSecondsRealtime(0.2f);

        Player.SetActive(false);

        yield return new WaitForSecondsRealtime(0.2f);

        int total = 0;
        Vector3 pos;

        for (int i = CaveMinZ; i <= CaveMaxZ; i++)
        {
            for (int j = CaveMaxX; j >= CaveMinX; j--)
            {
                cave_Block[total].SetActive(false);
                total++;
            }
            
            yield return new WaitForSecondsRealtime(0.01f);
        }

        yield return new WaitForSecondsRealtime(2f);

        total = 0;

        for (int i = MaxZ; i >= MinZ; i--)
        {
            for (int j = MinX; j <= MaxX; j++)
            {
                pos = new Vector3(j, 0, i);

                if (i == 1 && j == 1)
                {
                    GameManager.instance.SetSpecialBlock(pos, "Setting");
                }
                else if (i >= MaxZ - 1 && (j == 0))
                {
                    GameManager.instance.SetSpecialBlock(pos, "Store");
                }
                else if (i <= MinZ + 1 && (j == 0))
                {
                    GameManager.instance.SetSpecialBlock(pos, "Stair");
                }
                home_Block[total].SetActive(true);
                total++;
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }

        yield return new WaitForSecondsRealtime(0.6f);

        Player.SetActive(true);
        PlayerBehave._playerState = PlayerBehave.PlayerState.IDLE;
    }
}
