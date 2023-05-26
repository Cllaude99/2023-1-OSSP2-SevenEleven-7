using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    private Camera theCamera;

    public AudioManager theAudio;
    private FadeManager theFade;
    public string crash_sound;


    [SerializeField]  float shake_strength = 20f;      
    [SerializeField]  float shake_duration = 2f;        //화면흔들림강도,지속시간 유니티내에서 수정가능하게 serialfield
    [SerializeField]  float fall_second = 2f;
    private void OnTriggerEnter2D(Collider2D collision)                                
    {
        theCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        theCamera.GetComponent<CameraManager>().enabled = false;            //카메라매니저가 계속 카메라 위치수정해서 off
        
        onShakeCamera(shake_strength, shake_duration);
                                                                             
        //theCamera.GetComponent<CameraManager>().enabled = true;             //테스팅용 카메라 다시돌아오게
    }

    public void onShakeCamera(float shake_strength, float shake_duraiton)
    {
        this.shake_duration = shake_duraiton;
        this.shake_strength= shake_strength;
        StartCoroutine("delay");                        
    }

    private IEnumerator delay()
    {
        yield return StartCoroutine("ShakeByPosition");
        yield return StartCoroutine("Elevator_Down");               //코루틴 순차호출

    }

    private IEnumerator ShakeByPosition()                                   //카메라 흔들림
    {
        
        Vector3 startPosition= theCamera.transform.position;

       
        while(shake_duration>0.0f)
        {

            theCamera.transform.position= startPosition + Random.insideUnitSphere*shake_strength;   //반지름이 1인구의 랜덤xyz * 흔들림강도만큼 위치 변화

            shake_duration -= Time.deltaTime;     

            yield return null;

        }
        theCamera.transform.position= startPosition;
    }


    private IEnumerator Elevator_Down()                                 //카메라위로이동(엘레베이터추락)
    {
        Vector3 startPosition = theCamera.transform.position;
        Vector3 endPosition = theCamera.transform.position+ Vector3.up * 450;           //카메라 목표위치 y축+300     


        while (fall_second > 0.0f)
        {

            theCamera.transform.position = Vector3.MoveTowards(theCamera.transform.position, endPosition, 5f);      //5f속도만큼 목표위치로 카메라이동

            fall_second -= Time.deltaTime;

            yield return null;

        }
        if (fall_second < 0)
        {
            theFade = FindObjectOfType<FadeManager>();
            theFade.Fadeout();
        }
    }
}
