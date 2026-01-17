using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private SceneLoader sceneLoader;

    [SerializeField] private int _life = 5;
    public float life { get => _life; }
    public void loseLife(int num) { _life -= num; }
    public void getLife(int num) { _life += num; }

    [SerializeField] private bool _isPlay;
    public bool isPlay { get => _isPlay; }

    [SerializeField] private float _time;
    public float time { get => _time; }

    [Header("Bullet")]
    // 장전이 되어 있는가?
    [SerializeField] private bool _isLoaded;
    public bool isLoaded { get => _isLoaded; }
    public void toogleLoaded() { _isLoaded = !_isLoaded; }

    // 선택된 탄환의 레시피
    [SerializeField] private Recipe _chosenRecipe;
    public Recipe chosenRecipe { get => _chosenRecipe; }

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
        _chosenRecipe.SetSpecial(recipe.GetSpecial);
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
        if (isPlay)
            _time -= Time.deltaTime;
    }
}
