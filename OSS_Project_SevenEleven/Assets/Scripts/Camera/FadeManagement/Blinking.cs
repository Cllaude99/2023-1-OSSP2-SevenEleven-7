using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    public float blinkDuration = 1f; // 반짝이는 주기를 결정하는 변수 (초 단위)
    public float minTransparency = 0.3f; //최소 투명도(0부터 1 사이의 값)
    public float maxTransparency = 1f; // 최대 투명도 (0부터 1 사이의 값)

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

// Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        StartCoroutine(BlinkingCoroutine());
    }

    IEnumerator BlinkingCoroutine()
    {
        while (true)
        {
            // 반짝이는 과정에서 최대 투명도까지 이동
            for (float t = 0; t < 1; t += Time.deltaTime / blinkDuration)
            {
                Color newColor = originalColor;
                newColor.a = Mathf.Lerp(minTransparency, maxTransparency, t);
                spriteRenderer.color = newColor;
                yield return null;
            }

            // 반짝이는 과정에서 최소 투명도까지 이동
            for (float t = 0; t < 1; t += Time.deltaTime / blinkDuration)
            {
                Color newColor = originalColor;
                newColor.a = Mathf.Lerp(maxTransparency, minTransparency, t);
                spriteRenderer.color = newColor;
                yield return null;
            }
        }
    }
}
