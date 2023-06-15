using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerBoothEvent : MonoBehaviour
{
    public GameObject theGhost;

    private AudioManager theAudio;
    public string screamSound;

    private OrderManager theOrder;

    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            theAudio.Play(screamSound);
            theOrder.Move("ShowerGhost", "LEFT");
            theOrder.Move("ShowerGhost", "LEFT");
            theOrder.Move("ShowerGhost", "LEFT");
            theOrder.Move("ShowerGhost", "LEFT");
            theOrder.Move("ShowerGhost", "LEFT");
            theOrder.Move("ShowerGhost", "LEFT");
            theOrder.Move("ShowerGhost", "LEFT");
            theOrder.Move("ShowerGhost", "LEFT");
            theOrder.Move("ShowerGhost", "LEFT");
            theOrder.Move("ShowerGhost", "LEFT");
            theOrder.Move("ShowerGhost", "LEFT");
            theOrder.Move("ShowerGhost", "LEFT");
            StartCoroutine(RemoveObj());
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator RemoveObj()
    {
        yield return new WaitForSeconds(3f);
        theGhost.SetActive(false);
    }

}
