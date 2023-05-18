using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MovingObject
{
    public Transform target; // 추격 대상(플레이어)
    public float chaseSpeed; // 추격 속도

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(GhostCoroutine());
    }

    private void Update()
    {
    }

    IEnumerator GhostCoroutine()
    {
        while (true)
        {
            // 추격 대상의 위치 가져오기
            Vector2 targetPosition = target.position;

            // 추격자와 추격 대상 사이의 방향 계산
            Vector2 direction = targetPosition - rb.position;
            direction.Normalize(); // 방향 벡터 정규화

            yield return null;

            while (currentWalkCount < walkCount)
            {
                animator.SetBool("Walking", true);
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) //x와의 거리가 더 크다면
                {
                    //rb.velocity = new Vector2(Mathf.Sign(direction.x) * chaseSpeed, 0f);
                    transform.Translate(Mathf.Sign(direction.x) * speed, 0, 0);
                    //Animation
                    animator.SetFloat("DirX", Mathf.Sign(direction.x)); // x벡터 값을 전달해서 animation을 실행시킴
                    animator.SetFloat("DirY", 0); // y벡터 값을 전달해서 animation을 실행시킴
                }
                else
                {
                    //rb.velocity = new Vector2(0f, Mathf.Sign(direction.y) * chaseSpeed);
                    transform.Translate(0, Mathf.Sign(direction.y) * speed, 0);
                    //Animation
                    animator.SetFloat("DirX", 0); // x벡터 값을 전달해서 animation을 실행시킴
                    animator.SetFloat("DirY", Mathf.Sign(direction.y)); // y벡터 값을 전달해서 animation을 실행시킴
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
            animator.SetBool("Walking", false);
        }
    }
}