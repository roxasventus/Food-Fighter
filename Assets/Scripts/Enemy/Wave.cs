using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class Wave : MonoBehaviour
{
    // json 불러오기
    private Dictionary<int, TemplateData> templates;
    private Dictionary<int, WaveData> waves;

    // 콜백 함수
    private Action _end;

    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private EnemyConsts ec;

    void Start()
    {
        templates = new Dictionary<int, TemplateData>(); 
        waves = new Dictionary<int, WaveData>();

        TextAsset templateJson = Resources.Load<TextAsset>("WaveData/templates");
        List<TemplateData> rawTemplates = JsonConvert.DeserializeObject<List<TemplateData>>(templateJson.text);
        foreach (TemplateData t in rawTemplates)
        {
            templates[t.id] = t;
        }

        TextAsset waveJson = Resources.Load<TextAsset>("WaveData/waves");
        Dictionary<string, WaveData> rawWaves = JsonConvert.DeserializeObject<Dictionary<string, WaveData>>(waveJson.text);
        foreach (WaveData w in rawWaves.Values)
        {
            waves[w.id] = w;
        }

        // 테스트
        StartWave(201, ()=>{Debug.Log("end!");
            GameManager.instance.endWave();
        });
    }

    public void StartWave(int waveId, Action end, bool isHard=false)
    {
        _end = end;

        StartCoroutine(WaveCoroutine(waveId, isHard));
    }

    private IEnumerator WaveCoroutine(int waveId, bool notEnd)
    {
        WaveData waveData = waves[waveId];
        
        // 주머니 생성
        int id, cnt;
        List<int> pouch = new List<int>();
        foreach (Dictionary<string, int> unit in waveData.units)
        {
            id = unit["template_id"];
            cnt = unit["quantity"];

            for (int i=0; i<cnt; i++)
            {
                pouch.Add(id);
            }
        }

        // 주머니가 빌 때까지 하나씩 뽑으면서 yield return StartCoroutine(SpawnTemplate(id)) 함.
        while (pouch.Count > 0)
        {
            // 최대 개수보다 적을 때까지 대기
            while (spawner.GetCnt() > waveData.zombie_limit)
            {
                yield return null;
            }

            int index = UnityEngine.Random.Range(0, pouch.Count);
            int selectedId = pouch[index];
            pouch.RemoveAt(index);

            yield return StartCoroutine(SpawnTemplate(selectedId));
        }

        if (notEnd)
        {
            StartCoroutine(WaveCoroutine(waveId, false));
        }
        else
        {
            while (spawner.GetCnt() > 0)
            {
                yield return null;
            }
            _end?.Invoke();
        }
    }

    private IEnumerator SpawnTemplate(int id)
    {
        TemplateData data;

        if (id == -1)
        {
            data = templates[UnityEngine.Random.Range(0, 5)];
        }
        else
        {
            data = templates[id];
        }
        
        yield return new WaitForSeconds(data.pre_delay);

        foreach (Dictionary<string, int> sp in data.spawn)
        {
            for (int i=0; i<sp["quantity"]; i++)
            {
                spawner.Spawn(sp["enemy_id"]);
                yield return new WaitForSeconds(UnityEngine.Random.Range(ec.templateGapRange[0], ec.templateGapRange[1]));
            }
        }

        yield return new WaitForSeconds(data.delay);
    }
}


[System.Serializable]
public class TemplateData
{
    public int id;
    public string explain;
    public List<Dictionary<string, int>> spawn;
    public float pre_delay;
    public float delay;
}

[System.Serializable]
public class WaveData
{
    public int id;
    public string explain;
    public List<Dictionary<string, int>> units;
    public int zombie_limit;
}