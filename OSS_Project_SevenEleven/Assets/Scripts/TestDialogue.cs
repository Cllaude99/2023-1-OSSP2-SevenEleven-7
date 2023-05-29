using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue;

    private DialogueManager theDM;
    private bool hasEntered = false;

    public bool isFlash;
    private FadeManager theFade; 
    
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theFade = FindObjectOfType<FadeManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasEntered && collision.gameObject.name=="Player")
        {
            theDM.ShowDialogue(dialogue);
            hasEntered = true;
            if (isFlash)  theFade.Flash();
        }
    }


}
