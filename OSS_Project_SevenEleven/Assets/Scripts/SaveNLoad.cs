using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNLoad : MonoBehaviour
{
    [System.Serializable]
    public class Data // 모든 세이브 기록들을 담을 클래스
    {
        public float playerX;
        public float playerY;
        public float playerZ;

        public List<int> playerItemInventory; // 가지고 있던 아이템의 아이디값을 저장
        public List<int> playerItemInventoryCount; 

        public string mapName; // 캐릭터가 어느 맵에 있었는지
        public string sceneName; // 캐릭터가 어느 씬에 있었는지

        public List<bool> swList;
        public List<string> swNameList;
        public List<string> varNameList;
        public List<float> varNumberList;
    }

    private PlayerManager thePlayer; // 플레이어의 위치값을 알기위한 변수
    private DatabaseManager theDatabase;
    private Inventory theInven;

    public Data data;

    private Vector3 vector; // vector에 플레이어의 위치를 담고 playerX,playerY,playerZ에 넣고 불러줄 예정


    public void CallSave() // 세이브가 이루어지는 역할
    {
        theDatabase = FindObjectOfType<DatabaseManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theInven = FindObjectOfType<Inventory>();
    }

    public void CallLoad() // 로드가 이루어지는 역할
    {

    }
}
