using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class Minimap : MonoBehaviour
{

    [SerializeField] private Image path1;
    [SerializeField] private Image path2;
    [SerializeField] private Image path3;

    Color[] colorList = {  Color.white, Color.green, Color.red, Color.magenta, Color.blue  };

    private void getRandomColor() {

        Color[] copy = (Color[])colorList.Clone();

        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(i, copy.Length);
            (copy[i], copy[rand]) = (copy[rand], copy[i]);
        }

        path1.color = copy[0];
        path2.color = copy[1];
        path3.color = copy[2];
    }

    
    public void colorPath(Image image){

        if (image.color == Color.white) {
            Debug.Log("일반적인 스테이지입니다.");
        }

        else if (image.color == Color.green)
        {
            Debug.Log("남은 생존 시간을 1 줄입니다. Normal과 같은 스테이지 세팅. ");
            GameManager.instance.clearRound();
        }

        else if (image.color == Color.red)
        {
            Debug.Log("어려우며 아이템을 2개 획득 가능합니다.");
            GameManager.instance.getRandomItems(2);
        }

        else if (image.color == Color.magenta)
        {
            Debug.Log("체력을 2 회복합니다. Normal과 같은 스테이지 세팅.");
            GameManager.instance.getLife(2);
        }

        else if (image.color == Color.blue)
        {
            Debug.Log("아이템을 획득 가능합니다. Normal과 같은 스테이지 세팅.");
            GameManager.instance.getRandomItems(1);
        }
        GameManager.instance.clearRound();
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        getRandomColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
