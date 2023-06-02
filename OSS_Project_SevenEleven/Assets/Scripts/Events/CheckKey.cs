using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CheckKey : MonoBehaviour
{

    public int checkIndex;

    public Inventory theInven;

    public int[] itemcode;

    public GameObject[] door;

    private AudioManager theAudio;

    public string lockdoor;
    public string unlockdoor;

    private bool canOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        theInven = FindObjectOfType<Inventory>();
        theAudio = FindObjectOfType<AudioManager>();
    }



    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = 0; i < theInven.inventoryItemList.Count; i++)
            {
                if (theInven.inventoryItemList[i].itemID == itemcode[checkIndex])
                {
                    theInven.inventoryItemList.RemoveAt(i);
                    for (int j = 0; j < door.Length; j++)
                    {
                        door[j].SetActive(false);
                    }
                    canOpen = true;
                    break;
                }
            }
            if (!canOpen) theAudio.Play(lockdoor);
            else if (canOpen) theAudio.Play(unlockdoor);
        }

    }*/

    private bool isPlayerOn = false;                        // 박스 콜라이더와 플레이어가 겹쳐있는지 확인하는 변수
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))                 //플레이어가 박스 콜라이더 위에 있으면
            isPlayerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isPlayerOn)                                     //X키 누르고 playeron이면
            {
                for (int i = 0; i < theInven.inventoryItemList.Count; i++)
                {
                    if (theInven.inventoryItemList[i].itemID == itemcode[checkIndex])
                    {
                        theInven.inventoryItemList.RemoveAt(i);
                        for (int j = 0; j < door.Length; j++)
                        {
                            door[j].SetActive(false);
                        }
                        canOpen = true;
                        break;
                    }
                }
                if (!canOpen) theAudio.Play(lockdoor);
                else if (canOpen) theAudio.Play(unlockdoor);
            }

        }
    }


}
