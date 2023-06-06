using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Singleton>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

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
