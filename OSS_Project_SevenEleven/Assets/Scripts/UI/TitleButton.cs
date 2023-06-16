using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Menu;

public class TitleButton : MonoBehaviour
{

    private bool first_new = true;
    private AudioManager theAudio;
    private PlayerManager thePlayerManager;
    private GameManager theGameManager;
    private SaveNLoad theTestSaveNLoad;
    public OrderManager theOrder;

    public GameObject theLoadUI;
    public GameObject theTitleUI;
    private GameObject theLogo;
    private GameObject theRun;
    private GameObject theRun2;


    private GameObject LoadBtn1;
    private GameObject LoadBtn2;
    private GameObject LoadBtn3;
    private bool isitFirstNew=true;
    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        thePlayerManager = FindObjectOfType<PlayerManager>();
        theGameManager= FindObjectOfType<GameManager>();
        theAudio.Play("TitleBGM");
        theTestSaveNLoad = FindObjectOfType<SaveNLoad>();
        theOrder = FindObjectOfType<OrderManager>();
        theOrder.PreLoadCharacter();
        theOrder.NotMove();
        Slot_Refresh_BeforeOpenUI(1, "Load");
        Slot_Refresh_BeforeOpenUI(2, "Load");
        Slot_Refresh_BeforeOpenUI(3, "Load");
    }

    // Update is called once per frame
    void Update()
    {

            
    }
    public void NewGame()                                  
    {
        if (isitFirstNew)
        {
            theTestSaveNLoad.MakeDeafultSaveFile();
            isitFirstNew= false;                      //ù���� �����̸� ������ ����Ʈ�����ͻ���
        }
        theAudio.Play("select2");
        theTestSaveNLoad.CallNewGame();
        theTitleUI.SetActive(false);
        theOrder.Move();
        theAudio.Stop("TitleBGM");
        DeleteJsonFile();

    }
    public void OpenTitleUI()
    {
        theOrder.NotMove();
        theTitleUI.SetActive(true); 
    }

    public void CloseTitleUI_loadbtn_1()
    {
        CloseTitleUI(0);
    }
    public void CloseTitleUI_loadbtn_2()
    {
        CloseTitleUI(1);
    }
    public void CloseTitleUI_loadbtn_3()
    {
        CloseTitleUI(2);
    }

    public void CloseTitleUI(int i)                              //�ε尡 �����Ǹ� ����Ǵ��Լ�
    {
        if (!theTestSaveNLoad.check_save_File_before_load(i))
        {
            theLoadUI.SetActive(false);
            theTitleUI.SetActive(false);
            theOrder.Move();
            theAudio.Stop("TitleBGM");
        }

    }

    public void OpenLoadUI()
    {
        theLogo = GameObject.Find("DDAYLogo");
        theLogo.GetComponent<Renderer>().enabled = false;

        theRun = GameObject.Find("CharacterRun");
        theRun.GetComponent<Renderer>().enabled = false;

        theRun2 = GameObject.Find("CharacterRun2");
        theRun2.GetComponent<Renderer>().enabled = false;//�̹��� �������� ��ħ������ loadâ�������� off

        theAudio.Play("select1");
        theLoadUI.SetActive(true);

        LoadBtn1 = GameObject.Find("Load_Button1");
        LoadBtn2 = GameObject.Find("Load_Button2");
        LoadBtn3 = GameObject.Find("Load_Button3");         

        LoadBtn1.GetComponent<Button>().onClick.AddListener(theTestSaveNLoad.callTestLoad1);
        LoadBtn2.GetComponent<Button>().onClick.AddListener(theTestSaveNLoad.callTestLoad2);
        LoadBtn3.GetComponent<Button>().onClick.AddListener(theTestSaveNLoad.callTestLoad3);            //�ٸ����� �ִ� ��ũ��Ʈ �Լ� �����ϱ����� addlister�� ��Ŭ���Լ��߰�

        Slot_Refresh_BeforeOpenUI(1, "Load");
        Slot_Refresh_BeforeOpenUI(2, "Load");
        Slot_Refresh_BeforeOpenUI(3, "Load");              //�ε嵥���� UI ����ȭ�� ( �÷���Ÿ��,������ġ)
    }
    public void Slot_Refresh_BeforeOpenUI(int i, string slot_type)  // slot_type(in hieraarchy) -> Load or Save
    {
        GameObject slot = GameObject.Find("Title"+slot_type + "Slot" + i);
        string filename = "Save_UI_Slot" + i;   // Save_UI_Slot1  -> ���ϸ�
        string path = Application.dataPath + "/" + filename + ".Json";        //���̺� ������ path
        bool fileExists = File.Exists(path);
        if (fileExists)
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);           //���̺� ������ �����Ѵٸ� Json���� ������ ���Կ� ���ش�
            slot.GetComponent<TextMeshProUGUI>().text =
                 "Saved Place : " + data.saved_map + "\n Play Time : " + data.playtime_minute + " Min " + data.playtime_seconds + " Sec";
        }
        else
        {
            slot.GetComponent<TextMeshProUGUI>().text = "Save File " + i;
        }
    }
    public void CloseLoadUI()
    {
        theAudio.Play("cancel1");
        theLoadUI.SetActive(false);
        theLogo.GetComponent<Renderer>().enabled = true;
        theRun.GetComponent<Renderer>().enabled = true;
        theRun2.GetComponent<Renderer>().enabled = true;
    }

    public void RunTitleAnimation()
    {
        theLogo = GameObject.Find("DDAYLogo");
        theRun = GameObject.Find("CharacterRun");
        theRun2 = GameObject.Find("CharacterRun2");

        theLogo.GetComponent<Renderer>().enabled = true;
        theRun.GetComponent<Renderer>().enabled = true;
        theRun2.GetComponent<Renderer>().enabled = true;
    }


    public void ExitGame()
    {
        Application.Quit();         //��������, �����Ϳ����� �ȸ���
    }

    public bool isFirstGame()
    {
        return first_new;           //�׽��ÿ�
    }

    public void DeleteJsonFile()
    {
        string filename;
        for (int i = 1; i < 4; i++)
        {
           
             filename = "Save_UI_Slot" + i;   // Save_UI_Slot1  -> ���ϸ�
            string path = Application.dataPath + "/" + filename + ".Json";        //���̺� ������ path
            bool fileExists = File.Exists(path);
            if (fileExists)
                System.IO.File.Delete(path);
        }
    }
}
