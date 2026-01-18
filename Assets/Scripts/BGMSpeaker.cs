using System.Collections.Generic;
using UnityEngine;

public class BGMSpeaker : MonoBehaviour
{
    [SerializeField] List<BGM> bgmSources = new List<BGM>();
    void Start()
    {
        foreach(var src in bgmSources)
        {
            SoundManager.instance.PlaySound(src.name, src.volume, src.pitch, true);
        }
    }
}
[System.Serializable]
public class BGM
{
    public string name;
    public float volume;
    public float pitch;
}
