using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCMove
{
    [Tooltip("NPCMove�� üũ�ϸ� NPC�� ������")]
    public bool NPCmove;
    public string[] direction; //NPC�� ������ ����

    [Range(1, 5)]
    [Tooltip("1(�ſ� õõ��) ~ 5(�ſ� ������)")]
    public int frequency; // ������ �������� �����̴� ��
    //

}
public class NPCManager : MovingObject
{
    [SerializeField]
    public NPCMove npc;
    public bool ischase = false;
    public Transform target;
    public GameObject current_transfer;
    public bool istransfer = false;
    public string currentMapName;
    public float distance;
    private Rigidbody2D rb;
    private bool NPCCanMove = true;
    private PlayerManager thePlayer;
    private float npcRunspeed;
    private bool npcNotmove;

    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        if (npc.NPCmove)
        {
            SetMove();
        }
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        thePlayer = FindObjectOfType<PlayerManager>();
        boxCollider = GetComponent<BoxCollider2D>();
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
        if (ischase && NPCCanMove)
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
        distance = Vector2.Distance(targetPosion, rb.position);
        while (distance>72f&&distance<144f)
        {
            targetPosion = target.position;
            Vector2 direction = targetPosion - rb.position;
            direction.Normalize();
            distance = Vector2.Distance(targetPosion, rb.position);

            if (thePlayer.applyRunFlag)
            {
                npcRunspeed = 2.4f;
            }
            else
            {
                npcRunspeed = 0;
            }

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) //x�� ���Ͱ��� �� ũ�ٸ� x�� ���Ͱ��� ����ȭ
            {
                vector.x = Mathf.Sign(direction.x);
                vector.y = 0;
            }
            else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                vector.x = 0;
                vector.y = Mathf.Sign(direction.y);
            }
            animator.SetFloat("DirX", vector.x); // x���� ���� �����ؼ� animation�� �����Ŵ
            animator.SetFloat("DirY", vector.y); // y���� ���� �����ؼ� animation�� �����Ŵ
            animator.SetBool("Walking", true);
            //�����̰��� �ϴ� �������� Boxcolider �̵�
            boxCollider.offset = new Vector2(vector.x * 0.7f * speed * walkCount, vector.y * 0.7f * speed * walkCount);
            while (currentWalkCount < walkCount)
            {

                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) //x���� �Ÿ��� �� ũ�ٸ�
                {
                    //rb.velocity = new Vector2(Mathf.Sign(direction.x) * chaseSpeed, 0f);
                    transform.Translate(vector.x * (speed + npcRunspeed), 0, 0);
                }
                else
                {
                    //rb.velocity = new Vector2(0f, Mathf.Sign(direction.y) * chaseSpeed);
                    transform.Translate(0, vector.y * (speed + npcRunspeed), 0);

                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
        }

        animator.SetBool("Walking", false);
        NPCCanMove = true;
    }
    IEnumerator MoveCoroutine()
    {
        if (npc.direction.Length != 0)
        {
            for (int i = 0; i < npc.direction.Length; i++)
            {
                //��Ÿ�� ���� ����
                yield return new WaitUntil(() => queue.Count < 2); //ť�� ��� 0�� 1���̷� ���� ��Ŵ
                //ť�� 3�� ��� ��� ������� �߻�

                base.Move(npc.direction[i], npc.frequency);

                if (i == npc.direction.Length - 1) //���� �ݺ��� ����
                {
                    i = -1;
                }
            }
        }
    }
}