using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SaveNLoad : MonoBehaviour
{
    // 현재 상태 저장 및 로드를 위한 스크립트


    //For Initialization
    public GameObject GhostPrefab;

    public PlayerManager thePlayer;
    public CameraManager theCamera;
    public NPCManager theNPC;

    public DatabaseManager theDatabase; //
    public Inventory theInven; //
    public List<string> item_id__should_destroy;//

    public GameObject VisitManager;
    public GameObject[] VisitManagerChild;

    public GameObject KeyManager;
    public GameObject[] KeyManagerChild;

    public GameObject SpawnManager;
    public GameObject[] SpawnManagerChild;

    public GameObject ActiveManager;
    public GameObject[] ActiveManagerChild;

    public GameObject TextManager;
    public GameObject[] TextManagerChild;


    //Save N Load File
    public SaveFile[] saveFile;
    public int SaveFileNum;
    public int item_count;
    public int FileIndex;

    public void Start()
    {
        //Test Save File
        saveFile = new SaveFile[SaveFileNum]; //총 3개의 세이브 파일  //4번째 세이브파일은 새로하기용세이브파일
        saveFile = FindObjectsOfType<SaveFile>();
        //세이브 파일 정렬
        Array.Sort(saveFile, (a, b) =>
        {
            return a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex());
        });

        thePlayer = FindObjectOfType<PlayerManager>();
        theCamera = FindObjectOfType<CameraManager>();
        theNPC = FindObjectOfType<NPCManager>();

        theDatabase = FindObjectOfType<DatabaseManager>();//
        theInven = FindObjectOfType<Inventory>();//

        
    }

    private void callSave()
    {
        //초기 설정
        saveFile[FileIndex].PlayerPos = thePlayer.transform.position;
        saveFile[FileIndex].CameraPos = theCamera.transform.position;
        saveFile[FileIndex].NPCPos = theNPC.transform.position;
        saveFile[FileIndex].currentBound = theCamera.bound;
        
        //리스트 초기화
        saveFile[FileIndex].confirmVisit.Clear();
        saveFile[FileIndex].confirmKeySpawn.Clear();
        saveFile[FileIndex].GhostSpawn.Clear();
        saveFile[FileIndex].ObjectActive.Clear();
        saveFile[FileIndex].isTextEnter.Clear();



        //VisitManager
        VisitManager = GameObject.Find("VisitManager");
        VisitManagerChild = new GameObject[VisitManager.transform.childCount]; 
        for (int i = 0; i < VisitManager.transform.childCount; i++)
        {
            VisitManagerChild[i] = VisitManager.transform.GetChild(i).gameObject;
            saveFile[FileIndex].confirmVisit.Add(VisitManagerChild[i].GetComponent<checkVisit>().confirmvisitnum);
        }

        //KeyManager
        KeyManager = GameObject.Find("KeyManager");
        KeyManagerChild = new GameObject[KeyManager.transform.childCount];
        for (int i = 0; i < KeyManager.transform.childCount; i++)
        {
            KeyManagerChild[i] = KeyManager.transform.GetChild(i).gameObject;
            saveFile[FileIndex].confirmKeySpawn.Add(KeyManagerChild[i].GetComponent<SpawnKey>().visit);
        }

        //SpawnManager
        SpawnManager = GameObject.Find("SpawnManager");
        SpawnManagerChild = new GameObject[SpawnManager.transform.childCount];
        for (int i = 0; i < SpawnManager.transform.childCount; i++)
        {
            SpawnManagerChild[i] = SpawnManager.transform.GetChild(i).gameObject;
            saveFile[FileIndex].GhostSpawn.Add(SpawnManagerChild[i].activeSelf);
        }

        //ActiveManager
        ActiveManager = GameObject.Find("ActiveManager");
        ActiveManagerChild = new GameObject[ActiveManager.transform.childCount];
        for(int i = 0; i < ActiveManager.transform.childCount; i++)
        {
            ActiveManagerChild[i] = ActiveManager.transform.GetChild(i).gameObject;
            saveFile[FileIndex].ObjectActive.Add(ActiveManagerChild[i].activeSelf);
        }

        //TextManager
        TextManager = GameObject.Find("TextManager");
        TextManagerChild = new GameObject[TextManager.transform.childCount];
        for (int i = 0; i < TextManager.transform.childCount; i++)
        {
            TextManagerChild[i] = TextManager.transform.GetChild(i).gameObject;
            if(TextManagerChild[i].GetComponent<TestDialogue>()!=null)
                saveFile[FileIndex].isTextEnter.Add(TextManagerChild[i].GetComponent<TestDialogue>().hasEntered);
            else
                saveFile[FileIndex].isTextEnter.Add(TextManagerChild[i].GetComponent<StartStory>().hasEntered);
        }

        saveFile[FileIndex].playerItemInventory.Clear();//
        saveFile[FileIndex].playerItemInventoryCount.Clear();//

        List<Item> itemList = theInven.SaveItem();//

        for (int i = 0; i < itemList.Count; i++)//
        {
            saveFile[FileIndex].playerItemInventory.Add(itemList[i].itemID);//
            saveFile[FileIndex].playerItemInventoryCount.Add(itemList[i].itemCount);//
        }
    }

    private void callLoad()
    {
        thePlayer.transform.position = saveFile[FileIndex].PlayerPos;
        theNPC.transform.position = saveFile[FileIndex].NPCPos;
        theCamera.bound = saveFile[FileIndex].currentBound;
        theCamera.minBound = saveFile[FileIndex].currentBound.bounds.min;
        theCamera.maxBound = saveFile[FileIndex].currentBound.bounds.max;
        theCamera.transform.position = saveFile[FileIndex].CameraPos;

        if(thePlayer.ghostlive == true)
            Destroy(GameObject.Find(GhostPrefab.name));

        //VisitManager
        VisitManager = GameObject.Find("VisitManager");
        VisitManagerChild = new GameObject[VisitManager.transform.childCount];
        for (int i = 0; i < VisitManager.transform.childCount; i++)
        {
            VisitManagerChild[i] = VisitManager.transform.GetChild(i).gameObject;
            VisitManagerChild[i].gameObject.SetActive(true);
            VisitManagerChild[i].GetComponent<checkVisit>().confirmvisitnum =
                saveFile[FileIndex].confirmVisit[i];
        }

        //KeyManager
        KeyManager = GameObject.Find("KeyManager");
        KeyManagerChild = new GameObject[KeyManager.transform.childCount];
        for (int i = 0; i < KeyManager.transform.childCount; i++)
        {
            KeyManagerChild[i] = KeyManager.transform.GetChild(i).gameObject;
            KeyManagerChild[i].gameObject.SetActive(true);
            KeyManagerChild[i].GetComponent<SpawnKey>().visit = saveFile[FileIndex].confirmKeySpawn[i];
        }

        //SpawnManager
        SpawnManager = GameObject.Find("SpawnManager");
        SpawnManagerChild = new GameObject[SpawnManager.transform.childCount];
        for (int i = 0; i < SpawnManager.transform.childCount; i++)
        {
            SpawnManagerChild[i] = SpawnManager.transform.GetChild(i).gameObject;
            SpawnManagerChild[i].SetActive(saveFile[FileIndex].GhostSpawn[i]);
        }

        //ActiveManager
        ActiveManager = GameObject.Find("ActiveManager");
        ActiveManagerChild = new GameObject[ActiveManager.transform.childCount];
        for (int i = 0; i < ActiveManager.transform.childCount; i++)
        {
            ActiveManagerChild[i] = ActiveManager.transform.GetChild(i).gameObject;
            ActiveManagerChild[i].SetActive(saveFile[FileIndex].ObjectActive[i]);
        }

        //TextManager
        TextManager = GameObject.Find("TextManager");
        TextManagerChild = new GameObject[TextManager.transform.childCount];
        for (int i = 0; i < TextManager.transform.childCount; i++)
        {
            TextManagerChild[i] = TextManager.transform.GetChild(i).gameObject;
            if(TextManagerChild[i].GetComponent<TestDialogue>()!=null)
                TextManagerChild[i].GetComponent<TestDialogue>().hasEntered = saveFile[FileIndex].isTextEnter[i];
            else
                TextManagerChild[i].GetComponent<StartStory>().hasEntered = saveFile[FileIndex].isTextEnter[i];
        }


        List<Item> itemList = new List<Item>();

        for (int i = 0; i < saveFile[FileIndex].playerItemInventory.Count; i++)
        {
            for (int x = 0; x < theDatabase.itemList.Count; x++)
            {
                if (saveFile[FileIndex].playerItemInventory[i] == theDatabase.itemList[x].itemID)
                {
                    itemList.Add(theDatabase.itemList[x]);
                }
            }
        }

        for (int i = 0; i < saveFile[FileIndex].playerItemInventoryCount.Count; i++)
        {
            itemList[i].itemCount = saveFile[FileIndex].playerItemInventoryCount[i];
        }

        theInven.LoadItem(itemList);

        item_count = 0;
        for (int i = 0; i < saveFile[FileIndex].playerItemInventoryCount.Count; i++)
        {
            item_id__should_destroy.Add((saveFile[FileIndex].playerItemInventory[i]).ToString());
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
        if (!check_save_File_before_load(0))
        {
            FileIndex = 0;
            callLoad();
        }
    }

    public void callTestLoad2()
    {
        if (!check_save_File_before_load(1))
        {
            FileIndex = 1;
            callLoad();
        }
    }

    public void callTestLoad3()
    {
        if (!check_save_File_before_load(2))
        {
            FileIndex = 2;
            callLoad();
        }
    }

    public bool check_save_File_before_load(int i)       //빈 파일이면 true
    {
        if (saveFile[i].CameraPos.x==0) //빈 세이브파일의 카메라pos -> 0
            return true;
        else
            return false;
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