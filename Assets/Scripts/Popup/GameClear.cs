using UnityEngine;
using TMPro;
using System.Collections;

public class GameClear : MonoBehaviour
{
    [SerializeField] TMP_Text stage;
    [SerializeField] TMP_Text burn;
    [SerializeField] TMP_Text made;
    [SerializeField] TMP_Text dist;
    [SerializeField] TMP_Text zomcnt;

    [SerializeField] GameObject bg;

    void Start()
    {
        ClearInfo ci = GameManager.instance.clearInfo;

        stage.text = $"{ci.stageNum}";
        burn.text = ci.burnFood;
        made.text = ci.madeFood;
        dist.text = $"{ci.distance:F2}km";
        zomcnt.text = $"{ci.happy}명";

        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);

        bg.SetActive(false);
    }
}
