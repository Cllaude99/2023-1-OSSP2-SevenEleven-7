using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveFile : MonoBehaviour
{


    //For Save N Load...
    public Vector3 PlayerPos;
    public Vector3 CameraPos;
    public BoxCollider2D currentBound;
    
    public List<int> confirmVisit;
    public List<int> confirmKeySpawn;
    public List<bool> confirmPickforSpawn;
    

    public List<int> playerItemInventory;//
    public List<int> playerItemInventoryCount;//
}