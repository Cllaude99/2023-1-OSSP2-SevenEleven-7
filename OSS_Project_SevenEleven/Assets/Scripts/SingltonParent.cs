using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingltonParent : MonoBehaviour
{
    private Singleton singletonInstance;
    // Start is called before the first frame update
    void Start()
    {
        singletonInstance = Singleton.instance;
    }

}
