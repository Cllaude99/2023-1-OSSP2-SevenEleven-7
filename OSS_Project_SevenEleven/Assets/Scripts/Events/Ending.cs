using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public GameObject go;
    private OrderManager theOrder;
    private Camera theCamera;
    public TitleButton thetitle;
    private FadeManager theFade;
    public NPCManager theNPC;

    BGMManager BGM;
    public int EndingSound;
    void Start()
    {
        theCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        theOrder = FindObjectOfType<OrderManager>();
        thetitle = FindObjectOfType<TitleButton>();
        theFade = FindObjectOfType<FadeManager>();
        BGM = FindObjectOfType<BGMManager>();
        theNPC = FindObjectOfType<NPCManager>();

    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        theOrder.NotMove();
        go.SetActive(true);
        BGM.Play(EndingSound);
        Invoke("OpenTitleAfterEnding", 20f);
    }

    public void OpenTitleAfterEnding()
    {
        theNPC.ischase = false;
        BGM.Stop();
        theFade.FadeIn();
        theCamera.GetComponent<CameraManager>().enabled = true;            //ī�޶� ����ũ���������� �ٽ�  true
        thetitle.OpenTitleUI();
        go.SetActive(false);

    }
}