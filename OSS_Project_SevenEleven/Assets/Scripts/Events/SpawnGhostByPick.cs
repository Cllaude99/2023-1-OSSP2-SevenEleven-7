using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGhostByPick : MonoBehaviour
{
    public GameObject spawner;
    public GameObject pick;

    public bool isTrue;
    // Update is called once per frame
    void Update()
    {
        if (isTrue)
        {
            if (pick.GetComponent<ItemPickup>().isPick)
            {
                spawner.SetActive(true);
                this.gameObject.SetActive(false);
            }

        }
        else
        {
            if (pick.GetComponent<ItemPickup>().isPick)
            {
                spawner.SetActive(false);
                this.gameObject.SetActive(false);
            }

        }

    }
}
