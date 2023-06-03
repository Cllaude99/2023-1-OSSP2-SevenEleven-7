using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCMove
{
    [Tooltip("NPCMove를 체크하면 NPC가 움직임")]
    public bool NPCmove;
    public string[] direction; //NPC가 움직일 방향

    [Range(1, 5)] [Tooltip("1(매우 천천히) ~ 5(매우 빠르게)")]
    public int frequency; // 지정한 방향으로 움직이는 빈도
    //

}
public class NPCManager : MovingObject
{
    [SerializeField]
    public NPCMove npc;
    public bool ischase = false;
    public Transform target;
    private Rigidbody2D rb;
    private bool NPCCanMove=true;
    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        if(npc.NPCmove)
        {
            SetMove();
        }
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void SetMove()
    {
        StartCoroutine(MoveCoroutine());
    }

    public void SetNotMove()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        if (ischase&&NPCCanMove)
        {
            NPCCanMove = false;
            StartCoroutine(Chase());

        }
        if (npc.NPCmove)
        {
            npc.NPCmove = false;
            SetMove();
        }
    }
    IEnumerator Chase()
    {
        Vector2 targetPosion = target.position;
        Vector2 direction = targetPosion - rb.position;
        direction.Normalize();

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) //x의 벡터값이 더 크다면 x의 벡터값만 단위화
        {
            vector.x = Mathf.Sign(direction.x);
            vector.y = 0;
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            vector.x = 0;
            vector.y = Mathf.Sign(direction.y);
        }
        animator.SetFloat("DirX", vector.x); // x벡터 값을 전달해서 animation을 실행시킴
        animator.SetFloat("DirY", vector.y); // y벡터 값을 전달해서 animation을 실행시킴
        animator.SetBool("Walking", true);

        while (currentWalkCount < walkCount)
        {

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) //x와의 거리가 더 크다면
            {
                //rb.velocity = new Vector2(Mathf.Sign(direction.x) * chaseSpeed, 0f);
                transform.Translate(vector.x * speed, 0, 0);
            }
            else
            {
                //rb.velocity = new Vector2(0f, Mathf.Sign(direction.y) * chaseSpeed);
                transform.Translate(0, vector.y * speed, 0);

            }
            currentWalkCount++;
            yield return new WaitForSeconds(0.01f);
        }
        currentWalkCount = 0;
        animator.SetBool("Walking", false);
        NPCCanMove = true;
    }
    IEnumerator MoveCoroutine()
    {
        if(npc.direction.Length != 0)
        {
            for (int i = 0; i < npc.direction.Length; i++)
            {
                //런타임 에러 방지
                yield return new WaitUntil(() => queue.Count < 2); //큐를 계속 0과 1사이로 유지 시킴
                //큐가 3이 상될 경우 오버헤드 발생

                base.Move(npc.direction[i], npc.frequency);

                if(i == npc.direction.Length - 1) //무한 반복을 위함
                {
                    i = -1;
                }
            }
        }
    }
}
