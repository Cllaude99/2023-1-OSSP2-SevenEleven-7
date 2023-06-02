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
    public string amulte_use_sound;



    // Start is called before the first frame update
    void Start()
    {
        theInven = FindObjectOfType<Inventory>();
        theAudio = FindObjectOfType<AudioManager>();
        theAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            
            for (int i = 0; i < theInven.inventoryItemList.Count; i++)
            {
                if (theInven.inventoryItemList[i].itemID == itemcode[checkIndex])
                {
                    theInven.inventoryItemList.RemoveAt(i);
                    theAudio.Play("amulte_use_sound");
                    BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
                    boxCollider.enabled = false;                             //±Í½ÅÁ×À¸¸é ºê±Ý ²ô°í boxcollider off (Ãæµ¹¹æÁö);
                    theAnimator.SetBool("Use", true);
                    //Invoke("setGhostDeath", 3f);
                    setGhostDeath();
                    break;
                }
            }

        }

    }

    void setGhostDeath()
    {
        theGhostManager.ghostdeath= true;
    }
}
