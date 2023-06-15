using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SaveNLoad : MonoBehaviour
{
    // ���� ���� ���� �� �ε带 ���� ��ũ��Ʈ


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

    public GameObject theEventManager;
    public GameObject[] theEventManagerChild;
    public EventManager[] etc_Events;

    public GameObject GhostList;

    public GameObject ShowerBoothGhost;
    public GameObject ShowerGhostInstance;
    //Save N Load File
    public SaveFile[] saveFile;
    public int SaveFileNum;
    public int item_count;
    public int FileIndex;
    public GameObject items;

    public void Start()
    {
        //Test Save File
        saveFile = new SaveFile[SaveFileNum]; //�� 3���� ���̺� ����  //4��° ���̺������� �����ϱ�뼼�̺�����
        saveFile = FindObjectsOfType<SaveFile>();
        //���̺� ���� ����
        Array.Sort(saveFile, (a, b) =>
        {
            return a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex());
        });

        thePlayer = FindObjectOfType<PlayerManager>();
        theCamera = FindObjectOfType<CameraManager>();
        theNPC = FindObjectOfType<NPCManager>();

        theDatabase = FindObjectOfType<DatabaseManager>();//
        theInven = FindObjectOfType<Inventory>();//

        //������ �̺�Ʈ ���� �۾�
        ShowerBoothGhost = GameObject.Find("ShowerBoothGhost");
        ShowerGhostInstance = ShowerBoothGhost.transform.GetChild(0).gameObject;
        for (int i = 0; i < saveFile.Length; i++)
        {
            saveFile[i].isShowerGhost = true;
            saveFile[i].ShowerGhostPos = ShowerGhostInstance.transform.position;
        }

        etc_Events=GameObject.FindObjectsOfType<EventManager>();
        items = GameObject.Find("Items");
    }

    private void callSave()
    {
        //�ʱ� ����
        saveFile[FileIndex].PlayerPos = thePlayer.transform.position;
        saveFile[FileIndex].CameraPos = theCamera.transform.position;
        saveFile[FileIndex].NPCPos = theNPC.transform.position;
        saveFile[FileIndex].currentBound = theCamera.bound;

        //����Ʈ �ʱ�ȭ
        saveFile[FileIndex].confirmVisit.Clear();
        saveFile[FileIndex].confirmKeySpawn.Clear();
        saveFile[FileIndex].GhostSpawn.Clear();
        saveFile[FileIndex].ObjectActive.Clear();
        saveFile[FileIndex].isTextEnter.Clear();
        saveFile[FileIndex].isETCEventEnter.Clear();

        //������ �̺�Ʈ
        ShowerBoothGhost = GameObject.Find("ShowerBoothGhost");
        ShowerGhostInstance = ShowerBoothGhost.transform.GetChild(0).gameObject;

        if (!ShowerGhostInstance.activeSelf) saveFile[FileIndex].isShowerGhost = false;

        //���� ���̺� ������ �ͽ��� �ϳ��� ��� �ִٸ� true

        GhostList = GameObject.Find("GhostList");
        if (GhostList.transform.childCount > 0)
        {
            saveFile[FileIndex].isGhostLive = true;
            for (int i = 0; i < GhostList.transform.childCount; i++)
            {
                saveFile[FileIndex].GhostPos.Add(GhostList.transform.GetChild(i).position);
            }
        }
        else saveFile[FileIndex].isGhostLive = false;

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
        for (int i = 0; i < ActiveManager.transform.childCount; i++)
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
            saveFile[FileIndex].isTextEnter.Add(TextManagerChild[i].GetComponent<TestDialogue>().hasEntered);
        }

        saveFile[FileIndex].playerItemInventory.Clear();//
        saveFile[FileIndex].playerItemInventoryCount.Clear();//

        List<Item> itemList = theInven.SaveItem();//

        for (int i = 0; i < itemList.Count; i++)//
        {
            saveFile[FileIndex].playerItemInventory.Add(itemList[i].itemID);//
            saveFile[FileIndex].playerItemInventoryCount.Add(itemList[i].itemCount);//
        }

        //etc event manager
        theEventManager = GameObject.Find("ETCEventManager");
        theEventManagerChild = new GameObject[theEventManager.transform.childCount];
        for (int i = 0; i < theEventManager.transform.childCount; i++)
        {
            theEventManagerChild[i] = theEventManager.transform.GetChild(i).gameObject;
            saveFile[FileIndex].isETCEventEnter.Add(theEventManagerChild[i].GetComponent<EventManager>().is_event_activated);
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

        //������ �̺�Ʈ
        ShowerBoothGhost = GameObject.Find("ShowerBoothGhost");
        ShowerGhostInstance = ShowerBoothGhost.transform.GetChild(0).gameObject;
        if (saveFile[FileIndex].isShowerGhost)
        {
            ShowerGhostInstance.SetActive(true);
            ShowerGhostInstance.transform.position = saveFile[FileIndex].ShowerGhostPos;
        }



        //�ͽ� �ε�
        if (!saveFile[FileIndex].isGhostLive)
        {
            GhostList = GameObject.Find("GhostList");
            for (int i = 0; i < GhostList.transform.childCount; i++)
            {
                Destroy(GhostList.transform.GetChild(i).gameObject);
            }
        }
        else
        {
            for (int i = 0; i < GhostList.transform.childCount; i++)
            {
                GhostList.transform.GetChild(i).position = saveFile[FileIndex].GhostPos[i];
            }
        }

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
            TextManagerChild[i].GetComponent<TestDialogue>().hasEntered = saveFile[FileIndex].isTextEnter[i];
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
                else
                {
                    foreach(Transform child in items.transform)
                    {
                        if (child.name == theDatabase.itemList[x].itemID.ToString() && child.name != "10026" && child.name != "10027" && child.name != "10029")
                        {
                            child.gameObject.SetActive(true);
                        }
                    }
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

        Reset_Etc_Event();  //��Ÿ �̺�Ʈ bool �ʱ�ȭ
        for (int i = 0; i < theEventManager.transform.childCount; i++)
        {
            theEventManagerChild[i] = theEventManager.transform.GetChild(i).gameObject;
            theEventManagerChild[i].GetComponent<EventManager>().is_event_activated = saveFile[FileIndex].isETCEventEnter[i];
        }
        Update_Etc_Event();

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

    public bool check_save_File_before_load(int i)       //�� �����̸� true
    {
        if (saveFile[i].CameraPos.x == 0) //�� ���̺������� ī�޶�pos -> 0
            return true;
        else
            return false;
    }


    public void MakeDeafultSaveFile()       //����Ʈ���̺����ϻ���
    {
        FileIndex = 3;
        callSave();
        Debug.Log("����Ʈ ���� ���� �ϰ� �ε����� " + FileIndex);
    }

    public void CallNewGame()               //���ε��� ���̺����Ϸε�
    {
        ResetAllSaveFiles();
        FileIndex = 3;
        callLoad();
        Debug.Log("����Ʈ ���� �ε� �ϰ� �ε����� " + FileIndex);
    }


    public void Reset_Etc_Event()
    {
        foreach (EventManager e in etc_Events)
        {
            e.ResetEventBool();
        }
    }

    public void Update_Etc_Event()
    {
        foreach (EventManager e in etc_Events)
        {
            e.UpdateEventBool();
        }
    }

    public void ResetAllSaveFiles()
    {
        for (int i = 0; i < 3; i++)
        {
            saveFile[i].CameraPos.x = 0;
        }
    }
}