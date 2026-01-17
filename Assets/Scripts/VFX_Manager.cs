using System.Collections.Generic;
using UnityEngine;

public class VFX_Manager : MonoBehaviour
{
    [field: SerializeField] List<VFX_Info> vfxList = new List<VFX_Info>();
    public static VFX_Manager i;
    private void Awake()
    {
        if (i == null) i = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// VFX 재생
    /// PlayVFX(재생할 VFX, 위치, 회전, 파괴 시간(0이면 재생시간 이후 자동 파괴, loop는 파괴안됨))
    /// </summary>
    public GameObject PlayVFX(GameObject vfxPrefab, Vector2 position, Quaternion rotation, Transform parent = null, float destroyTime = 0)
    {
        Debug.Log("[VFX] " + vfxPrefab.name + " 재생");

        GameObject vfx = Instantiate(vfxPrefab, position, rotation, parent);
        ParticleSystem particle = vfx.GetComponent<ParticleSystem>();
        vfx.transform.parent = parent;
        particle.Play();
        Debug.Log("[VFX] 재생함.");
        if (destroyTime > 0)
        {
            Destroy(vfx, destroyTime);
        }
        else
        {
            Destroy(vfx, particle.main.duration);
        }
        return vfx;
    }
    public GameObject PlayVFX(string vfxPrefab, Vector2 position, Quaternion rotation, Transform parent = null, float destroyTime = 0)
    {
        GameObject vfx = vfxList.Find(x => x.name == vfxPrefab)?.prefab;
        if(vfx == null)
        {
            Debug.LogError("[VFX] " + vfxPrefab + " 이(가) VFX 리스트에 없습니다!");
            return null;
        }


        return PlayVFX(
            vfxList.Find(x => x.name == vfxPrefab).prefab,
            position,
            rotation,
            parent, destroyTime
        );
    }
    public GameObject PlayVFX(GameObject vfxPrefab, Vector2 position, Transform parent = null)
    {
        return PlayVFX(vfxPrefab, position, Quaternion.identity, null, 0);
    }


    

    
}

[System.Serializable]
public class VFX_Info
{
    public string name;
    public GameObject prefab;
}