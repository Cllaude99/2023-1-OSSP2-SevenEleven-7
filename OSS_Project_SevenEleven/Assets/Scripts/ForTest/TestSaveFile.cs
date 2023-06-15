using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveFile : MonoBehaviour
{


    //For Save N Load...
    public Vector3 PlayerPos;
    public Vector3 CameraPos;
    public Vector3 NPCPos;
    public BoxCollider2D currentBound;
    
    public List<int> confirmVisit;
    public List<int> confirmKeySpawn;
    public List<bool> GhostSpawn;
    public List<bool> ObjectActive;
    public List<bool> isTextEnter;
    

    public List<int> playerItemInventory;//
    public List<int> playerItemInventoryCount;//

}