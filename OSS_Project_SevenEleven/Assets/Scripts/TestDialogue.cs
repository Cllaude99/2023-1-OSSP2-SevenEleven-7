using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue;

    private OrderManager theOrder;
    private DialogueManager theDM;
    private bool hasEntered = false;

    
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasEntered && collision.gameObject.name=="Player")
        {
            theOrder.NotMove();
            theDM.ShowDialogue(dialogue);
            hasEntered = true;
        }
    }


}
