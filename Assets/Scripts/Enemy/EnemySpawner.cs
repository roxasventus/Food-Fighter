using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyConsts ec;
    [SerializeField] private Transform manager;
    [SerializeField] private Transform truck;

    // 현재 살아있는 enemy 리스트
    private List<Enemy> enemyList = new List<Enemy>();
    private List<Food> foodList = new List<Food>();

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
        // 트럭에 도착했는가?
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            Enemy e = enemyList[i];

            if (e.isCrash())
            {
                e.StartCoroutine(e.Crash(truck.position));
                enemyList.RemoveAt(i);
            }
            if (e.isBreathSound())
            {
                SoundManager.instance.PlaySound("ZombieBreathing"+Random.Range(1, 4).ToString()); //| PlaySound
            }
        }

        // 음식 리스트 갱신
        for (int i=foodList.Count - 1; i >= 0; i--)
        {
            Food f = foodList[i];

            if (f.CheckY())
            {
                f.Release();
                foodList.RemoveAt(i);
            }
        }

        // 음식에 닿았는가?
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            Enemy e = enemyList[i];
            foreach (Food f in foodList)
            {
                bool touched = GetDist(f, e) <= ec.foodRecDist;
                bool can_eat = f.countdown > 0;
                bool matching = IsMatch(f, e);

                if (touched && can_eat && matching && f.isInit)
                {
                    f.countdown--;
                    e.StartCoroutine(e.FoundFood(f.transform, manager));
                    enemyList.RemoveAt(i);
                    SoundManager.instance.AfterSound(1f, "ZombieEating" + Random.Range(1, 4).ToString()); //| SOUND
                    break;
                }
                // 아이템 사용시
                if (touched && can_eat && GameManager.instance.chosenRecipe.GetSpecial == Recipe.Special.hot && f.isInit)
                {
                    f.countdown--;
                    e.StartCoroutine(e.FoundFood(f.transform, manager));
                    enemyList.RemoveAt(i);
                    SoundManager.instance.AfterSound(1f, "ZombieEating" + Random.Range(1, 4).ToString()); //| SOUND
                    break;
                }
            }
        }
    }

    private float GetDist(Food f, Enemy e)
    {
        float v = Vector2.Distance(f.transform.position, e.transform.position);
        return v;
    }

    private bool IsMatch(Food f, Enemy e)
    {
        if (e.favorite == FavoriteFood.Eth)
        {
            return true;
        }
        else
        {
            if (e.favorite == f.type)
            {
                return true;
            }
        }

        return false;
    }

    public void AddFood(Food f)
    {
        foodList.Add(f);
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
