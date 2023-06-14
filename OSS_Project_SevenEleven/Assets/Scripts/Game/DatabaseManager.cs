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



    // Start is called before the first frame update
    void Start()
    {
        itemList.Add(new Item(10001, "Top left amulet piece", "Top left amulet piece", Item.ItemType.Quest));
        itemList.Add(new Item(10002, "Top right amulet piece", "Top right amulet piece", Item.ItemType.Quest));
        itemList.Add(new Item(10003, "Bottom left amulet piece", "Bottom left amulet piece", Item.ItemType.Quest));
        itemList.Add(new Item(10004, "Bottom right amulet piece", "Bottom right amulet piece", Item.ItemType.Quest));
        itemList.Add(new Item(10005, "A buddhist exorcism amulet", "It seems to be usable at the altar near a Buddha statue", Item.ItemType.Quest));

        itemList.Add(new Item(10006, "1st Diary", "1st Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10007, "2nd Diary", "2nd Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10008, "3rd Diary", "3rd Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10009, "4th Diary", "4th Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10010, "5th Diary", "5th Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10011, "6th Diary", "6th Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10012, "7th Diary", "7th Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10013, "8th Diary", "8th Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10014, "9th Diary", "9th Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10015, "10th Diary", "10th Diary", Item.ItemType.Quest));

        itemList.Add(new Item(10016, "Diary", "1st Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10017, "Diary", "2nd Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10018, "Diary", "3rd Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10019, "Diary", "4th Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10020, "Diary", "5th Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10021, "Diary", "6th Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10022, "Diary", "7th Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10023, "Diary", "8th Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10024, "Diary", "9th Diary", Item.ItemType.Quest));
        itemList.Add(new Item(10025, "Diary", "10th Diary", Item.ItemType.Quest));
        

        itemList.Add(new Item(10026, "I.Space key", "Key to open the I.Space", Item.ItemType.Quest));
        itemList.Add(new Item(10027, "5th floor lab key", "Key to open the 5th-floor laboratory", Item.ItemType.Quest));

        itemList.Add(new Item(10028, "Student ID", "Student ID", Item.ItemType.Quest));
        itemList.Add(new Item(10029, "Wallet", "Wallet", Item.ItemType.Quest));
    }
}
