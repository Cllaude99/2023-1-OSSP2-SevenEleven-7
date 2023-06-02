using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostManager : MovingObject
{
    public Transform target; // 추격 대상(플레이어)
    public string gameOver;  //게임오버씬
    public float lifeTime;
    public GameObject GhostPrefab;

    private Rigidbody2D rb;

    private AudioManager audioManager;
    private PlayerManager thePlayer;
    private ParticleSystem theParticle;   //파티클 오브젝트 연결
    BGMManager BGM;

    public int PlayMusicTrack;

    public bool ghostcanMove = true;

    public bool ghostdeath = false;             //귀신 상태 변수 true로 바꾸면 죽는 애니메이션 시작
    public bool ghost_stop_corutine = false;     //귀신 죽으면 코루틴 종료용 변수

    private void Start()
    {
        BGM = FindObjectOfType<BGMManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        thePlayer = FindObjectOfType<PlayerManager>();

        Destroy(GameObject.Find(GhostPrefab.name), lifeTime);

        BGM.Play(PlayMusicTrack);//생성시 브금 재생
        BGM.Loop();
        Invoke("stopBGM", 25); //소멸시 브금 중지
        thePlayer.istransfer = false; //맵 자동 이동 버그 수정
    }

    private void Update()
    {
        if (!thePlayer.notMove&&!thePlayer.ghostNotMove&&ghostcanMove)     
        {
            ghostcanMove = false; //중복 코루틴 방지
            StartCoroutine(GhostCoroutine()); //코루틴 실행
        }

        if (Input.GetKeyDown(KeyCode.K))              // K키누르면 귀신죽임 테스트용!!!
                    ghostdeath = true;                //테스팅용 충돌시 변수 업데이트 
    }

    IEnumerator GhostCoroutine()
    {
        if(ghostdeath)                               //귀신이 죽을 시에 ghostdeath가 true로 바뀌게 설정하면 죽는모션시작
        {
            if(!ghost_stop_corutine)
                Invoke("playDeath",3f);                         // Invoke함수로 부적 사용애니메이션끝나고 귀신죽는애니메이션
            ghost_stop_corutine = true;
            yield break;
        }


        if (thePlayer.isDeathPoint)
        {
            BGM.FadeOutMusic();
            Destroy(GameObject.Find(GhostPrefab.name));
            thePlayer.isDeathPoint = false;
        }

        if (thePlayer.istransfer) Invoke("warpGhost", 1f);
        thePlayer.istransfer = false;

        // 추격 대상의 위치 가져오기
        Vector2 targetPosition = target.position;


        // 추격자와 추격 대상 사이의 방향 계산
        Vector2 direction = targetPosition - rb.position;
        direction.Normalize(); // 방향 벡터 정규화

        //정규환 된 벡터를 통해 vector값 결정 (1,0,0) or (0,1,0)
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

        //Animation
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
        ghostcanMove = true;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            stopBGM();
            thePlayer.currentMapName = gameOver; // 만약 이동 영역과 부딪힌다면 이동할 맵의 이름을 Player오브젝트로 넘겨줌
            SceneManager.LoadScene(gameOver); // transferMapName으로 이동
        }
        
    }

    void warpGhost()
    {
        this.gameObject.transform.position = thePlayer.current_transfer.GetComponent<TransferMap>().target.transform.position;
    }

    void stopBGM()
    {
        BGM.FadeOutMusic();
        BGM.UnLoop();
    }

    void playDeath()
    {
            stopBGM();
            BoxCollider2D boxCollider =GetComponent<BoxCollider2D>();
            boxCollider.enabled= false;                             //귀신죽으면 브금 끄고 boxcollider off (충돌방지)
            theParticle =FindObjectOfType<ParticleSystem>();
            Vector2 particle_postion = transform.position;
            theParticle.transform.position = particle_postion;      //파티클 포지션을 귀신 포지션으로 옮기고 재생
            theParticle.Play();
            animator.SetBool("Death", true);                        //죽는 애니메이션 재생
            
    }
}