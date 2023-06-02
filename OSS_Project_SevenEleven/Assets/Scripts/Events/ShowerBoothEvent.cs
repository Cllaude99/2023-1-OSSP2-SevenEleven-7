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
        theAudio.Play(screamSound);
        theGhost.GetComponent<NPCManager>().npc.NPCmove = true;
        //theGhost.GetComponent<NPCManager>().npc.NPCmove = false;
        Invoke("removeObject", 3f);
        this.gameObject.SetActive(false);
    }

    private void removeObject()
    {
        Destroy(theGhost);
    }
}
