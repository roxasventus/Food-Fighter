using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] Animator chefAnim;
    private InputActions inputActions;
    private Vector2 moveInput;

    private FavoriteFood? loadedFood;

    [SerializeField] GameConstants gc;
    [SerializeField] GameObject foodDisplay;

    [SerializeField] Sprite rm;
    [SerializeField] Sprite jrm;
    [SerializeField] Sprite tb;
    [SerializeField] Sprite jtb;

    [SerializeField] Transform truck;

    [SerializeField] EnemySpawner spawner;

    [SerializeField] Pause p;

    private Dictionary<FavoriteFood, Sprite> f2s = new Dictionary<FavoriteFood, Sprite>();

    void Awake()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();

        f2s[FavoriteFood.RM] = rm;
        f2s[FavoriteFood.JRM] = jrm;
        f2s[FavoriteFood.TB] = tb;
        f2s[FavoriteFood.JTB] = jtb;
    }
    void Start()
    {
        if(chefAnim == null)
        {
            truck.transform.GetChild(0).TryGetComponent<Animator>(out chefAnim); //! 위험한 임시 코드입니다 반드시 chefAnim SerializeField에 할당해주세요.
        }
    }

    void Update()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        Vector2 target = transform.position + (Vector3) (moveInput * gc.focusSpeed * Time.deltaTime);
        target.x = Mathf.Clamp(target.x, gc.focusXClamp[0], gc.focusXClamp[1]);
        target.y = Mathf.Clamp(target.y, gc.focusYClamp[0], gc.focusYClamp[1]);

        transform.position = target;

        // 발사를 하는가?
        if (inputActions.Player.Confirm.WasPressedThisFrame())
        {
            Fire();
        }

        if (inputActions.Player.Test.WasPressedThisFrame())
        {
            loadedFood = FavoriteFood.RM;
        }

        // 일시정지를 하는가?
        if (inputActions.Player.Pause.WasPressedThisFrame())
        {
            p.StartShow();
        }

        // 로드 신호가 왔는가?
        if (GameManager.instance.isLoaded)
        {
            GameManager.instance.toogleLoaded();
            
            // 음식 저장
            loadedFood = Recipe2food(GameManager.instance.chosenRecipe);

        }

        // 장전되었는가?
        if (loadedFood == null)
        {
            foodDisplay.SetActive(false);
        }
        else
        {
            UpdateFoodDisplay();
            foodDisplay.SetActive(true);
        }
    }

    private void Fire()
    {
        if (loadedFood == null)
            return;
        
        Food f = ObjPoolManager.instance.InstantiateFromPool("Food").GetComponent<Food>();
        f.Init(
            f2s[(FavoriteFood) loadedFood],
            chefAnim.transform.position, 
            transform.position
        );
        f.type = (FavoriteFood) loadedFood;

        // 아이템 사용
        if (GameManager.instance.chosenRecipe.GetSpecial == Recipe.Special.miwon)
        {
            Debug.Log("미원 사용");
            f.transform.GetChild(0).gameObject.transform.localScale = Vector3.one * 2;
            GameManager.instance.chosenRecipe.SetSpecial(Recipe.Special.none);
        }
        else if (GameManager.instance.chosenRecipe.GetSpecial == Recipe.Special.olive)
        {
            Debug.Log("올리브 사용");
            GameManager.instance.getLife(1);
            GameManager.instance.chosenRecipe.SetSpecial(Recipe.Special.none);
        }
        else {
            f.transform.GetChild(0).gameObject.transform.localScale = Vector3.one;
        }

        //| Ani
        chefAnim.SetTrigger("Throw");
        //| SOUND
        SoundManager.instance.PlaySound("ThrowSwingWhoosh", 1f);

        spawner.AddFood(f);

        loadedFood = null;
    }

    private void UpdateFoodDisplay()
    {
        SpriteRenderer sr = foodDisplay.GetComponent<SpriteRenderer>();
        sr.sprite = f2s[(FavoriteFood) loadedFood];
    }

    private FavoriteFood? Recipe2food(Recipe r)
    {
        if (r.GetBase == Recipe.Base.noodle)
        {
            if (r.GetSauce == Recipe.Sauce.soup)
            {
                return FavoriteFood.RM;
            }
            else if (r.GetSauce == Recipe.Sauce.jjajang)
            {
                return FavoriteFood.JRM;
            }
            else
            {
                Debug.Log("소스 오류!");
                return null;
            }
            
        }
        else if (r.GetBase == Recipe.Base.tteok)
        {
            if (r.GetSauce == Recipe.Sauce.soup)
            {
                return FavoriteFood.TB;
            }
            else if (r.GetSauce == Recipe.Sauce.jjajang)
            {
                return FavoriteFood.JTB;
            }
            else
            {
                Debug.Log("소스 오류!");
                return null;
            }
        }
        else
        {
            Debug.Log("베이스 오류!");
            return null;
        }
    }
}