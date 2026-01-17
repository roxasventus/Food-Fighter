using UnityEngine;

public class VFX_Manager : MonoBehaviour
{
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
    public GameObject PlayVFX(GameObject vfxPrefab, Vector2 position, Quaternion rotation, float destroyTime = 0)
    {
        Debug.Log("[VFX] " + vfxPrefab.name + " 재생");

        GameObject vfx = Instantiate(vfxPrefab, position, rotation);
        ParticleSystem particle = vfx.GetComponent<ParticleSystem>();
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
    public GameObject PlayVFX(GameObject vfxPrefab, Vector2 position)
    {
        return PlayVFX(vfxPrefab, position, Quaternion.identity);
    }
}