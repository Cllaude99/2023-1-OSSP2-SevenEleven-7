using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene 관리를 위한 라이브러리

public class TransferMap : MonoBehaviour
{

    //Build Settings에서 사용하기 전에 Scene Load 할 것!!


    //Variables

    //Public 
    public Transform target;
    public string transferMapName;
    public BoxCollider2D targetBound;
    //Private
    private PlayerManager thePlayer;
    private CameraManager theCamera;
    private FadeManager theFade;
    private OrderManager theOrder;


    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
        theCamera = FindObjectOfType<CameraManager>();
        theFade = FindObjectOfType<FadeManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(TransferCoroutine());
        }
    }

    IEnumerator TransferCoroutine()
    {
        theOrder.NotMove();
        theFade.Fadeout();

        yield return new WaitForSeconds(1f);
        if (!thePlayer.istransfer) thePlayer.istransfer = true;
        thePlayer.current_transfer = this.gameObject;

        thePlayer.currentMapName = transferMapName;

        theCamera.SetBound(targetBound);
        theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
        thePlayer.transform.position = target.transform.position;

        theFade.FadeIn();
        yield return new WaitForSeconds(0.5f);
        theOrder.Move();
    }
}
