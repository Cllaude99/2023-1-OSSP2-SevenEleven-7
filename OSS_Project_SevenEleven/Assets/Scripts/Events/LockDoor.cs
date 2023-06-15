using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour
{
    public GameObject[] lockdoor; //��� �� ���� ������Ʈ

    private AudioManager theAudio;

    private FadeManager theFade;
    private PlayerManager thePlayer;
    public string lockingsound; // ������ �Ҹ�

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
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            thePlayer.islock = true;
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
