using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    private Animator animator;
    public AudioManager theAudio;
    public string event_sound;
    bool sound_activated;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("Enter", true);
        if (!sound_activated)
        {
            theAudio.Play(event_sound);
            sound_activated = true;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }



}
