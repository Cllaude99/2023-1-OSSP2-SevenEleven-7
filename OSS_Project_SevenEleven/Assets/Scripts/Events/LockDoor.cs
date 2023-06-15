using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour
{
    public GameObject[] lockdoor; //잠글 문 게임 오브젝트

    private AudioManager theAudio;

    private FadeManager theFade;

    public string lockingsound; // 문잠기는 소리

    BGMManager BGM;

    public int timersound;

    private DeathCount theCount;
    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        theFade = FindObjectOfType<FadeManager>();
        BGM = FindObjectOfType<BGMManager>();
        theCount = FindObjectOfType<DeathCount>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            for (int i = 0; i< lockdoor.Length; i++)
            {
                lockdoor[i].SetActive(true);
            }
            theAudio.Play(lockingsound);
            BGM.Play(timersound);
            theCount.isCount = true;
            this.gameObject.SetActive(false);
        }
    }
}
