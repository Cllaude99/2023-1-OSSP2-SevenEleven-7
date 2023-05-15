using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    static public DatabaseManager instance; //싱글톤

    private void Awake() 
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
    }

    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches; // 보스 출현정보에 대한 스위치.

    public List<Item> itemList = new List<Item>();

    /* public void UseItem(int _itemID) 소모품이 있는 경우 포함시키면 좋을 것 같습니다.(소모품이 없으면 제거해도 되는 코드) ex. 물약 
    {
        switch (_itemID)
        {
            case 10015:
                Debug.Log("아이스페이스가 열렸습니다.");
                break;            
        }
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        itemList.Add(new Item(10001, "1번째 부적", "왼쪽 위 부적", Item.ItemType.Quest));
        itemList.Add(new Item(10002, "2번째 부적", "오른쪽 위 부적", Item.ItemType.Quest));
        itemList.Add(new Item(10003, "3번째 부적", "왼쪽 아래 부적", Item.ItemType.Quest));
        itemList.Add(new Item(10004, "4번째 부적", "오른쪽 아래 부적", Item.ItemType.Quest));

        itemList.Add(new Item(10005, "1번째 일기장", "1번째 일기장", Item.ItemType.Quest));
        itemList.Add(new Item(10006, "2번째 일기장", "2번째 일기장", Item.ItemType.Quest));
        itemList.Add(new Item(10007, "3번째 일기장", "3번째 일기장", Item.ItemType.Quest));
        itemList.Add(new Item(10008, "4번째 일기장", "4번째 일기장", Item.ItemType.Quest));
        itemList.Add(new Item(10009, "5번째 일기장", "5번째 일기장", Item.ItemType.Quest));
        itemList.Add(new Item(10010, "6번째 일기장", "6번째 일기장", Item.ItemType.Quest));
        itemList.Add(new Item(10011, "7번째 일기장", "7번째 일기장", Item.ItemType.Quest));
        itemList.Add(new Item(10012, "8번째 일기장", "8번째 일기장", Item.ItemType.Quest));
        itemList.Add(new Item(10013, "9번째 일기장", "9번째 일기장", Item.ItemType.Quest));
        itemList.Add(new Item(10014, "10번째 일기장", "10번째 일기장", Item.ItemType.Quest));

        itemList.Add(new Item(10015, "아이스페이스 열쇠", "아이스페이스를 여는 열쇠", Item.ItemType.Quest));
        //열쇠부분은 추후에 더 추가
    }
}
