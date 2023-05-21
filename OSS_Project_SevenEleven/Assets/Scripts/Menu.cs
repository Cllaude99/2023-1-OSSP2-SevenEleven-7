using System.Collections;
using System.Collections.Generic;
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


    //

    public void Exit()          //게임(애플리케이션) 종료를 위한 함수
    {
        Application.Quit();
    }



    public void Continue()   // 메뉴에서 Resume버튼 누르면 게임으로 재개
    {
        activated= false;
        menu_obj.SetActive(false);
        //theOrder.Move(); 
        theAudio.Play(cancel_sound);
    }


    public void Open_SaveCanvas()   // 세이브 버튼누르면 savecanvas를 띄움
    {
        theAudio.Play(call_sound);
        savecanvas_obj.SetActive(true);
    }

    public void Save_Canvas_Resume()   // 세이브 컨버스에서 Resume버튼 누르면 메뉴 컨버스로
    {
        savecanvas_obj.SetActive(false);
        theAudio.Play(cancel_sound);
    }

    public void Open_Inventory_Canvas()   // 인벤토리 버튼누르면 인벤토리canvas 띄움
    {
        menu_obj.SetActive(false);
        theAudio.Play(call_sound);
        inventory_canvas_obj.SetActive(true);
        
    }

    public void Inventory_Canvas_Resume()   // Inventory 컨버스에서 Resume버튼 누르면 메뉴 컨버스로
    {
        inventory_canvas_obj.SetActive(false);
        theAudio.Play(cancel_sound);
    }

    public void Open_load_Canvas()   // 로드 버튼누르면 이어하기 컨버스 띄움
    {
        theAudio.Play(call_sound);
        load_canvas_obj.SetActive(true);
    }

    public void Load_Canvas_Resume()   // Load 컨버스에서 Resume버튼 누르면 메뉴 컨버스로
    {
        load_canvas_obj.SetActive(false);
        theAudio.Play(cancel_sound);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  //ESC 입력을 받으면 메뉴 화면 ON/OFF
        {
            if(savecanvas_obj.activeSelf==false && inventory_canvas_obj.activeSelf == false
                && load_canvas_obj.activeSelf == false)        //  메뉴 캔버스 만 띄어져있을경우
            {
                activated = !activated;

                if (activated)                  // activated변수가 1이면 메뉴창열기
                {
                    //theOrder.NotMove();       // 메뉴 화면을 누르면 캐릭터들이 멈춤
                    menu_obj.SetActive(true);
                    theAudio.Play(call_sound);
                }
                else
                {
                    menu_obj.SetActive(false);
                    theAudio.Play(cancel_sound);
                    //theOrder.Move();          // 게임을 재개하면 다시 움직임
                }
            }
            else if(savecanvas_obj.activeSelf == true)   //메뉴 캔버스위에 세이브 캔버스가 있을경우 
            {
                savecanvas_obj.SetActive(false);
                theAudio.Play(cancel_sound);
            }
            else if (inventory_canvas_obj.activeSelf == true)   //메뉴 캔버스위에 인벤토리 캔버스가 있을경우 
            {
                inventory_canvas_obj.SetActive(false);
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
