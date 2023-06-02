using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    BGMManager BGM;
    public int PlayMusicTrack;

    public bool isPlay = true;

    public bool isKeeping = false;
    // Start is called before the first frame update
    void Start()
    {
        BGM = FindObjectOfType<BGMManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlay)
        {
            BGM.Play(PlayMusicTrack);
            BGM.Loop();
        }
        else
        {
            BGM.UnLoop();
            BGM.Stop();
        }

        if (!isKeeping)this.gameObject.SetActive(false);
    }
}
