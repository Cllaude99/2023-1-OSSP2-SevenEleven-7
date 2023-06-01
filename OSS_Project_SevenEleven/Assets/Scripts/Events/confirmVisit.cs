using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class confirmVisit : MonoBehaviour
{
    public checkVisit theVisit;
    public SpawnKey spawnKey;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spawnKey.visit.Add(this.gameObject);
        theVisit.visit.Add(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
