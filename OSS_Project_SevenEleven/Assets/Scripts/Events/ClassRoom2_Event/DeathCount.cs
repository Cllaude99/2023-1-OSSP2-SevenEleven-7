using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCount : MonoBehaviour
{
    public bool isCount = false;
    public string gameOver;
    BGMManager BGM;
    // Start is called before the first frame update
    void Start()
    {
        BGM = FindObjectOfType<BGMManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCount)
        {
            isCount = false;
            CountCoroutine();
        }
    }

    IEnumerator CountCoroutine()
    {
        yield return new WaitForSeconds(180f);
        BGM.Stop();
        SceneManager.LoadScene(gameOver); // transferMapName으로 이동
    }


}
