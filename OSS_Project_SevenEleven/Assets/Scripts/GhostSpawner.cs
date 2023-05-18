using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostSpawner : MonoBehaviour
{
    public GameObject GhostPrefab;
    public GameObject ghostSpawner;
    public Transform SpawnPoint;
    public GameObject instance;

    private PlayerManager thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 spawnPoint = SpawnPoint.position;
        GameObject instance = Instantiate(GhostPrefab, spawnPoint, Quaternion.identity);
        instance.GetComponent<GhostManager>().target = thePlayer.transform;
        ghostSpawner.SetActive(false);
    }
}
