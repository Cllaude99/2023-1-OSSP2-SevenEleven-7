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

    BGMManager BGM;
    public int EndingSound;
    void Start()
    {
        theCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        theOrder = FindObjectOfType<OrderManager>();
        thetitle = FindObjectOfType<TitleButton>();
        theFade = FindObjectOfType<FadeManager>();
        BGM = FindObjectOfType<BGMManager>();

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
        BGM.Stop();
        theFade.FadeIn();
        theCamera.GetComponent<CameraManager>().enabled = true;            //카메라 쉐이크에서꺼진거 다시  true
        thetitle.OpenTitleUI();
        go.SetActive(false);

    }
}