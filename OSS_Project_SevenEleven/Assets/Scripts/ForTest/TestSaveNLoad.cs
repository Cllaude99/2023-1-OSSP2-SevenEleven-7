using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveNLoad : MonoBehaviour
{
    // 현재 상태 저장 및 로드를 위한 스크립트

    //For Initialization
    private PlayerManager thePlayer;
    private CameraManager theCamera;

    private DatabaseManager theDatabase; //
    private Inventory theInven; //
    public List<string> item_id__should_destroy;//

    private GameObject VisitManager;
    private GameObject[] ChildVisitManager;
    private int CheckVisitLength;

    //Save N Load File
    public TestSaveFile[] testSaveFile;
    public int item_count;
    private int FileIndex;

    public void Start()
    {
        testSaveFile = new TestSaveFile[3]; //총 3개의 세이브 파일
        thePlayer = FindObjectOfType<PlayerManager>();
        theCamera = FindObjectOfType<CameraManager>();

        theDatabase = FindObjectOfType<DatabaseManager>();//
        theInven = FindObjectOfType<Inventory>();//
    }

    private void callSave()
    {
        testSaveFile[FileIndex].PlayerPos = thePlayer.transform.position;
        testSaveFile[FileIndex].currentBound = theCamera.bound;

        VisitManager = GameObject.Find("VisitManager");
        CheckVisitLength = VisitManager.transform.childCount;
        ChildVisitManager = new GameObject[CheckVisitLength];

        for (int i = 0; i < CheckVisitLength; i++)
        {
            ChildVisitManager[i] = VisitManager.transform.GetChild(i).gameObject;
        }

        foreach (GameObject obj in ChildVisitManager)
        {
            if (!obj.activeSelf) testSaveFile[FileIndex].isVisitCheck.Add(false);
            else testSaveFile[FileIndex].isVisitCheck.Add(true);
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

        /////////////////////////// 이 이후코드
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