using System.Collections;
using System.Collections.Generic;
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

    public GameObject go;
    public AudioManager theAudio;

    public string call_sound;
    public string cancel_sound;

    public OrderManager theOrder;

    private bool activated;               //메뉴창 활성화비활성화 변수


    public void Exit()          //게임(애플리케이션) 종료를 위한 함수
    {
        Application.Quit();
    }



    public void Continue()   // 메뉴에서 Resume버튼 누르면 게임으로 재개
    {
        activated= false;
        go.SetActive(false);
        //theOrder.Move(); 
        theAudio.Play(cancel_sound);
    }




    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  //ESC 입력을 받으면 메뉴 화면 ON/OFF
        {
            activated = !activated;

            if (activated)
            {
                //theOrder.NotMove();       // 메뉴 화면을 누르면 캐릭터들이 멈춤
                go.SetActive(true);
                theAudio.Play(call_sound);
            }
            else
            {
                go.SetActive(false);
                theAudio.Play(cancel_sound);
                //theOrder.Move();          // 게임을 재개하면 다시 움직임
            }
        }

    }
}
