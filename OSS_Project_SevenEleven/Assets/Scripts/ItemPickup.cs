using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int itemID; // 데이터베이스의 ItemID와 비교한다. 일치하는 경우 인벤토리에 추가함
    public int _count;
    public string pickUpSound; // 아이템 획득시 사운드

    public bool isPick = false;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Z)) // Z키 누르면 아이템 획득
        {
            AudioManager.instance.Play(pickUpSound);
            Inventory.instance.GetAnItem(itemID, _count); // 인벤토리에 획득한 아이템 추가하는 과정
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
            isPick = true;
        }
    }
}
