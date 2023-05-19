using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int itemID; // 아이템의 고유 ID값. 중복 불가능. (10001, 10002)
    public string itemName; // 아이템의 이름. 중복 가능/ (ex. 열쇠,열쇠 ) -> 하지만 아이템별로 이름을 모두 다르게 해줄 예정
    public string itemDescription; // 아이템 설명.
    public int itemCount; // 소지 개수.
    public Sprite itemIcon;// 아이템의 아이콘.
    public ItemType itemType;

    public enum ItemType
    {
        Use, // 소모품 아이템
        Equip, // 장비 아이템
        Quest, // 퀘스트 아이템 -> 만들고자 하는 게임에서는 퀘스트 아이템만 사용됨
        ETC // 기타 아이템
    }

    public Item(int _itemID, string _itemName, string _itemDes, ItemType _itemType, int _itemCount = 1)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemType = _itemType;
        itemCount = _itemCount;
        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
