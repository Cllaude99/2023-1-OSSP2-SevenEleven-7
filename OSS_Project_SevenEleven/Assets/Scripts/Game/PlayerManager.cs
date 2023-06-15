using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MovingObject
{
    // Variables

    //Static
    static public PlayerManager instance; //static으로 선언된 변수의 값을 공유

    public float runSpeed; // 달리기 속력

    public bool notMove = false;

    public string currentMapName;
    public string currentSceneName; // 캐릭터가 어느 씬에 있는지 확인하기 위한 용도.

    public string walkSound_1; // 이름으로 접근해서 사운드 이용
    public string walkSound_2;
    public string walkSound_3;
    public string walkSound_4;

    // 스테미나 기능 추가

    public float maxStemina; // 최대 스테미나
    public float currentStemina; // 현재 스테미나
    public float decreaseStemina; // 감소시킬 스테미나의 수치
    public float recoverStemina; // 회복 시킬 스테미나의 수치

    private float applyRunSpeed; // 실제 적용 RunSpeed
    public bool canMove = true; //코루틴 다중 실행 방지
    public bool applyRunFlag = false;

    private AudioManager theAudio;

    public GameObject current_transfer;
    public bool istransfer = false;

    public bool ghostNotMove = false;

    public bool isDeathPoint = false;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>(); //큐 초기화
        animator = GetComponent<Animator>(); // 컴포넌트를 animator 변수에 불러옴
        boxCollider = GetComponent<BoxCollider2D>();
        theAudio = FindObjectOfType<AudioManager>();
        //SceneManager.LoadScene("TitleScene");
        //theSaveNLoad.CallSave(0);
    }

    IEnumerator MoveCoroutine() // 대기시간을 만들어줄 Coroutine
    {
        while ((Input.GetAxisRaw("Vertical") != 0 && !notMove) || (Input.GetAxisRaw("Horizontal") != 0 && !notMove)) // 단일 코루틴 속 이동을 계속 가능하게 함
        {
            //Runs
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (currentStemina >= decreaseStemina)
                {
                    applyRunSpeed = runSpeed;
                    applyRunFlag = true;
                    currentStemina -= decreaseStemina; //현재 스테미나가 0보다 크다면 계속 감소
                }
                else
                {
                    currentStemina = 0;
                    applyRunSpeed = 0;
                    applyRunFlag = false;
                }
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }



            //Vector Set
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z); //벡터값 설정

            if (vector.x != 0) vector.y = 0; //좌우로 이동한다면 y벡터를 0으로 만들어 이동에 이상함이 없도록 함

            //Animation
            animator.SetFloat("DirX", vector.x); // x벡터 값을 전달해서 animation을 실행시킴
            animator.SetFloat("DirY", vector.y); // y벡터 값을 전달해서 animation을 실행시킴

            bool checkCollisionFlag = base.CheckCollision();
            if (checkCollisionFlag) break;

            animator.SetBool("Walking", true); // Bool 값을 전달해서 animation 전이

            //AUDIO
            int temp = Random.Range(1, 4);
            switch (temp)
            {
                case 1:
                    theAudio.Play(walkSound_1);
                    break;

                case 2:
                    theAudio.Play(walkSound_2);
                    break;

                case 3:
                    theAudio.Play(walkSound_3);
                    break;

                case 4:
                    theAudio.Play(walkSound_4);
                    break;
            }

            //움직이고자 하는 방향으로 Boxcolider 이동
            boxCollider.offset = new Vector2(vector.x * 0.7f * speed * walkCount, vector.y * 0.7f * speed * walkCount);

            //Add Value
            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0); //Translate 현재 위치 값에 ()안의 값을 더해줌, 즉 speed의 값만큼 더해줌
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }


                if (applyRunFlag) currentWalkCount++; // Run Flag가 잡혔을 때 cuurentWalkCount를 두배씩 증가시킴

                currentWalkCount++;
                if (currentWalkCount == 12) boxCollider.offset = Vector2.zero;

                yield return new WaitForSeconds(0.01f); // () 안만큼 대기
                                                        //speed = 2.4, walkcount = 20 => 2.4 * 20 = 48 
                                                        //방향키 한번마다 48픽셀씩 움직이도록 함

                //if currentWalkCount가 20이 되면 반복문을 빠져나감 

            }
            currentWalkCount = 0; // 0으로 초기화
        }
        animator.SetBool("Walking", false);
        canMove = true; //방향키 입력이 가능하도록 함
    } // 다중 처리 기능 함수

    // Update is called once per frame
    void Update()
    {
        if (!applyRunFlag)
        {
            if (currentStemina > maxStemina) currentStemina = maxStemina;
            else currentStemina += recoverStemina * Time.deltaTime;
        }

        if (canMove && !notMove) //코루틴 다중 실행 방지 분기문
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) //방향키에 따라 -1 또는 1 리턴
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DeathPoint")
        {
            isDeathPoint = true;
            collision.gameObject.SetActive(false);
        }
    }
}