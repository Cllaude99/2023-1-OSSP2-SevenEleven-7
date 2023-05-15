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

    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
    }

    public void SetMove()
    {
        StartCoroutine(MoveCoroutine());
    }

    public void SetNotMove()
    {
        StopAllCoroutines();
    }

    IEnumerator MoveCoroutine()
    {
        if(npc.direction.Length != 0)
        {
            for (int i = 0; i < npc.direction.Length; i++)
            {
                switch(npc.frequency)
                {
                    case 1:
                        yield return new WaitForSeconds(4f);
                        break;
                    case 2:
                        yield return new WaitForSeconds(3f);
                        break;
                    case 3:
                        yield return new WaitForSeconds(2f);
                        break;
                    case 4:
                        yield return new WaitForSeconds(1f);
                        break;
                    case 5:
                        break;
                }

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
