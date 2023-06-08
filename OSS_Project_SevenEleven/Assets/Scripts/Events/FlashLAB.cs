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

    public bool isCoroutineStart = false;

    public bool canFlash = false;

    // Start is called before the first frame update
    void Start()
    {
        theFade = FindObjectOfType<FadeManager>();
    }

    private void Update()
    {
        if (isCoroutineStart)
        {
            isCoroutineStart = false;
            StartCoroutine(FlashCoroutine());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (!canFlash)
            {
                canFlash = true;
                isCoroutineStart = true;
            }

        }

    }

    IEnumerator FlashCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < loopnum; i++)
            {
                theFade.BlackFlash();
            }
            yield return new WaitForSeconds(delay);
        }

    }

}
