using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletUse : MonoBehaviour
{
    public int checkIndex;

    public Inventory theInven;

    public int[] itemcode;


    private AudioManager theAudio;
    private GhostManager theGhostManager;
    private Animator theAnimator;
    public string amulet_use_sound;



    // Start is called before the first frame update
    void Start()
    {
        theInven = FindObjectOfType<Inventory>();
        theAudio = FindObjectOfType<AudioManager>();
        theAnimator = GetComponent<Animator>();

    }


    private bool isPlayerOn = false;                        // 박스 콜라이더와 플레이어가 겹쳐있는지 확인하는 변수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))                 //플레이어가 박스 콜라이더 위에 있으면
            isPlayerOn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))                 //플레이어가 박스 콜라이더 위에 있으면
            isPlayerOn = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isPlayerOn)                                     //X키 누르고 playeron이면
            {
                

                for (int i = 0; i < theInven.inventoryItemList.Count; i++)
                {
                    if (theInven.inventoryItemList[i].itemID == itemcode[checkIndex])
                    {
                        theInven.inventoryItemList.RemoveAt(i);
                        theAudio.Play("amulet_use_sound");
                        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
                        boxCollider.enabled = false;                             //귀신죽으면 브금 끄고 boxcollider off (충돌방지);
                        theAnimator.SetBool("Use", true);
                        //Invoke("setGhostDeath", 3f);
                        setGhostDeath();
                        break;
                    }
                }

            }
        }
    }

    void setGhostDeath()
    {
        theGhostManager.ghostdeath= true;
    }
}
