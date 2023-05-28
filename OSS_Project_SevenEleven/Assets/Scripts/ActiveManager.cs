using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveManager : MonoBehaviour
{

    public GameObject[] objects;
    public bool activeSet;
    
    public void SetActiveT()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(true);
        }
    }

    public void SetActiveF()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (activeSet) SetActiveT();
            else SetActiveF();
        }
    }
}
