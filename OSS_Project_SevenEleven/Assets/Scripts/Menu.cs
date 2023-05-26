using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public static Menu instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public GameObject menu_obj;
    public AudioManager theAudio;

    public string call_sound;
    public string cancel_sound;

    public OrderManager theOrder;

    private bool activated;               //메뉴창 활성화비활성화 변수


    // 다른 캔버스 UI 연결용 변수

    public GameObject savecanvas_obj;
    public GameObject inventory_canvas_obj;
    public GameObject load_canvas_obj;

    public void Exit()          //게임(애플리케이션) 종료를 위한 함수
    {
        Application.Quit();
    }



    public void Continue()   // 메뉴에서 Resume버튼 누르면 게임으로 재개
    {
        activated= false;
        menu_obj.SetActive(false);
        theOrder.Move(); 
        theAudio.Play(cancel_sound);
    }


    public void Open_SaveCanvas()   // 세이브 버튼누르면 savecanvas를 띄움
    {
        theAudio.Play(call_sound);
        savecanvas_obj.SetActive(true);
        Slot_Refresh_BeforeOpenUI(1, "Save");
        Slot_Refresh_BeforeOpenUI(2, "Save");
        Slot_Refresh_BeforeOpenUI(3, "Save");

    }

    public void Save_Canvas_Resume()   // 세이브 컨버스에서 Resume버튼 누르면 메뉴 컨버스로
    {
        savecanvas_obj.SetActive(false);
        theAudio.Play(cancel_sound);
    }



    public class SaveData               //세이브 슬롯 UI용 json데이터
    {
        public string saved_map;
        public int playtime_minute;
        public int playtime_seconds;

    }



    public void SaveSlot_1_Modify()
    {
        SaveSlotModify(1);
    }
    public void SaveSlot_2_Modify()
    {
        SaveSlotModify(2);
    }
    public void SaveSlot_3_Modify()
    {
        SaveSlotModify(3);
    }

    public void SaveSlotModify(int i)     //세이브버튼1,2,3 를 누르면 각 세이브 슬롯에 현재 캐릭터의 위치 , 플레이 타임을 text로 대체
    {
        GameObject save_slot_1 = GameObject.Find("SaveSlot"+i);
        PlayerManager thePlayer = FindObjectOfType<PlayerManager>();

        double playtime = Math.Round(Time.time);
        int playtime_minute=(int)(playtime/60);
        int playtime_seconds = (int)(playtime % 60); //플레이타임 초를 분,초로 

        save_slot_1.GetComponent<TextMeshProUGUI>().text = 
            "Saved Place : "+thePlayer.currentMapName +"\n Play Time : "+ playtime_minute+" Min " + playtime_seconds+" Sec";



        SaveData data = new SaveData();
        data.saved_map = thePlayer.currentMapName;
        data.playtime_minute = playtime_minute;
        data.playtime_seconds= playtime_seconds;
        string json=JsonUtility.ToJson(data);             // 현재 플레이어의 정보를 data화                      

        string filename = "Save_UI_Slot" + i;   // Save_UI_Slot1,2,3  -> 파일명
        string path = Application.dataPath + "/" + filename + ".Json";          //세이브 데이터 json화 시켜서 저장
        File.WriteAllText(path, json);

    }


    public void Open_load_Canvas()   // 로드 버튼누르면 이어하기 컨버스 띄움
    {
        theAudio.Play(call_sound);
        load_canvas_obj.SetActive(true);
        Slot_Refresh_BeforeOpenUI(1, "Load");
        Slot_Refresh_BeforeOpenUI(2, "Load");
        Slot_Refresh_BeforeOpenUI(3, "Load");       //이어하기 캔버스의 슬롯을 Refresh

    }

    public void Load_Canvas_Resume()   // Load 컨버스에서 Resume버튼 누르면 메뉴 컨버스로
    {
        load_canvas_obj.SetActive(false);
        theAudio.Play(cancel_sound);
    }

    public void Slot_Refresh_BeforeOpenUI(int i,string slot_type)  // slot_type(in hieraarchy) -> Load or Save
    {
        GameObject slot = GameObject.Find(slot_type+"Slot" + i);
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

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  //ESC 입력을 받으면 메뉴 화면 ON/OFF
        {
            if(savecanvas_obj.activeSelf==false && load_canvas_obj.activeSelf == false)        //  메뉴 캔버스 만 띄어져있을경우
            {
                activated = !activated;

                if (activated)                  // activated변수가 1이면 메뉴창열기
                {
                    theOrder.NotMove();       // 메뉴 화면을 누르면 캐릭터들이 멈춤
                    menu_obj.SetActive(true);
                    theAudio.Play(call_sound);
                }
                else
                {
                    menu_obj.SetActive(false);
                    theAudio.Play(cancel_sound);
                    theOrder.Move();          // 게임을 재개하면 다시 움직임
                }
            }
            else if(savecanvas_obj.activeSelf == true)   //메뉴 캔버스위에 세이브 캔버스가 있을경우 
            {
                savecanvas_obj.SetActive(false);
                theAudio.Play(cancel_sound);
            }

            else if (load_canvas_obj.activeSelf == true)   //메뉴 캔버스위에 load 캔버스가 있을경우 
            {
                load_canvas_obj.SetActive(false);
                theAudio.Play(cancel_sound);
            }

        }

    }
}
