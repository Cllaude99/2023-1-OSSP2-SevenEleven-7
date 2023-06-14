using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Bound[] bounds;
    private PlayerManager thePlayer;
    private CameraManager theCamera;

    public GameObject item_object_should_destroyed;
    public bool isFirstGameStart = true;

    public void LoadStart()
    {
        StartCoroutine(LoadWaitCoroutine());
    }
    public void BoundStart()
    {
        StartCoroutine(BoundCoroutine());
    }

    IEnumerator LoadWaitCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        thePlayer = FindObjectOfType<PlayerManager>();
        bounds = FindObjectsOfType<Bound>();
        theCamera = FindObjectOfType<CameraManager>();

        theCamera.target = GameObject.Find("Player");

        /*
         캐릭터가 있던 맵의 BoxCollider를 카메라 바운드로 지정하기 위해서, currentMapName과 BoundName을 조건 비교한뒤
         카메라 바운드로 설정해줌.
        */
        for (int i = 0; i < bounds.Length; i++)
        {
            if (bounds[i].boundName == thePlayer.currentMapName)
            {
                bounds[i].SetBound();
                break;
            }
        }
    }


    IEnumerator BoundCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        thePlayer = FindObjectOfType<PlayerManager>();
        bounds = FindObjectsOfType<Bound>();
        theCamera = FindObjectOfType<CameraManager>();

        theCamera.target = GameObject.Find("Player");
        

        /*
         캐릭터가 있던 맵의 BoxCollider를 카메라 바운드로 지정하기 위해서, currentMapName과 BoundName을 조건 비교한뒤
         카메라 바운드로 설정해줌.
        */
        for (int i = 0; i < bounds.Length; i++)
        {
            if (bounds[i].boundName == thePlayer.currentMapName)
            {
                bounds[i].SetBound();
                break;
            }
        }
    }
}
