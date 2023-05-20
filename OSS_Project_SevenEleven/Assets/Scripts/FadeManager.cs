using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{

    public SpriteRenderer white;
    public SpriteRenderer black;
    private Color color;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    public void Fadeout(float _speed = 0.02f)
    {
        StartCoroutine(FadeoutCorutine(_speed));
    }
    IEnumerator FadeoutCorutine(float _speed)    //페이드 인 코루틴  speed만큼 알파값(투명도)증가
    {
        color = black.color;

        while(color.a<1f)
        {
            color.a += _speed;
            black.color= color;
            yield return waitTime;
        }

    }

    public void FadeIn(float _speed = 0.02f)     
    {
        StartCoroutine(FadeInCorutine(_speed));
    }
    IEnumerator FadeInCorutine(float _speed)    //페이드 아웃 코루틴  speed만큼 알파값(투명도)증가
    {
        color = black.color;

        while (color.a > 0f)
        {
            color.a -= _speed;
            black.color = color;
            yield return waitTime;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
