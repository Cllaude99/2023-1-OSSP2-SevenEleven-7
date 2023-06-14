using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue;

    private OrderManager theOrder;
    private DialogueManager theDM;
    public bool hasEntered = false;

    
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!hasEntered && collision.gameObject.name=="Player")
        {
            theDM.ShowDialogue(dialogue);
            hasEntered = true;
        }
    }


}
