using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public const int MinX = -25;
    public const int MaxX = 25;
    public const int MaxZ = 25;
    public const int MinZ = -25;

    public float coinSpawnDeley = 10f;
    public int coinCount = 0;

    public List<Enemy> _enemyList = new List<Enemy>();
    public List<Boss> _bossList = new List<Boss>();

    public GameObject backGroundPrefab = null;
    public GameObject Wall = null;
    public GameObject Player = null;
    public GameObject storeBlock = null;
    public GameObject coin = null;
    public Enemy _enemy = null;
    public Boss _boss = null;


    [Range(1, 25)]
    public int enemycount;

    void Start()
    {
        coinCount = 0;
        if (backGroundPrefab != null)
        {
            StartCoroutine(CreateBackGroundBlock());
            StartCoroutine("SpawnCoin");
        }
    }

    IEnumerator CreateBackGroundBlock()
    {
        GameObject _groundprefab = backGroundPrefab;
        GameObject _wall = Wall;
        GameObject _storeBlock = storeBlock;

        for (int i = MaxZ; i >= MinZ; i--)
        {
            for (int j = MinX; j <= MaxX; j++)
            {
                if (j == MaxX || j == MinX || i == MinZ || i == MaxZ)
                {
                    Instantiate(_wall, new Vector3(j, 0, i), Quaternion.identity);
                }
                else if (i >= MaxZ - 2 && (j >= -1 && j <= 1))
                {
                    Instantiate(_storeBlock, new Vector3(j, 0, i), Quaternion.identity);
                }
                else
                {
                    Instantiate(_groundprefab, new Vector3(j, 0, i), Quaternion.identity);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }

        CreatePlayer();
    }

    public void CreatePlayer()
    {
        int x = Random.Range(-2, 3);  //  2
        int z = Random.Range(-2, 3);  // -2
        Player.transform.position = new Vector3(x, 0, z);
        Player.SetActive(true);

        StartCoroutine(CreateEnemyForCount(x, z));
    }

    IEnumerator CreateEnemyForCount(int Px, int Pz)
    {
        int random = Random.Range(1, 11);
        for (int i = 0; i < enemycount; i++)
        {
            CreateEnemy(Px, Pz);
            yield return new WaitForSeconds(0.002f);
        }

        CreateBoss(Px, Pz);
    }


    public void CreateBoss(int Px = 0, int Pz = 0)
    {
        Boss boss = _boss;

        int z1 = Random.Range(MinZ + 2, -5);
        int z2 = Random.Range(5, MaxZ - 2);
        int x1 = Random.Range(MinX + 2, -5);
        int x2 = Random.Range(5, MaxX - 2);

        int a = Random.Range(0, 2);

        int x, z;
        x = a == 1 ? x1 : x2;
        z = a == 1 ? z1 : z2;

        for (int j = 0; j < _bossList.Count; j++)
        {
            if (x == Mathf.RoundToInt(_bossList[j].transform.position.x) && z == Mathf.RoundToInt(_bossList[j].transform.position.z))
            {
                x += Random.Range(-3, 3);
                z += Random.Range(-3, 3);
            }
        }

        _bossList.Add(boss);
        Instantiate(boss, new Vector3(x, 0, z), Quaternion.Euler(0, 180, 0));
        boss.gameObject.SetActive(true);
    }

    public void CreateEnemy(int Px = 0, int Pz = 0)
    {
        Debug.Log("利捞 积己   利捞 积己   利捞 积己   利捞 积己   利捞 积己");
        Enemy enemy = _enemy;
        int z1 = Random.Range(MinZ + 2, -3);
        int z2 = Random.Range(3, MaxZ - 2);
        int x1 = Random.Range(MinX + 2, -3);
        int x2 = Random.Range(3, MaxX - 2);

        int a = Random.Range(0, 2);

        int x, z;
        x = a == 1 ? x1 : x2;
        z = a == 1 ? z1 : z2;

        for (int j = 0; j < _enemyList.Count; j++)
        {
            if (x == Mathf.RoundToInt(_enemyList[j].transform.position.x) && z == Mathf.RoundToInt(_enemyList[j].transform.position.z))
            {
                x += Random.Range(-3, 3);
                z += Random.Range(-3, 3);
            }
        }

        
        _enemyList.Add(enemy);
        Instantiate(enemy, new Vector3(x, 0, z), Quaternion.Euler(0, 180, 0));
        enemy.gameObject.SetActive(true);
    }

    public IEnumerator SpawnCoin()
    {
        yield return new WaitForSeconds(7f);

        while (true)
        {
            if (coinCount < 40)
            {
                yield return new WaitForSeconds(1);
                int x = Random.Range(MinX + 1, MaxX);
                int z = Random.Range(MinZ + 1, MaxZ - 2);

                Instantiate(coin, new Vector3(x, 0, z), Quaternion.Euler(0, 180, 0));
                coinCount++;
            }

            else yield return new WaitForSeconds(coinSpawnDeley * coinSpawnDeley - 30);
        }
    }
}
