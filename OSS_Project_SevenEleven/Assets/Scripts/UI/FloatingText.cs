using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{

    public float moveSpeed; // 텍스트 떠다니는 속도
    public float destroyTime; // 텍스트 일정시간후에 사라지도록

    public Text text;

    private Vector3 vector;



    // Update is called once per frame
    void Update()
    {
        vector.Set(text.transform.position.x, text.transform.position.y + (moveSpeed * Time.deltaTime), text.transform.position.z);
        text.transform.position = vector;

        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0)
            Destroy(this.gameObject);
    }
}
