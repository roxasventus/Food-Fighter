
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/*
전체적인 동작방법:
    1. EffectSource가 Public Array에서 오디오 소스와 이름을 읽어와 Dictionary를 만든다
    2. SoundManager은 playSound(ClipName) 함수를 포함, 이 함수는 사운드를 재생한다
    3. 사운드 재생 함수가 호출되면 (1) 해당 사운드가 있는지 검사
                               (2) 해당 사운드를 플레이리스트(진짜로 리스트)에 넣음
    4. 한편 Update에서 플레이리스트를 순회하며 재생이 끝난게 있으면 해당 인수를 없앰.
*/

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private List<ClipInf> clip_information = new List<ClipInf>();
    private Dictionary<string, AudioClip> clip_map = new Dictionary<string, AudioClip>();

    // Pooling Logic
    private IObjectPool<AudioSource> adPool;
    // Currently active AudioSources to check for playback finish
    private List<AudioSource> activeAudioSources = new List<AudioSource>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitializePool();
        }
        else
        {
            Debug.LogWarning("two or more SoundManagers...");
            Destroy(gameObject);
        }

        foreach (ClipInf ci in clip_information)
        {
            clip_map[ci.name] = ci.clip;
            Debug.Log(ci.name);
        }
    }

    private void InitializePool()
    {
        adPool = new ObjectPool<AudioSource>(
            CreateSoundObject,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            true, // collectionCheck
            10,   // defaultCapacity
            50    // maxCapacity
        );
    }

    // Pool Callback: Create
    private AudioSource CreateSoundObject()
    {
        // Create a new child GameObject for the AudioSource
        GameObject go = new GameObject("SoundEffect");
        go.transform.SetParent(this.transform);
        return go.AddComponent<AudioSource>();
    }

    // Pool Callback: Get
    private void OnTakeFromPool(AudioSource source)
    {
        source.gameObject.SetActive(true);
    }

    // Pool Callback: Release
    private void OnReturnedToPool(AudioSource source)
    {
        if (source.gameObject.activeSelf)
        {
            source.Stop();
            source.clip = null; // Clean up refrance
            source.gameObject.SetActive(false);
        }
    }

    // Pool Callback: Destroy
    private void OnDestroyPoolObject(AudioSource source)
    {
        Destroy(source.gameObject);
    }

    void Update()
    {
        // Check for finished sounds and return to pool
        // Iterate backwards to safely remove from list
        for (int i = activeAudioSources.Count - 1; i >= 0; i--)
        {
            AudioSource source = activeAudioSources[i];

            // Should verify source is not null in case it was destroyed externally
            if (source == null)
            {
                activeAudioSources.RemoveAt(i);
                continue;
            }

            if (!source.isPlaying)
            {
                // Release back to pool
                adPool.Release(source);
                activeAudioSources.RemoveAt(i);
            }
        }
    }

    public void PlaySound(string clipName)
    {
        if (clip_map.ContainsKey(clipName))
        {
            // Get from pool
            AudioSource source = adPool.Get();

            source.clip = clip_map[clipName];
            source.loop = false;
            source.Play();

            // Add to active list specifically for checking isPlaying in Update
            activeAudioSources.Add(source);
        }
        else
        {
            Debug.LogError($"No Clip : {clipName}");
        }
    }
}

[System.Serializable]
public class ClipInf
{
    public string name;
    public AudioClip clip;
}