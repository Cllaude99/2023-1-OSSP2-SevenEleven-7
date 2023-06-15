using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    private Animator animator;
    public AudioManager theAudio;
    public string event_sound;
    bool sound_activated;
    public bool is_event_activated=false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("Enter", true);
        if (!sound_activated)
        {
            theAudio.Play(event_sound);
            sound_activated = true;
            is_event_activated=true;
        }
    }


    public void ResetEventBool()
    {
            animator.SetBool("Enter", false);
            sound_activated = false;

    }

    public void UpdateEventBool()
    {
        if (is_event_activated)
            animator.SetBool("Enter", true);
        else
            animator.SetBool("Enter", false);

    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }



}
