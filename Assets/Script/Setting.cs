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
            StartCoroutine(CreateStartBlock());
            //StartCoroutine("SpawnCoin");   // <--------------------이거 꼭 넣어야함
            //StartCoroutine(CreateEnemyForCount());
        }
        maxCoinCount = 100;
        coinSpawnDeley = 6f;
    }

    IEnumerator CreateStartBlock()
    {
        GameObject _groundprefab = backGroundPrefab;
        GameObject _wall = Wall;
        GameObject _storeBlock = storeBlock;
        GameObject _stairBlock = stairBlock;

        for (int i = MaxZ; i >= MinZ; i--)
        {
            for (int j = MinX; j <= MaxX; j++)
            {
                Vector3 pos = new Vector3(j, 0, i);

                if (j == MaxX || j == MinX || i == MinZ || i == MaxZ)
                {
                    Instantiate(_wall, pos, Quaternion.identity);
                }
                else if (i >= MaxZ - 1 && (j == 0))
                {
                    Instantiate(_storeBlock, pos, Quaternion.identity);
                    GameManager.instance.SetSpecialBlock(pos, "Store");
                }
                else if (i <= MinZ + 1 && (j == 0))
                {
                    Instantiate(_stairBlock, pos, Quaternion.identity);
                    GameManager.instance.SetSpecialBlock(pos, "Stair");
                }
                else
                {
                    Instantiate(_groundprefab, pos, Quaternion.identity);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }

        CreatePlayer();
    }

    IEnumerator CreateCaveBlock()
    {
        GameObject _groundprefab = backGroundPrefab;
        GameObject _wall = Wall;

        for (int i = CaveMaxZ; i >= CaveMinZ; i--)
        {
            for (int j = CaveMinX; j <= CaveMaxX; j++)
            {
                Vector3 pos = new Vector3(j, 0, i);

                if (j == CaveMaxX || j == CaveMinX || i == CaveMinZ || i == CaveMaxZ)
                {
                    Instantiate(_wall, pos, Quaternion.identity);
                }
                else
                {
                    Instantiate(_groundprefab, pos, Quaternion.identity);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }

        SetEnemy();
    }

    public void CreatePlayer()
    {
        Player.transform.position = Vector3.zero;
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
}
