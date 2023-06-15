using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Menu;

public class GameOverButton : MonoBehaviour
{
    public GameObject theLoadUI;
    public GameObject gameDeathUI;
    public TitleButton thetitle; 
    private AudioManager theAudio;
    private PlayerManager thePlayerManager;
    private SaveNLoad theTestSaveNLoad;
    public OrderManager theOrder;

    private GameObject NewGameBtn;
    private GameObject LoadBtn1;
    private GameObject LoadBtn2;
    private GameObject LoadBtn3;



    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        thePlayerManager = FindObjectOfType<PlayerManager>();
        theTestSaveNLoad= FindObjectOfType<SaveNLoad>();
        thetitle=FindObjectOfType<TitleButton>();
        theOrder=FindObjectOfType<OrderManager>();  
    }

    // Update is called once per frame
    void Update()
    {

            
    }

    public void OpenDeathUI()
    {
        theOrder.NotMove();
        gameDeathUI.SetActive(true);

    }

    public void CloseDeathUI()
    {
        theOrder.Move();
        theLoadUI.SetActive(false);
        gameDeathUI.SetActive(false);
    }

    public void OpenLoadUI()
    {
        theAudio.Play("select1");
        theLoadUI.SetActive(true);
        LoadBtn1 = GameObject.Find("Load_Button1");
        LoadBtn2 = GameObject.Find("Load_Button2");
        LoadBtn3 = GameObject.Find("Load_Button3");
          
        LoadBtn1.GetComponent<Button>().onClick.AddListener(theTestSaveNLoad.callTestLoad1);           //다른씬에 있는 스크립트 함수 적용하기위해 addlister로 온클릭함수추가
        LoadBtn2.GetComponent<Button>().onClick.AddListener(theTestSaveNLoad.callTestLoad2);           //다른씬에 있는 스크립트 함수 적용하기위해 addlister로 온클릭함수추가
        LoadBtn3.GetComponent<Button>().onClick.AddListener(theTestSaveNLoad.callTestLoad3);           //다른씬에 있는 스크립트 함수 적용하기위해 addlister로 온클릭함수추가

        Slot_Refresh_BeforeOpenUI(1, "Load");
        Slot_Refresh_BeforeOpenUI(2, "Load");
        Slot_Refresh_BeforeOpenUI(3, "Load");    //로드데이터 UI 동기화용 ( 플레이타임,저장위치)
    }


    public void CloseLoadUI()
    {
        theAudio.Play("cancel1");
        theLoadUI.SetActive(false);
    }

    public void CloseDeathUIAndLoadNewGame()
    {
        thetitle.DeleteJsonFile();
        CloseDeathUI();
        theTestSaveNLoad.CallNewGame();
    }
   

    public void ExitToTitle()
    {
        theAudio.Play("select1");
        gameDeathUI.SetActive(false);
        thetitle.OpenTitleUI();

    }
    public void Slot_Refresh_BeforeOpenUI(int i, string slot_type)  // slot_type(in hieraarchy) -> Load or Save
    {
        GameObject slot = GameObject.Find(slot_type + slot_type + "Slot" + i);
        string filename = "Save_UI_Slot" + i;   // Save_UI_Slot1  -> 파일명
        string path = Application.dataPath + "/" + filename + ".Json";        //세이브 슬롯의 path
        bool fileExists = File.Exists(path);
        if (fileExists)
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);           //세이브 슬롯이 존재한다면 Json파일 내용을 슬롯에 써준다
            slot.GetComponent<TextMeshProUGUI>().text =
                 "Saved Place : " + data.saved_map + "\n Play Time : " + data.playtime_minute + " Min " + data.playtime_seconds + " Sec";
        }
        else
        {
            slot.GetComponent<TextMeshProUGUI>().text = "Save File " + i;
        }
    }
}
