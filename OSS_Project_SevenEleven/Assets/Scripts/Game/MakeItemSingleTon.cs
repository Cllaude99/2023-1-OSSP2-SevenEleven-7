using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeItemSingleTon : MonoBehaviour
{
    public static MakeItemSingleTon instance;

    private void Awake() //Start보다 먼저 실행
    {
        //Scene 전환시 객체 파괴 방지 코드

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
    // Start is called before the first frame update

}
