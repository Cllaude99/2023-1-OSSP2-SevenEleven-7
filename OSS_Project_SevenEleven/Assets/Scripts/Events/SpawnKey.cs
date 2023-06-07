using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour
{
    public GameObject[] onObject; //나타날 키

    public int visit; //방문한 곳 저장할 변수

    public int visitnum; //방문해야할 방의 수

    // Update is called once per frame
    void Update()
    {
        if (visit == visitnum)
        {
            for (int i = 0; i < onObject.Length; i++)
            {
                onObject[i].SetActive(true);
            }
            this.gameObject.SetActive(false);
        }
    }
}
