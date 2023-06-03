using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLAB : MonoBehaviour
{
    private FadeManager theFade;

    public int loopnum;

    public bool iswhite;

    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        theFade = FindObjectOfType<FadeManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Invoke("Flash", delay);
        }

    }

    private void Flash()
    {
        if (iswhite)
        {
            for (int i = 0; i < loopnum; i++)
            {
                theFade.WhiteFlash();
            }
        }
        else
        {
            for (int i = 0; i < loopnum; i++)
            {
                theFade.BlackFlash();
            }
        }
    }
}
