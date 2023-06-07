using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class confirmVisit : MonoBehaviour
{
    public checkVisit theVisit;
    public SpawnKey spawnKey;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            spawnKey.visit++;
            //theVisit.visit.Add(this.gameObject);
            theVisit.confirmvisitnum++;
            this.gameObject.SetActive(false);
        }

    }
}
