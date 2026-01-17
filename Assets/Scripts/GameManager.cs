using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private SceneLoader sceneLoader;

    [SerializeField] private int _life = 5;
    public float life { get => _life; }
    public void loseLife() { _life -= 1; }
    public void getLife(int num) { _life += num; }

    [SerializeField] private bool _isPlay;
    public bool isPlay { get => _isPlay; }

    [SerializeField] private int _roundCount = 15;
    public int roundCount { get => _roundCount; }

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
