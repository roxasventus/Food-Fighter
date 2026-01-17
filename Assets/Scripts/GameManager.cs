using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private SceneLoader sceneLoader;

    [SerializeField] private int _life = 5;
    public float life { get => _life; }
    public void loseLife() { 
        heartContainers.GetChild(_life-1).gameObject.SetActive(false);    
        _life -= 1;
        if (_life == 0)
            SceneLoader.Instance.Load("MainScene");
    }
    public void getLife(int num) { 

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

    [SerializeField] private int _roundCount = 15;
    public int roundCount { get => _roundCount; }
    public void clearRound() { 
        _roundCount--;

        BroadcastMessage("progressBarUpdate");


    }

    [SerializeField] private int _score = 0;
    public int score { get => _score; }
    public void loseScore(int num) { _score -= num; }
    public void getScore(int num) { _score += num; }


    [Header("Bullet")]
    // 장전이 되어 있는가?
    [SerializeField] private bool _isLoaded;
    public bool isLoaded { get => _isLoaded; }
    public void toogleLoaded() { _isLoaded = !_isLoaded; }

    // 선택된 탄환의 레시피
    [SerializeField] private Recipe _chosenRecipe;
    public Recipe chosenRecipe { get => _chosenRecipe; }

    [Header("Items")]
    [SerializeField] private int _miwonCount;
    public int getMiwon { get => _miwonCount; }
    [SerializeField] private int _hotCount;
    public int getHot { get => _hotCount; }
    [SerializeField] private int _oliveCount;
    public int getOlive { get => _oliveCount; }

    public void getItem(int index) {
        if (index == 0)
        {
            _miwonCount++;
        }

        if (index == 1)
        {
            _hotCount++;
        }

        if (index == 2)
        {
            _oliveCount++;
        }
    }

    public void getRandomItems(int num)
    {
        if (num > 2) {
            Debug.LogWarning(num+"개의 아이템은 가져올 수 없음");
            return;
        }

        int[] copy = { 0, 1, 2 };

        for (int i = 0; i < num; i++)
        {
            int rand = Random.Range(0, copy.Length);
            getItem(copy[rand]);
        }


    }

    [Header("UI")]
    [SerializeField] private Slider progressBar;
    [SerializeField] private Image progressBarFill;
    [SerializeField] private RectTransform heartContainers;
    public void progressBarUpdate() { 
        progressBar.value = 1-_roundCount/15f;
        if (progressBar.value > 0.5) { 
            progressBarFill.color = Color.yellow;
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
        else {
            _chosenRecipe.SetSpecial(Recipe.Special.none);
        }

    }

    public void useItem()
    {

        if (_chosenRecipe.GetSpecial == Recipe.Special.none)
        {
           return;
        }

        if (_chosenRecipe.GetSpecial == Recipe.Special.miwon && _miwonCount > 0) {
            _miwonCount--;
        }

        if (_chosenRecipe.GetSpecial == Recipe.Special.hot && _hotCount > 0)
        {
            _hotCount--;
        }

        if (_chosenRecipe.GetSpecial == Recipe.Special.olive && _oliveCount > 0)
        {
            _oliveCount--;
        }
        _chosenRecipe.SetSpecial(Recipe.Special.none);

    }

    private void Awake()
    {
        instance = this;
        initRecipe();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneLoader.StartGame();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
