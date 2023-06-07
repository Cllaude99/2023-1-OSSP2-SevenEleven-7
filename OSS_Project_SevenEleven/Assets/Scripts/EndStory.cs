using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndStory : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3; 

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;
    private FadeManager theFade; // FadeManager 참조를 위한 추가 변수
    private Animator theAnimator; 
    private AudioManager theAudioManager;
    private bool flag = false;

    //Use this for initialization
    void Start()
    {
        theFade = FindObjectOfType<FadeManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theAnimator=FindObjectOfType<Animator>();
        theAudioManager = FindObjectOfType<AudioManager>();
        theOrder.PreLoadCharacter();
    }
  
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        
        theOrder.NotMove();
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);
        theFade.FadeIn();
        yield return new WaitForSeconds(1f);
        
        theFade.Fadeout();
        yield return new WaitForSeconds(1f);

        theDM.ShowDialogue(dialogue_2);
        theAnimator.SetBool("Activate", true);
        yield return new WaitForSeconds(0.5f);
        theAudioManager.Play("ghostscream");
        yield return new WaitUntil(() => !theDM.talking);
        theFade.FadeIn();
        yield return new WaitForSeconds(1f);
      
        theDM.ShowDialogue(dialogue_3);
        yield return new WaitUntil(() => !theDM.talking);

    }
}
