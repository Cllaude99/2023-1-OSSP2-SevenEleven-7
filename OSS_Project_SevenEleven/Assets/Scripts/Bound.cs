using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    // 씬 이동시 바운드를 넘겨주기 위한 스크립트

    //Variables

    //Private
    private BoxCollider2D bound;
    public string boundName; // 카메라의 바운드로 설정해주기 위한 변수.
    private CameraManager theCamera;

    // Start is called before the first frame update
    void Start()
    {
        bound = GetComponent<BoxCollider2D>();
        theCamera = FindObjectOfType<CameraManager>();
        theCamera.SetBound(bound);

        GameManager theGM = FindObjectOfType<GameManager>();
        theGM.LoadStart();
    }

    public void SetBound() // 불러오기에서 사용되는 함수
    {
        if(theCamera != null)
        {
            theCamera.SetBound(bound);
        }
    }
}
