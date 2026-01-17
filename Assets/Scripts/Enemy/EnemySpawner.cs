using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyConsts ec;

    // 현재 살아있는 enemy 리스트
    private List<Enemy> enemyList = new List<Enemy>();

    // enemy 정보 딕셔너리
    private Dictionary<int, EnemyData> dataDict = new Dictionary<int, EnemyData>();
    void Start()
    {
        EnemyData[] data = Resources.LoadAll<EnemyData>("EnemyData");
        foreach (EnemyData d in data)
        {
            dataDict[d.id] = d;
        }
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

    public int GetCnt()
    {
        return enemyList.Count;
    }

    public void Spawn(int id)
    {
        Enemy e = ObjPoolManager.instance.InstantiateFromPool("Enemy").GetComponent<Enemy>();
        float x = Random.Range(ec.spawnRange[0], ec.spawnRange[1]);
        e.Init(new Vector2(x, ec.spawnY), dataDict[id]);
        enemyList.Add(e);
    }
}
