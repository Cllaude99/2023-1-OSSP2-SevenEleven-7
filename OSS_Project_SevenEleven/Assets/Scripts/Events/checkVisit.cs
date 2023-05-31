using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkVisit : MonoBehaviour
{
    public GameObject[] barricade; // 열 곳

    public List<GameObject> visit; //방문한 곳 저장할 배열

    public int visitnum; //방문해야할 방의 수

    // Update is called once per frame
    void Update()
    {
        if(visit.Count == visitnum)
        {
            for (int i = 0; i < barricade.Length; i++)
            {
                barricade[i].SetActive(false);
            }
        }
    }
}
