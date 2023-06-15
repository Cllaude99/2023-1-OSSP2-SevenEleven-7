using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerBoothEvent : MonoBehaviour
{
    public GameObject theGhost;

    private AudioManager theAudio;
    public string screamSound;

    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            theAudio.Play(screamSound);
            theGhost.GetComponent<NPCManager>().npc.NPCmove = true;
            Invoke("removeObject", 1.5f);
            this.gameObject.SetActive(false);
        }
    }

    private void removeObject()
    {
        theGhost.GetComponent<NPCManager>().SetNotMove();
        theGhost.SetActive(false);
    }
}
