using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkVisit : MonoBehaviour
{
    
    public GameObject[] barricade; // 열 곳


    public int visitnum; //방문해야할 방의 수

    public int confirmvisitnum = 0;

    public bool isSound = false;

    public string openSound;

    private AudioManager theAudio;
    private PlayerManager thePlayer;

    BGMManager bgm;

    
    private void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        bgm = FindObjectOfType<BGMManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(confirmvisitnum == visitnum)
        {
            thePlayer.islock = false;
            if (isSound)
            {
                theAudio.Play(openSound);
                bgm.Stop();
            }
            for (int i = 0; i < barricade.Length; i++)
            {
                barricade[i].SetActive(false);
            }
            this.gameObject.SetActive(false);
        }
    }
}
