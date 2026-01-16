using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private float _time;
    public float time { get => _time; }

    [SerializeField] private Recipe _chosenRecipe;
    public Recipe chosenRecipe { get => _chosenRecipe; }

    public void setRecipe(Recipe recipe) { 
        _chosenRecipe = recipe;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
