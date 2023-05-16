using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    //Variablels

    public float speed; // 캐릭터 이동속력 변수 
    public int walkCount;
    protected int currentWalkCount; //현재 walkCount loop를 빠져나가기 위한 변수
    public LayerMask layerMask; // 통과 불가 레이어 설정

    protected Vector3 vector; // x,y,z의 값을 동시에 갖는 Vector 변수
    public Animator animator; // 애니메이션 관리를 위한 변수
    public BoxCollider2D boxCollider;
    private bool notCoroutine = false; //코루틴 중복 실행 방지

    public string characterName;
    public Queue<string> queue;

    public void Move(string _dir, int _frequency = 5)  //실행할때마다 큐에 방향이 enqueue
    {
        queue.Enqueue(_dir);
        if(!notCoroutine)
        {
            notCoroutine = true;
            StartCoroutine(MoveCoroutine(_dir, _frequency));
        }

    }

    IEnumerator MoveCoroutine(string _dir, int _frequency) //이동 코루틴
    {
        while(queue.Count != 0) //큐가 빌때까지 반복
        {
            switch (_frequency) //무한 대기 방지를 위한 스위치문
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

            string direction = queue.Dequeue();     
            vector.Set(0, 0, vector.z);

            switch (direction)
            {
                case "UP":
                    vector.y = 1f;
                    break;
                case "DOWN":
                    vector.y = -1f;
                    break;
                case "RIGHT":
                    vector.x = 1f;
                    break;
                case "LEFT":
                    vector.x = -1f;
                    break;
            }

            //Animation
            animator.SetFloat("DirX", vector.x); // x벡터 값을 전달해서 animation을 실행시킴
            animator.SetFloat("DirY", vector.y); // y벡터 값을 전달해서 animation을 실행시킴

            while (true) //충돌체크
            {
                bool checkCollisionFlag = CheckCollision();
                if (checkCollisionFlag)
                {
                    animator.SetBool("Walking", false); // 이동 애니메이션 false => 장애물이 있다면 움직이지 않음
                    yield return new WaitForSeconds(1f); //1초 대기
                }
                else
                {
                    break; //만약 장애물이 없다면 break
                }
            }


            animator.SetBool("Walking", true);

            //움직이고자 하는 방향으로 Boxcolider 이동
            boxCollider.offset = new Vector2(vector.x * 0.7f * speed * walkCount, vector.y * 0.7f * speed * walkCount);


            while (currentWalkCount < walkCount)
            {
                //코루틴 내에서 초기화 시켰기 때문에 다음과 같이 코드를 작성하여도 가능
                transform.Translate(vector.x * speed, vector.y * speed, 0); //Translate 현재 위치 값에 ()안의 값을 더해줌, 즉 speed의 값만큼 더해줌 
                currentWalkCount++;
                if (currentWalkCount == 12) boxCollider.offset = Vector2.zero;
                yield return new WaitForSeconds(0.01f); // () 안만큼 대기
                                                        //if currentWalkCount가 walkCount가 되면 반복문을 빠져나감 
            }
            currentWalkCount = 0;

            //애니메이션 끊김 현상 수정
            if (_frequency != 5) animator.SetBool("Walking", false);
        }
        animator.SetBool("Walking", false); //반복이 종료되면 애니메이션 off
        //애니메이션 지속 오류 디버그
        notCoroutine = false;
    }
    
    //충돌 감지 함수 
    protected bool CheckCollision()
    {
        //RayCast : A -> B로 레이저를 쏴 아무것도 맞지 않는다면 hit == Null; else hit = 방해물
        RaycastHit2D hit;

        Vector2 start = transform.position; // 현재 캐릭터 위치 값
        Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); //이동하려고 하는 위치 값

        boxCollider.enabled = false; //hit 값에 해당 오브젝트가 들어가지 않도록 함
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true;

        if (hit.transform != null) return true; //충돌되는 물체가 있다면 break
        return false;
    }
}
