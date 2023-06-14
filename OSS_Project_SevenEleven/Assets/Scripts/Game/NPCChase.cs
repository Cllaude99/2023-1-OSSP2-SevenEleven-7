using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChase : MonoBehaviour
{
    private NPCManager theNPC;

    // Start is called before the first frame update
    void Start()
    {
        theNPC = FindObjectOfType<NPCManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            theNPC.ischase = true;
            theNPC.npc.NPCmove = true;

        }
    }

}
