using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

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
    public GameObject[] VisitManagerChild;

    public GameObject KeyManager;
    public GameObject[] KeyManagerChild;

    public GameObject SpawnManager;
    public GameObject[] SpawnManagerChild;


    //Save N Load File
    public TestSaveFile[] testSaveFile;
    public int item_count;
    public int FileIndex;

    public void Start()
    {
        //Test Save File
        testSaveFile = new TestSaveFile[4]; //총 3개의 세이브 파일  //4번째 세이브파일은 새로하기용세이브파일
        testSaveFile = FindObjectsOfType<TestSaveFile>();
        //세이브 파일 정렬
        Array.Sort(testSaveFile, (a, b) =>
        {
            return a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex());
        });

        thePlayer = FindObjectOfType<PlayerManager>();
        theCamera = FindObjectOfType<CameraManager>();

        theDatabase = FindObjectOfType<DatabaseManager>();//
        theInven = FindObjectOfType<Inventory>();//

        
    }

    private void callSave()
    {
        //초기 설정
        testSaveFile[FileIndex].PlayerPos = thePlayer.transform.position;
        testSaveFile[FileIndex].CameraPos = theCamera.transform.position;
        testSaveFile[FileIndex].currentBound = theCamera.bound;
        
        //리스트 초기화
        testSaveFile[FileIndex].confirmVisit.Clear();
        testSaveFile[FileIndex].confirmKeySpawn.Clear();
        testSaveFile[FileIndex].GhostSpawn.Clear();



        //VisitManager
        VisitManager = GameObject.Find("VisitManager");
        VisitManagerChild = new GameObject[VisitManager.transform.childCount]; 
        for (int i = 0; i < VisitManager.transform.childCount; i++)
        {
            VisitManagerChild[i] = VisitManager.transform.GetChild(i).gameObject;
            testSaveFile[FileIndex].confirmVisit.Add(VisitManagerChild[i].GetComponent<checkVisit>().confirmvisitnum);
        }

        //KeyManager
        KeyManager = GameObject.Find("KeyManager");
        KeyManagerChild = new GameObject[KeyManager.transform.childCount];
        for (int i = 0; i < KeyManager.transform.childCount; i++)
        {
            KeyManagerChild[i] = KeyManager.transform.GetChild(i).gameObject;
            testSaveFile[FileIndex].confirmKeySpawn.Add(KeyManagerChild[i].GetComponent<SpawnKey>().visit);
        }

        //SpawnManager
        SpawnManager = GameObject.Find("SpawnManager");
        SpawnManagerChild = new GameObject[SpawnManager.transform.childCount];
        for (int i = 0; i < SpawnManager.transform.childCount; i++)
        {
            SpawnManagerChild[i] = SpawnManager.transform.GetChild(i).gameObject;
            testSaveFile[FileIndex].GhostSpawn.Add(SpawnManagerChild[i].activeSelf);
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
        theCamera.minBound = testSaveFile[FileIndex].currentBound.bounds.min;
        theCamera.maxBound = testSaveFile[FileIndex].currentBound.bounds.max;
        theCamera.transform.position = testSaveFile[FileIndex].CameraPos;

        //VisitManager
        VisitManager = GameObject.Find("VisitManager");
        VisitManagerChild = new GameObject[VisitManager.transform.childCount];
        for (int i = 0; i < VisitManager.transform.childCount; i++)
        {
            VisitManagerChild[i] = VisitManager.transform.GetChild(i).gameObject;
            VisitManagerChild[i].GetComponent<checkVisit>().confirmvisitnum =
                testSaveFile[FileIndex].confirmVisit[i];
        }

        //KeyManager
        KeyManager = GameObject.Find("KeyManager");
        KeyManagerChild = new GameObject[KeyManager.transform.childCount];
        for (int i = 0; i < KeyManager.transform.childCount; i++)
        {
            KeyManagerChild[i] = KeyManager.transform.GetChild(i).gameObject;
            KeyManagerChild[i].GetComponent<SpawnKey>().visit = testSaveFile[FileIndex].confirmKeySpawn[i];
        }

        //SpawnManager
        SpawnManager = GameObject.Find("SpawnManager");
        SpawnManagerChild = new GameObject[SpawnManager.transform.childCount];
        for (int i = 0; i < SpawnManager.transform.childCount; i++)
        {
            SpawnManagerChild[i] = SpawnManager.transform.GetChild(i).gameObject;
            SpawnManagerChild[i].SetActive(testSaveFile[FileIndex].GhostSpawn[i]);
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


    public void callTestLoadFromAnotherScene1()
    {
        SceneManager.LoadScene("StartScene");
        FileIndex = 0;
        callLoad();
    }

    public void callTestLoadFromAnotherScene2()
    {
        SceneManager.LoadScene("StartScene");
        FileIndex = 1;
        callLoad();
    }
    public void callTestLoadFromAnotherScene3()
    {
        SceneManager.LoadScene("StartScene");
        FileIndex = 2;
        callLoad();
    }

    public void MakeDeafultSaveFile()       //디폴트세이브파일생성
    {
        FileIndex = 3;
        callSave();
        Debug.Log("디폴트 파일 생성 하고 인덱스는 " + FileIndex);
    }

    public void CallNewGame()               //씬로드후 세이브파일로드
    {
        FileIndex = 3;
        callLoad();
        Debug.Log("디폴트 파일 로드 하고 인덱스는 " + FileIndex);
    }

}