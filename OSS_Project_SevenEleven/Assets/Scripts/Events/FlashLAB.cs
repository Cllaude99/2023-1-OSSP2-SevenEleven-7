using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLAB : MonoBehaviour
{
    private FadeManager theFade;

    public int loopnum;

    public bool iswhite;


    // Start is called before the first frame update
    void Start()
    {
        theFade = FindObjectOfType<FadeManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
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
            //this.gameObject.SetActive(false);
        }

    }
}
