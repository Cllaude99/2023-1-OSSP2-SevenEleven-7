using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerBoothEvent : MonoBehaviour
{
    public GameObject theGhost;

    private AudioManager theAudio;
    public string screamSound;

    private OrderManager theOrder;

    public bool isEnter = true;

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
        if(collision.gameObject.name == "Player" && isEnter)
        {
            isEnter = false;
            StartCoroutine(RemoveObj());

        }
    }

    IEnumerator RemoveObj()
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
        yield return new WaitForSeconds(3f); 
        theGhost.SetActive(false);
        this.gameObject.SetActive(false);
    }

}
