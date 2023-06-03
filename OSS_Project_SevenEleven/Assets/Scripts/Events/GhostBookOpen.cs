using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBookOpen : MonoBehaviour
{



    public OrderManager theOrder;
    public GameObject displayImage;
    private Animator theAnimator;
    public string ghostscream;
    private AudioManager theAudio;


    void Start()
    {
        
        theAudio = FindObjectOfType<AudioManager>();
        theAnimator = GetComponent<Animator>();

    }
    /*public GameObject read_or_exit_UI;
    public bool activated=false;               //UI 활성화비활성화 변수

    public void Resume_UI()
    {
        theOrder.Move();
        read_or_exit_UI.SetActive(false);
        activated = false;
    }

    public void Read_Book()
    {
        theOrder.Move();
        read_or_exit_UI.SetActive(false);
        activated = false;
    }*/                                         //선택지 구현용

    private void ShowItemImage()                            //아이템 상세사진 플로팅 
    {
        theOrder.NotMove();
        displayImage.SetActive(true);
    }

    private void HideItemImage()                            //아이템 상세사진 감춤 
    {
        displayImage.SetActive(false);
        this.gameObject.SetActive(false);
        theOrder.Move();
    }
    private void play_scream()                            //아이템 상세사진 감춤 
    {
        theAudio.Play("ghostscream");

    }
    private bool isPlayerOn = false;                        // 박스 콜라이더와 플레이어가 겹쳐있는지 확인하는 변수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))                 //플레이어가 박스 콜라이더 위에 있으면
            isPlayerOn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))                 //플레이어가 박스 콜라이더 위에 있으면
            isPlayerOn = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isPlayerOn)         //X키 누르고 playeron가 true activated가 false면
            {
                theOrder.NotMove();
                ShowItemImage();
                BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
                boxCollider.enabled = false;                             
                theAnimator.SetBool("Read", true);
                this.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                this.GetComponent<SpriteRenderer>().sortingOrder = 101;
                Invoke("play_scream", 1.4f);
                Invoke("HideItemImage", 3.1f);
            }
        }

    }
}
