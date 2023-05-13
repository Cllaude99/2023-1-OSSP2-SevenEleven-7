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
    public bool[] switches; // 보스 정보에 대한 스위치.

    public List<Item> itemList = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        itemList.Add(new Item(10001, "부적", "왼쪽 위 부적", Item.ItemType.Quest));
        itemList.Add(new Item(10002, "일기장", "첫번째 일기장", Item.ItemType.Quest));
        itemList.Add(new Item(10003, "열쇠", "아이스페이스 열쇠", Item.ItemType.Quest));
    }

}
