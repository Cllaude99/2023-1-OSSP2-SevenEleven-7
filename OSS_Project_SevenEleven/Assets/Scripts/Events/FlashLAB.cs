using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLAB : MonoBehaviour
{
    private FadeManager theFade;
    private PlayerManager thePlayer;

    public int loopnum;



    // Start is called before the first frame update
    void Start()
    {
        theFade = FindObjectOfType<FadeManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        thePlayer.notMove = true;
        for (int i = 0; i < loopnum; i++)
        {
            theFade.Flash();
        }
        thePlayer.notMove = false;
    }
}
