using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public GameObject go;
    private OrderManager theOrder;

    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();

    }
    public void OnTriggerStay2D(Collider2D collision)
    {
            theOrder.NotMove();
            go.SetActive(true);
 
    }
}