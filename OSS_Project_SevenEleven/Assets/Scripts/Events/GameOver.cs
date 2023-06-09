using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameOverButton theDeathUI;

    BGMManager BGM;
    public string gameOver;
    // Start is called before the first frame update
    void Start()
    {
        BGM = FindObjectOfType<BGMManager>();
        theDeathUI = FindObjectOfType<GameOverButton>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            BGM.Stop();
            //SceneManager.LoadScene(gameOver); // transferMapName으로 이동
            theDeathUI.OpenDeathUI();

        }

    }
}
