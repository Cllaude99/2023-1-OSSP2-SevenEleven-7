using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private PlayerManager thePlayer; //플레이어가 바라보고 있는 방향
     private Vector2 vector;

    private Quaternion rotation;//회전(각도)을 담당하는 Vector4 xyzw

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        vector.Set(thePlayer.animator.GetFloat("DirX"), thePlayer.animator.GetFloat("DirY"));
    }
}
