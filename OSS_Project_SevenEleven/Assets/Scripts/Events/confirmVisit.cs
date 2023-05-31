using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class confirmVisit : MonoBehaviour
{
    public checkVisit theVisit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        theVisit.visit.Add(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
