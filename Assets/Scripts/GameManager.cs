using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private SceneLoader sceneLoader;

    [SerializeField] private int _life = 5;
    public float life { get => _life; }
    public void loseLife()
    {
        heartContainers.GetChild(_life - 1).gameObject.SetActive(false);
        _life -= 1;
        if (_life == 0)
            SceneLoader.Instance.Load("GameOverScene");
    }
    public void getLife(int num)
    {

        for (int i = 0; i < num; i++)
        {
            if (_life >= 5)
            {
                break;
            }

            heartContainers.GetChild(_life).gameObject.SetActive(true);
            _life++;
        }

    }

    [SerializeField] private bool _isPlay;
    public bool isPlay { get => _isPlay; }

    [SerializeField] private int _roundCount = 5;
    public int roundCount { get => _roundCount; }
    public void clearRound()
    {
        _roundCount--; // 초기화는 어떻게 하나요?
        if (_roundCount < 0) 
        {
            GameClear ();
            _roundCount = 0;
        }
        BroadcastMessage("progressBarUpdate");


    }

    [SerializeField] private int _score = 0;
    public int score { get => _score; }
    public void loseScore(int num) { _score -= num; }
    public void getScore(int num) { _score += num; }

    [SerializeField] private Wave wave;


    [Header("Bullet")]
    // 장전이 되어 있는가?
    [SerializeField] private bool _isLoaded;
    public bool isLoaded { get => _isLoaded; }
    public void toogleLoaded() { _isLoaded = !_isLoaded; }

    // 선택된 탄환의 레시피
    [SerializeField] private Recipe _chosenRecipe;
    public Recipe chosenRecipe { get => _chosenRecipe; }

    [Header("Items")]
    [SerializeField] private Item _miwon;
    [SerializeField] private Item _hot;
    [SerializeField] private Item _olive;

    [SerializeField] private int _miwonCount;
    public int getMiwon { get => _miwonCount; }
    [SerializeField] private int _hotCount;
    public int getHot { get => _hotCount; }
    [SerializeField] private int _oliveCount;
    public int getOlive { get => _oliveCount; }

    public float totalTime = 0f;

    [SerializeField] Gameover go;

    public void getItem(int index)
    {
        if (index == 0)
        {
            _miwonCount++;
            _miwon.setcounterText(_miwonCount);
        }

        if (index == 1)
        {
            _hotCount++;
            _hot.setcounterText(_hotCount);
        }

        if (index == 2)
        {
            _oliveCount++;
            _olive.setcounterText(_oliveCount);
        }
    }

    public void getRandomItems(int num)
    {
        if (num > 2)
        {
            Debug.LogWarning(num + "개의 아이템은 가져올 수 없음");
            return;
        }

        int[] copy = { 0, 1, 2 };

        for (int i = 0; i < num; i++)
        {
            int rand = Random.Range(0, copy.Length);
            getItem(copy[rand]);
        }


    }

    public void allButtonScaleInit()
    {
        if (_miwon != null) _miwon.buttonScaleInit();
        if (_hot != null) _hot.buttonScaleInit();
        if (_olive != null) _olive.buttonScaleInit();
    }

    [Header("UI")]
    [SerializeField] private RectTransform crossPathCanvas;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Image progressBarFill;
    [SerializeField] private RectTransform heartContainers;

    bool Tuto2Played = false;
    public void startNormalWave()
    {
        if(!Tuto2Played)
        {
            wave.StartWave(901, () =>
            {
                Debug.Log("Start new normal wave!");
                GameManager.instance.endWave();
            });
            return;
        }

        int[] normal = { 100, 101, 102, 103, 104, 105, 106, 201, 202 };
        wave.StartWave(normal[Random.Range(0, normal.Length)], () =>
        {
            Debug.Log("Start new normal wave!");
            GameManager.instance.endWave();
        });
    }

    public void startHardWave()
    {
        if (!Tuto2Played)
        {
            wave.StartWave(901, () =>
            {
                Debug.Log("Start new normal wave!");
                GameManager.instance.endWave();
            });
            return;
        }

        int[] hard = { 300, 301, 302 };
        wave.StartWave(hard[Random.Range(0, hard.Length)], () =>
        {
            Debug.Log("Start new hard wave!");
            GameManager.instance.endWave();
        });
    }

    public void endWave()
    {
        crossPathCanvas.gameObject.SetActive(true);
    }

    public void progressBarUpdate()
    {
        progressBar.value = 1 - _roundCount / 15f;
        if (progressBar.value > 0.5)
        {
            progressBarFill.color = Color.yellow;
        }
        if (progressBar.value == 1.0f)
        {
            SceneLoader.Instance.Load("GameOverScene");
        }
    }


    public void initRecipe()
    {
        _chosenRecipe.SetStatus(Recipe.Status.fail);
        _chosenRecipe.SetBase(Recipe.Base.none);
        _chosenRecipe.SetSauce(Recipe.Sauce.none);
        _chosenRecipe.SetSpecial(Recipe.Special.none);
    }

    public void setRecipe(Recipe recipe)
    {
        _chosenRecipe.SetStatus(recipe.GetStatus);
        _chosenRecipe.SetBase(recipe.GetBase);
        _chosenRecipe.SetSauce(recipe.GetSauce);
    }

    public void setItem(int index)
    {
        if (index == 0 && _chosenRecipe.GetSpecial != Recipe.Special.miwon)
        {
            _chosenRecipe.SetSpecial(Recipe.Special.miwon);
        }
        else if (index == 1 && _chosenRecipe.GetSpecial != Recipe.Special.hot)
        {
            _chosenRecipe.SetSpecial(Recipe.Special.hot);
        }
        else if (index == 2 && _chosenRecipe.GetSpecial != Recipe.Special.olive)
        {
            _chosenRecipe.SetSpecial(Recipe.Special.olive);
        }
        else
        {
            _chosenRecipe.SetSpecial(Recipe.Special.none);
        }

    }

    public void useItem()
    {

        if (_chosenRecipe.GetSpecial == Recipe.Special.none)
        {
            return;
        }

        if (_chosenRecipe.GetSpecial == Recipe.Special.miwon && _miwonCount > 0)
        {
            _miwonCount--;
            _miwon.setcounterText(_miwonCount);
            _miwon.buttonScaleInit();
        }

        if (_chosenRecipe.GetSpecial == Recipe.Special.hot && _hotCount > 0)
        {
            _hotCount--;
            _hot.setcounterText(_hotCount);
            _hot.buttonScaleInit();
        }

        if (_chosenRecipe.GetSpecial == Recipe.Special.olive && _oliveCount > 0)
        {
            _oliveCount--;
            _olive.setcounterText(_oliveCount);
            _olive.buttonScaleInit();
        }

    }

    private void Awake()
    {
        instance = this;
        initRecipe();

    }

    public void GameStart()
    {
        sceneLoader.StartGame();
    }

    void Start()
    {
        StartCoroutine(Test());
    }


    public void GameClear()
    {
        SceneManager.LoadScene("GameClearScene");
    }

    public string GetTotalTimeString()
    {
        // totaltime을 분:초 로 반환한다 ex) 05:33
        int minutes = (int)(totalTime / 60);
        int seconds = (int)(totalTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(5);
        go.StartShow();
    }
}
