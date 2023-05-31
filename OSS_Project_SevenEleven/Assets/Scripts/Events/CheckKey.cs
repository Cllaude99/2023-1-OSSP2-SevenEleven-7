using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CheckKey : MonoBehaviour
{

    public int checkIndex;

    public DatabaseManager theDB;

    public int[] itemcode;

    public GameObject[] door;

    private AudioManager theAudio;

    public string lockdoor;
    public string unlockdoor;
    // Start is called before the first frame update
    void Start()
    {
        theDB.GetComponent<DatabaseManager>();
        theAudio = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = 0; i < theDB.itemList.Count; i++)
            {
                if (theDB.itemList[i].itemID == itemcode[checkIndex])
                {
                    theDB.itemList.RemoveAt(i);
                    for (int j = 0; j < door.Length; j++)
                    {
                        theAudio.Play(unlockdoor);
                        door[j].SetActive(false);
                    }
                }
                else theAudio.Play(lockdoor);
            }
        }

    }
}
