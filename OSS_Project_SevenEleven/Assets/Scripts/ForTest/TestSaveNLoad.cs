using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveNLoad : MonoBehaviour
{
    // 현재 상태 저장 및 로드를 위한 스크립트

    //For Initialization
    public PlayerManager thePlayer;
    public CameraManager theCamera;

    public DatabaseManager theDatabase; //
    public Inventory theInven; //
    public List<string> item_id__should_destroy;//

    public GameObject VisitManager;
    public GameObject[] ChildVisitManager;
    public int CheckVisitLength;

    public checkVisit[] checkVisits;

    //Save N Load File
    public TestSaveFile[] testSaveFile;
    public int item_count;
    public int FileIndex;

    public void Start()
    {
        testSaveFile = new TestSaveFile[3]; //총 3개의 세이브 파일

        for (int i = 0; i <testSaveFile.Length; i++)
        {
            testSaveFile[i] = GetComponent<TestSaveFile>();
        }

        thePlayer = FindObjectOfType<PlayerManager>();
        theCamera = FindObjectOfType<CameraManager>();

        theDatabase = FindObjectOfType<DatabaseManager>();//
        theInven = FindObjectOfType<Inventory>();//

        checkVisits = FindObjectsOfType<checkVisit>();
    }

    private void callSave()
    {
        testSaveFile[FileIndex].PlayerPos = thePlayer.transform.position;
        testSaveFile[FileIndex].currentBound = theCamera.bound;

        for (int i = 0; i < checkVisits.Length; i++)
        {
            testSaveFile[FileIndex].confirmVisit.Add(checkVisits[i].confirmvisitnum);
        }


        testSaveFile[FileIndex].playerItemInventory.Clear();//
        testSaveFile[FileIndex].playerItemInventoryCount.Clear();//

        List<Item> itemList = theInven.SaveItem();//

        for (int i = 0; i < itemList.Count; i++)//
        {
            testSaveFile[FileIndex].playerItemInventory.Add(itemList[i].itemID);//
            testSaveFile[FileIndex].playerItemInventoryCount.Add(itemList[i].itemCount);//
        }
    }

    private void callLoad()
    {
        thePlayer.transform.position = testSaveFile[FileIndex].PlayerPos;
        theCamera.bound = testSaveFile[FileIndex].currentBound;

        for (int i = 0; i < checkVisits.Length; i++)
        {
            //파일에 인덱스에 맞는 confirmvisit들을 불러옴
            checkVisits[i].confirmvisitnum = testSaveFile[FileIndex].confirmVisit[i];
        }

        List<Item> itemList = new List<Item>();

        for (int i = 0; i < testSaveFile[FileIndex].playerItemInventory.Count; i++)
        {
            for (int x = 0; x < theDatabase.itemList.Count; x++)
            {
                if (testSaveFile[FileIndex].playerItemInventory[i] == theDatabase.itemList[x].itemID)
                {
                    itemList.Add(theDatabase.itemList[x]);
                }
            }
        }

        for (int i = 0; i < testSaveFile[FileIndex].playerItemInventoryCount.Count; i++)
        {
            itemList[i].itemCount = testSaveFile[FileIndex].playerItemInventoryCount[i];
        }

        theInven.LoadItem(itemList);

        item_count = 0;
        for (int i = 0; i < testSaveFile[FileIndex].playerItemInventoryCount.Count; i++)
        {
            item_id__should_destroy.Add((testSaveFile[FileIndex].playerItemInventory[i]).ToString());
            item_count++;
        }
    }



    public void callTestSave1()
    {
        FileIndex = 0;
        callSave();
    }

    public void callTestSave2()
    {
        FileIndex = 1;
        callSave();
    }

    public void callTestSave3()
    {
        FileIndex = 2;
        callSave();
    }


    public void callTestLoad1()
    {
        FileIndex = 0;
        callLoad();
    }

    public void callTestLoad2()
    {
        FileIndex = 1;
        callLoad();
    }

    public void callTestLoad3()
    {
        FileIndex = 2;
        callLoad();
    }
}