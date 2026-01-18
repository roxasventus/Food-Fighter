using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    public static GameTimeManager instance;

    private float gameTimeScale = 1f;
    public float GameTimeScale { get => gameTimeScale; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetGameTimeScale()
    {
        gameTimeScale = 1f;
        Time.timeScale = gameTimeScale;
    }
    public void SetGameTimeScaleWithPercent(float percent)
    {
        Time.timeScale = gameTimeScale * percent;
    }
}