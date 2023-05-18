using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    //이벤트 관리를 위한 스크립트

    //Variables

    //Private
    private PlayerManager thePlayer; //이벤트 도중 키입력 처리 방지
    private List<MovingObject> characters;




    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>(); 
    }

    public void PreLoadCharacter() //이벤트 전에 호출할 함수
    {
        characters = ToList();
    }

    public List<MovingObject> ToList()
    {
        List<MovingObject> tempList = new List<MovingObject>();
        MovingObject[] temp = FindObjectsOfType<MovingObject>(); //플레이어와 NPC가 모두 배열 안에 들어감 

        for (int i = 0; i < temp.Length; i++)
        {
            tempList.Add(temp[i]);
        }

        return tempList;
    }
    public void Move(string _name, string _dir) //특정 방향으로 이동
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].Move(_dir);
            }
        }
    }


    public void Turn(string _name, string _dir) //특정 방향으로 회전
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].animator.SetFloat("DirX", 0f);
                characters[i].animator.SetFloat("DirY", 0f);
                switch (_dir)
                {
                    case "UP":
                        characters[i].animator.SetFloat("DirY",1f);
                        break;
                    case "DOWN":
                        characters[i].animator.SetFloat("DirY", -1f);
                        break;
                    case "RIGHT":
                        characters[i].animator.SetFloat("DirX", 1f);
                        break;
                    case "LEFT":
                        characters[i].animator.SetFloat("DirX", -1f);
                        break;
                }

            }
        }
    }

    public void SetTransparent(string _name) //투명화
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetUnTransparent(string _name) //투명화 해제
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(true);
            }
        }
    }
    
    public void SetThorought(string _name) //Layer 통과
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].boxCollider.enabled = false;
            }
        }
    }

    public void SetUnThorought(string _name) //Layer 통과 불가능
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].boxCollider.enabled = true;
            }
        }
    }

    //캐릭터 추격씬 추가 예정




    // Update is called once per frame
    void Update()
    {
        
    }
}
