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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            for (int i = 0; i < loopnum; i++)
            {
                theFade.Flash();
            }

        }

    }
}
