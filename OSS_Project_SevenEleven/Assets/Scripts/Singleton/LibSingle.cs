using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibSingle : MonoBehaviour
{
    static public LibSingle instance; //static으로 선언된 변수의 값을 공유
    // Start is called before the first frame update
    private void Awake()
    {
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
}
