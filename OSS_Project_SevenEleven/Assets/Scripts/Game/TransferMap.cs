using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene ������ ���� ���̺귯��

public class TransferMap : MonoBehaviour
{

    //Build Settings���� ����ϱ� ���� Scene Load �� ��!!


    //Variables

    //Public 
    public Transform target;
    public string transferMapName;
    public BoxCollider2D targetBound;
    public Transform NPCtarget;
    public GameObject PlaceTag;
    public NPCManager theNPC;
    //Private
    private PlayerManager thePlayer;
    private CameraManager theCamera;
    private FadeManager theFade;
    private OrderManager theOrder;
    private AudioManager theAudio;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
        theCamera = FindObjectOfType<CameraManager>();
        theFade = FindObjectOfType<FadeManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theAudio = FindObjectOfType<AudioManager>();
        theNPC = FindObjectOfType<NPCManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "FriendNPC")
        {
            GameObject.Find("FriendNPC").transform.position = NPCtarget.position;
        }
        if (collision.gameObject.name == "Player")
        {
            theAudio.Play("transfer_sound");
           StartCoroutine(TransferCoroutine());
            if(theNPC.ischase)
            {
                GameObject.Find("FriendNPC").transform.position = NPCtarget.position;
            }
        }
        
    }

    IEnumerator TransferCoroutine()
    {
        thePlayer.ghostNotMove = true;
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
        if (PlaceTag != null)
        {
            PlaceTag.SetActive(false);
            PlaceTag.SetActive(true);
        }
        theOrder.Move();
        thePlayer.ghostNotMove = false;
    }
}
