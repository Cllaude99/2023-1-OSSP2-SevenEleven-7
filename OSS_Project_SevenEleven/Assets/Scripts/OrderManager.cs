using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    //이벤트 관리를 위한 스크립트

    //Variables

    //Private
    private PlayerManager thePlayer; //이벤트 도중 키입력 처리 방지
    private List<MovingObject> characters;

    //Public

    


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

    public void Move(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].Move(_dir);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
