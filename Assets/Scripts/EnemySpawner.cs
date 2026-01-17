using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyConsts ec;

    private List<Enemy> enemyList = new List<Enemy>();
    void Start()
    {
        StartCoroutine(TestSpawn());
    }

    void Update()
    {
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            Enemy e = enemyList[i];
            if (e.isCrash())
            {
                e.StartCoroutine(e.Crash());
                enemyList.RemoveAt(i);
            }
        }
    }

    void Spawn()
    {
        Enemy e = ObjPoolManager.instance.InstantiateFromPool("Enemy").GetComponent<Enemy>();
        float x = Random.Range(ec.spawnRange[0], ec.spawnRange[1]);
        e.Init(new Vector2(x, ec.spawnY), false);
        enemyList.Add(e);
    }

    IEnumerator TestSpawn(int cnt=30)
    {
        for (int i=0; i<cnt; i++)
        {
            Spawn();
            yield return new WaitForSeconds(0.4f);
        }
    }
}
