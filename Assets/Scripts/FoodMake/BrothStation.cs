using UnityEngine;
using UnityEngine.EventSystems;

public class BrothStation : FoodStation
{
    [SerializeField]
    SpriteRenderer sr;
    public bool isHide = false;

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        if (mouseHand.Gethand() != null) 
        {
            bool SameName = mouseHand.Gethand().name.ToString().Replace("(Clone)", "") == IngerdentPrefab.name;
            if (SameName)
            {
                isHide = true;
            }
            else if (!SameName)
            {
                isHide = false;
            }
           
        } if (mouseHand.Gethand() == null) 
        { isHide = false; }

        HoidAtion();
    }
    public void HoidAtion() 
    {
      
        if (isHide)
        {
            Debug.Log("¼ûÀ½");
            sr.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            Debug.Log("³ªÅ¸³²");
            sr.color = new Color(1f, 1f, 1f, 1f);
        }
    }
    
}
