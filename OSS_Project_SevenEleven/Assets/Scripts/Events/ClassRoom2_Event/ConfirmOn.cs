using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmOn : MonoBehaviour
{

    public checkVisit checkV;
    public string gameOver;
    private AudioManager theAudio;
    BGMManager BGM;
    public string beep;
    public bool correct = false;
    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        BGM = FindObjectOfType<BGMManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Z))
        {
            theAudio.Play(beep);
            if (correct)
            {
                checkV.visit.Add(this.gameObject);
                this.gameObject.SetActive(false);
            }
            else
            {
                BGM.Stop();
                SceneManager.LoadScene(gameOver); // transferMapName으로 이동
            }
        }

    }
}
