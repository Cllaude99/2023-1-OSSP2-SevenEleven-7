using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashLAB : MonoBehaviour
{
    private FadeManager theFade;

    public int loopnum;

    public bool iswhite;

    public float delay;

    private bool isStart = false;

    private bool isCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        theFade = FindObjectOfType<FadeManager>();
    }

    /*private void Update()
    {
        if (isCollision)
        {
            isStart = false;
            StartCoroutine(FlashCoroutine());
        }
    }*/

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(FlashCoroutine());
        }

    }

    IEnumerator FlashCoroutine()
    {

        for (int i = 0; i < loopnum; i++)
        {
            theFade.BlackFlash();
        }
        yield return new WaitForSeconds(delay);
        //isStart = true;
    }

}
