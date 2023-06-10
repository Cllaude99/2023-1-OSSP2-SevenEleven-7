using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStory : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;
    private FadeManager theFade; // FadeManager 참조를 위한 추가 변수

    private bool flag = false;

    //Use this for initialization
    void Start()
    {
        theFade = FindObjectOfType<FadeManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theOrder.PreLoadCharacter();

        theOrder.NotMove();
        theOrder.Turn("Player", "UP");
        theOrder.Turn("FriendNPC", "UP");
        theOrder.Move("Player", "UP");
        theOrder.Move("FriendNPC", "UP");
        theOrder.Turn("Player", "Right");
        theOrder.Turn("FriendNPC", "Left");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag && collision.CompareTag("Player"))
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }


    IEnumerator EventCoroutine()
    {
        theOrder.NotMove();
        for (int i=0; i<2; i++)
        {
            theOrder.Move("Player", "UP");
            theOrder.Move("FriendNPC", "UP");
        }
        theOrder.Turn("Player", "RIGHT");
        theOrder.Turn("FriendNPC", "LEFT");
        yield return new WaitForSeconds(0.8f);

        theOrder.Turn("Player", "RIGHT");
        theOrder.Turn("FriendNPC", "LEFT");

        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);
        theOrder.NotMove();
        theOrder.Turn("Player", "DOWN");

        for (int i = 0; i < 5; i++)
        {
            theOrder.Move("FriendNPC", "DOWN");
        }

        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);
        theOrder.NotMove();
        for (int i = 0; i < 7; i++)
        {
            theOrder.Move("Player", "RIGHT");
        }
        for (int i = 4; i < 7; i++)
        {
            theOrder.Move("Player", "UP");
        }
        theOrder.Turn("Player", "RIGHT");
        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theFade.Fadeout();
        yield return new WaitForSeconds(3f);
        theOrder.Turn("Player", "DOWN");
        theFade.FadeIn();
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(0.8f);
        theOrder.Turn("Player", "UP");
        yield return new WaitForSeconds(0.8f);
        theOrder.Turn("Player", "RIGHT");
        yield return new WaitForSeconds(0.8f);
        theOrder.Turn("Player", "LEFT");
        yield return new WaitForSeconds(0.8f);
        theOrder.Turn("Player", "DOWN");
        yield return new WaitForSeconds(0.8f);
        theDM.ShowDialogue(dialogue_3);
        yield return new WaitUntil(() => !theDM.talking);
        theOrder.Move();

    }
}
