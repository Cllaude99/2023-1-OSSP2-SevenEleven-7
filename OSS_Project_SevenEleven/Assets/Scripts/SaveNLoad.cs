using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveNLoad : MonoBehaviour
{
    [System.Serializable]
    public class Data // ��� ���̺� ��ϵ��� ���� Ŭ����
    {
        public float playerX;
        public float playerY;
        public float playerZ;

        public List<int> playerItemInventory; // ������ �ִ� �������� ���̵��� ����
        public List<int> playerItemInventoryCount; 

        public string mapName; // ĳ���Ͱ� ��� �ʿ� �־�����
        public string sceneName; // ĳ���Ͱ� ��� ���� �־�����

        public List<bool> swList;
        public List<string> swNameList;
        public List<string> varNameList;
        public List<float> varNumberList;
    }

    private DatabaseManager theDatabase;
    private PlayerManager thePlayer; // �÷��̾��� ��ġ���� �˱����� ����
    private Inventory theInven;

    public Data data;

    private Vector3 vector; // vector�� �÷��̾��� ��ġ�� ��� playerX,playerY,playerZ�� �ְ� �ҷ��� ����


    public void CallSave() // ���̺갡 �̷������ ����
    {
        theDatabase = FindObjectOfType<DatabaseManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theInven = FindObjectOfType<Inventory>();

        data.playerX = thePlayer.transform.position.x;
        data.playerY = thePlayer.transform.position.y;
        data.playerZ = thePlayer.transform.position.z;

        data.mapName = thePlayer.currentMapName;
        data.sceneName = thePlayer.currentSceneName;

        Debug.Log("���� ������ ����");

        data.playerItemInventory.Clear();
        data.playerItemInventoryCount.Clear();

        for(int i=0; i < theDatabase.var_name.Length; i++)
        {
            data.varNameList.Add(theDatabase.var_name[i]);
            data.varNumberList.Add(theDatabase.var[i]);
        }
        for (int i = 0; i < theDatabase.switch_name.Length; i++)
        {
            data.swNameList.Add(theDatabase.switch_name[i]);
            data.swList.Add(theDatabase.switches[i]);
        }

        List<Item> itemList = theInven.SaveItem();

        for(int i=0;i<itemList.Count; i++)
        {
            Debug.Log("�κ��丮�� ������ ���� �Ϸ� : " + itemList[i].itemID);
            data.playerItemInventory.Add(itemList[i].itemID);
            data.playerItemInventoryCount.Add(itemList[i].itemCount);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/SaveFile.dat");

        bf.Serialize(file, data);
        file.Close();

        Debug.Log(Application.dataPath + "�� ��ġ�� �����߽��ϴ�.");
    }

    public void CallLoad() // �ε尡 �̷������ ����
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/SaveFile.dat", FileMode.Open);

        if(file != null && file.Length > 0)
        {
            data = (Data)bf.Deserialize(file);

            theDatabase = FindObjectOfType<DatabaseManager>();
            thePlayer = FindObjectOfType<PlayerManager>();
            theInven = FindObjectOfType<Inventory>();

            thePlayer.currentMapName = data.mapName;
            thePlayer.currentSceneName = data.sceneName;

            vector.Set(data.playerX, data.playerY, data.playerZ);
            thePlayer.transform.position = vector;

            theDatabase.var = data.varNumberList.ToArray();
            theDatabase.var_name = data.varNameList.ToArray();
            theDatabase.switches = data.swList.ToArray();
            theDatabase.switch_name = data.swNameList.ToArray();

            List<Item> itemList = new List<Item>();

            for(int i=0; i < data.playerItemInventory.Count; i++)
            {
                for(int x = 0; x < theDatabase.itemList.Count; x++)
                {
                    if(data.playerItemInventory[i] == theDatabase.itemList[x].itemID)
                    {
                        itemList.Add(theDatabase.itemList[x]);
                        Debug.Log("�κ��丮 �������� �ε��߽��ϴ� : " + theDatabase.itemList[x].itemID);
                        break;
                    }
                }
            }

            for(int i=0;i<data.playerItemInventoryCount.Count; i++)
            {
                itemList[i].itemCount = data.playerItemInventoryCount[i];
            }

            theInven.LoadItem(itemList);

            // ���� ���� �ٸ����� �ִ� ��ü���� ���� �Ұ����̱� ������ ���̵��� �̷������ �� �� �� ���� �پ��մ� ���� �ٿ�带 ������
            GameManager theGM = FindObjectOfType<GameManager>();
            theGM.LoadStart();

            SceneManager.LoadScene(data.sceneName); 
        }
        else
        {
            Debug.Log("����� ���̺� ������ �����ϴ�.");
        }

        file.Close();
    }
}
