using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public const int MinX = -15;
    public const int MaxX = 15;
    public const int MaxZ = 15;
    public const int MinZ = -15;

    public List<Enemy> _enemyList = new List<Enemy>();

    public GameObject backGroundPrefab = null;
    public GameObject Wall = null;
    public GameObject Player = null;
    public Enemy Enemy = null;
    [Range(1, 25)]
    public int enemycount;

    void Start()
    {
        if(backGroundPrefab != null)
        {
            StartCoroutine(CreateBackGroundBlock());
        }
    } 

    IEnumerator CreateBackGroundBlock()
    {
        GameObject _groundprefab = backGroundPrefab;
        GameObject _wall = Wall;

        for (int i = MaxZ; i >= MinZ; i--)
        {
            for (int j = MinX; j <= MaxX; j++)
            {
                if (j == MaxX || j == MinX || i == MinZ || i == MaxZ)
                {
                    Instantiate(_wall, new Vector3(j, 0, i), Quaternion.identity);
                }
                else
                {
                    Instantiate(_groundprefab, new Vector3(j, 0, i), Quaternion.identity);
                }

                yield return new WaitForSeconds(0.0001f);
            }
        }

        CreatePlayer();
    }

    private void CreatePlayer()
    {
        int x = Random.Range(-2, 3);  //  2
        int z = Random.Range(-2, 3);  // -2
        Player.transform.position = new Vector3(x, 0, z);
        Player.SetActive(true);

        StartCoroutine(CreateEnemy(x, z));
    }

    IEnumerator CreateEnemy(int Px, int Pz)
    {
        Enemy enemy = Enemy;
        for (int i = 0; i < enemycount; i++)
        {
            int z = Random.Range(MinZ + 1, MaxZ);
            int x = Random.Range(MinX + 1, MaxX);

            for (int j = 0; j < _enemyList.Count; j++)
            {
                if (x == Mathf.RoundToInt(_enemyList[j].transform.position.x) && z == Mathf.RoundToInt(_enemyList[j].transform.position.z))
                {
                    x += Random.Range(-3, 3);
                    z += Random.Range(-3, 3);
                }
            }

            if (Mathf.Abs(x) <= Mathf.Abs(Px) && Mathf.Abs(z) <= Mathf.Abs(Pz))
            {
                x += Random.Range(4, 8);
                z += Random.Range(-8, -4);
                if (Mathf.Abs(x) <= Mathf.Abs(Px) && Mathf.Abs(z) <= Mathf.Abs(Pz))
                {
                    x += Random.Range(4, 8);
                    z += Random.Range(-8, -4);
                }
            }
            _enemyList.Add(enemy);
            Instantiate(enemy, new Vector3(x, 0, z), Quaternion.Euler(0, 180, 0));
            yield return new WaitForSeconds(0.12f);
        }
    }
}
