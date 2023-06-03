using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private DatabaseManager theDatabase;
    private OrderManager theOrder;
    private AudioManager theAudio;
    private OkOrCancel theOOC;
    private DialogueManager theDialogueManager;
    private Menu theMenu;

    public string key_sound;
    public string enter_sound;
    public string cancel_sound;
    public string open_sound; // 인벤토리 킬때 들리는 사운드
    public string beep_sound; // 옳지 않은 행동을 했을때 비프소리 들려주도록 (ex. 퀘스트 아이템 사용시)

    private InventorySlot[] slots; // 인벤토리 슬롯들

    public List<Item> inventoryItemList; // 플레이어가 소지한 아이템 리스트.
    private List<Item> inventoryTabList; // 선택한 탭에 따라 다르게 보여질 아이템 리스트.

    public Text Description_Text; // 부연 설명.
    public string[] tabDescription; // 탭 부연 설명.

    public Transform tf; // slot 부모객체.

    public GameObject go; // 인벤토리 활성화 비활성화.
    public GameObject[] selectedTabImages;
    public GameObject go_OOC; // 선택지 활성화 비활성화
    public GameObject prefab_Floating_Text; // 플로팅 텍스트

    public GameObject menu_obj; // 메뉴연결용 오브젝트

    private int selectedItem; // 선택된 아이템.
    private int selectedTab; // 선택된 탭

    private int page; 
    private int slotCount; // 활성화된 슬롯개수
    private const int MAX_SLOTS_COUNT = 10; // 최대슬롯개수


    public bool activated; // 인벤토리 활성화시 true.
    private bool activated_Menu; // 인벤토리&메뉴UI 동기화용변수.

    private bool tabActivated; // 탭 활성화시 true.
    private bool itemActivated; // 아이템 활성화시 true.
    private bool stopKeyInput; // 키입력 제한 (소비할 때 질의가 나올 텐데, 그 때 키입력 방지)
    private bool preventExec; // 중복실행 제한.

    private GameObject diaryNote; //일기장 사진

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        theAudio = FindObjectOfType<AudioManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theDatabase = FindObjectOfType<DatabaseManager>();
        theOOC = FindObjectOfType<OkOrCancel>();
        theDialogueManager = FindObjectOfType<DialogueManager>();
        theMenu = FindObjectOfType<Menu>();

        inventoryItemList = new List<Item>();
        inventoryTabList = new List<Item>();
        slots = tf.GetComponentsInChildren<InventorySlot>();
    }

    public List<Item> SaveItem()
    {
        return inventoryItemList;
    }
    public void LoadItem(List<Item> _itemList)
    {
        inventoryItemList = _itemList;
    }

    public void GetAnItem(int _itemID, int _count = 1)
    {
        for (int i = 0; i < theDatabase.itemList.Count; i++) // 데이터베이스 아이템 검색
        {
            if (_itemID == theDatabase.itemList[i].itemID) // 데이터베이스에 아이템 발견
            {
                //var clone = Instantiate(prefab_Floating_Text, PlayerManager.instance.transform.position, Quaternion.Euler(Vector3.zero));
                //clone.GetComponent<FloatingText>().text.text = theDatabase.itemList[i].itemName + " " + _count + "개 획득 +";
                //clone.transform.SetParent(this.transform);
                
                inventoryItemList.Add(theDatabase.itemList[i]); // 소지품에 해당 아이템이 없는 경우 소지품에 해당 아이템 추가
                inventoryItemList[inventoryItemList.Count - 1].itemCount = _count;
                return;
            }
        }
        Debug.LogError("데이터베이스에 해당 ID값을 가진 아이템이 존재하지 않습니다."); // 데이터베이스에 ItemID 없음.
    }
    public void RemoveSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    } // 인벤토리 슬롯 초기화

    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();
    } // 탭 활성화
    public void SelectedTab()
    {
        StopAllCoroutines();
        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0f;
        for (int i = 0; i < selectedTabImages.Length; i++)
        {
            selectedTabImages[i].GetComponent<Image>().color = color;
        }
        Description_Text.text = tabDescription[selectedTab];
        StartCoroutine(SelectedTabEffectCoroutine());
    } // 선택된 탭을 제외하고 다른 모든 탭의 컬러 알파값 0으로 조정.
    IEnumerator SelectedTabEffectCoroutine()
    {
        while (tabActivated)
        {
            Color color = selectedTabImages[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }// 선택된 탭 반짝임 효과

    public void ShowItem()
    {
        inventoryTabList.Clear();
        RemoveSlot();
        selectedItem = 0;
        page = 0;

        if (selectedTab == 0) // 퀘스트 아이템을 선택한 경우
        {
            for (int i = 0; i < inventoryItemList.Count; i++)
            {
                if (Item.ItemType.Quest == inventoryItemList[i].itemType)
                    inventoryTabList.Add(inventoryItemList[i]);
            }
        }   //그것을 인벤토리 탭 리스트에 추가



        ShowPage();

        SelectedItem();
    }// 아이템 활성화 (inventoryTabList에 조건에 맞는 아이템들만 넣어주고, 인벤토리 슬롯에 출력)
    
    public void ShowPage()
    {
        slotCount = -1;
        for (int i = page*MAX_SLOTS_COUNT; i < inventoryTabList.Count; i++)
        {
            slotCount = i - (page * MAX_SLOTS_COUNT);
            slots[slotCount].gameObject.SetActive(true);
            slots[slotCount].Additem(inventoryTabList[i]);

            if (slotCount == MAX_SLOTS_COUNT-1)
                break;

        } // 인벤토리 탭 리스트의 내용을, 인벤토리 슬롯에 추가
    }

    public void SelectedItem()
    {
        StopAllCoroutines();
        if (slotCount> -1)
        {
            Color color = slots[0].selected_Item.GetComponent<Image>().color;
            color.a = 0f;
            for (int i = 0; i <= slotCount; i++)
                slots[i].selected_Item.GetComponent<Image>().color = color;
            Description_Text.text = inventoryTabList[selectedItem].itemDescription;
            StartCoroutine(SelectedItemEffectCoroutine());
        }
        else
            Description_Text.text = "Nothing in my pocket...";
    }// 선택된 아이템을 제외하고, 다른 모든 탭의 컬러 알파값을 0으로 조정.
    IEnumerator SelectedItemEffectCoroutine()
    {
        while (itemActivated)
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }// 선택된 아이템 반짝임 효과.

    // Update is called once per frame
    void Update()
    {
        if (theDialogueManager.talking == false)
        {
            if (!stopKeyInput)
            {
                if ((Input.GetKeyDown(KeyCode.I) && theMenu.activated == false) || activated_Menu) //  I키를 누르거나 메뉴UI에서인벤토리버튼을 누르면 인벤토리 창 활성화
                {
                    activated = !activated;
                    activated_Menu = false;
                    theMenu.activated = false;
                    if (activated)
                    {
                        theAudio.Play(open_sound);
                        theOrder.NotMove();
                        go.SetActive(true);
                        selectedTab = 0;
                        tabActivated = true;
                        itemActivated = false;
                        ShowTab();
                    }
                    else
                    {
                        theAudio.Play(cancel_sound);
                        StopAllCoroutines();
                        go.SetActive(false);
                        tabActivated = false;
                        itemActivated = false;
                        theOrder.Move();
                    }
                }

                if (activated)
                {
                    if (tabActivated)
                    {
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectedTab < selectedTabImages.Length - 1)
                                selectedTab++;
                            else
                                selectedTab = 0;
                            theAudio.Play(key_sound);
                            SelectedTab();
                        }
                        else if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectedTab > 0)
                                selectedTab--;
                            else
                                selectedTab = selectedTabImages.Length - 1;
                            theAudio.Play(key_sound);
                            SelectedTab();
                        }
                        else if (Input.GetKeyDown(KeyCode.Z))
                        {
                            theAudio.Play(enter_sound);
                            Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
                            color.a = 0.25f;
                            selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                            itemActivated = true;
                            tabActivated = false;
                            preventExec = true;
                            ShowItem();
                        }
                    }// 탭 활성화시 키입력 처리.

                    else if (itemActivated)
                    {
                        if (inventoryTabList.Count > 0)
                        {
                            if (Input.GetKeyDown(KeyCode.DownArrow))
                            {
                                if (selectedItem + 2 > slotCount)
                                {
                                    if (page < (inventoryTabList.Count - 1) / MAX_SLOTS_COUNT)
                                        page++;
                                    else
                                        page = 0;
                                    RemoveSlot();
                                    ShowPage();
                                    selectedItem = -2;
                                }

                                if (selectedItem < slotCount - 1)
                                    selectedItem += 2;
                                else
                                    selectedItem %= 2;
                                theAudio.Play(key_sound);
                                SelectedItem();
                            }
                            else if (Input.GetKeyDown(KeyCode.UpArrow))
                            {
                                if (selectedItem - 2 < 0)
                                {
                                    if (page != 0)
                                        page--;
                                    else
                                        page = (inventoryTabList.Count - 1) / MAX_SLOTS_COUNT;
                                    RemoveSlot();
                                    ShowPage();
                                }

                                if (selectedItem > 1)
                                    selectedItem -= 2;
                                else
                                    selectedItem = slotCount - selectedItem;
                                theAudio.Play(key_sound);
                                SelectedItem();
                            }
                            else if (Input.GetKeyDown(KeyCode.RightArrow))
                            {
                                if (selectedItem + 1 > slotCount)
                                {
                                    if (page < (inventoryTabList.Count - 1) / MAX_SLOTS_COUNT)
                                        page++;
                                    else
                                        page = 0;
                                    RemoveSlot();
                                    ShowPage();
                                    selectedItem = -1;
                                }

                                if (selectedItem < slotCount)
                                    selectedItem++;
                                else
                                    selectedItem = 0;
                                theAudio.Play(key_sound);
                                SelectedItem();
                            }
                            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                            {
                                if (selectedItem - 1 < 0)
                                {
                                    if (page != 0)
                                        page--;
                                    else
                                        page = (inventoryTabList.Count - 1) / MAX_SLOTS_COUNT;
                                    RemoveSlot();
                                    ShowPage();
                                }

                                if (selectedItem > 0)
                                    selectedItem--;
                                else
                                    selectedItem = slotCount;
                                theAudio.Play(key_sound);
                                SelectedItem();
                            }
                            else if (Input.GetKeyDown(KeyCode.Z) && !preventExec)
                            {
                                if (selectedTab == 0) // 소모품
                                {
                                    theAudio.Play(enter_sound);
                                    stopKeyInput = true;
                                    StartCoroutine(OOCCoroutine());
                                }
                                else if (selectedTab == 1)
                                {
                                    // 장비 장착
                                }
                                else // 퀘스트, 기타의 경우 비프음 출력
                                {
                                    theAudio.Play(beep_sound);
                                }
                            }
                        }

                        if (Input.GetKeyDown(KeyCode.X))
                        {
                            theAudio.Play(cancel_sound);
                            StopAllCoroutines();
                            itemActivated = false;
                            tabActivated = true;
                            ShowTab();
                        }
                    }// 아이템 활성화시 키입력 처리.

                    if (Input.GetKeyUp(KeyCode.Z)) // 중복 실행 방지.
                        preventExec = false;
                }
            }
        }
        if (CheckItemsInRange()) //부적1~4 전부 사라지고, 최종부적을 획득
        {
            inventoryItemList.RemoveAll(item => item.itemID >= 10001 && item.itemID <= 10004);
            GetAnItem(10005, 1);
        }
    }


    public void OnclickFromMenu()  //메뉴 UI에서 인벤토리UI 접근용 변수(인벤토리버튼온클릭이랑연결)
    {
        if(go.activeSelf==false)
            activated_Menu= true;
    }

    public void Close_Menu()   // 인벤토리 UI 열면 메뉴 UI 닫기(인벤토리버튼온클릭이랑연결)
    {
        menu_obj.SetActive(false);
    }


    IEnumerator OOCCoroutine()
    {
        go_OOC.SetActive(true);
        theOOC.ShowTwoChoice("Use", "Cancel");
        yield return new WaitUntil(() => !theOOC.activated);
        if (theOOC.GetResult())
        {
            for (int i = 0; i < inventoryItemList.Count; i++)
            {
                if (inventoryItemList[i].itemID == inventoryTabList[selectedItem].itemID)
                {
                    //theDatabase.UseItem(inventoryItemList[i].itemID); -> 소모품이 있을 경우에만 넣어줌 (지금은 물약같은게 없으므로 패스)

                    if (inventoryItemList[i].itemID == 10029) // 지갑을 사용하면 키를 획득하고, 지갑은 사라짐
                    {
                        GetAnItem(10027, 1);
                        inventoryItemList.RemoveAt(i);
                    }
                    else if (10016 <= inventoryItemList[i].itemID && inventoryItemList[i].itemID <= 10025)
                    {
                        string diaryname = (inventoryItemList[i].itemID-10).ToString();
                        diaryNote = GameObject.Find(diaryname);
                        diaryNote.SetActive(true);
                        theOrder.NotMove();


                        if (Input.GetKey(KeyCode.Z))
                        {
                            diaryNote.SetActive(false);
                            theOrder.Move();
                        }

                    }

                    else // 그외의 경우는 사용해도 아무런 변화 x
                    {
                        theAudio.Play(cancel_sound);
                    }

                    //여러개의 같은 소모품인 경우 use하면 카운트만 감소시키도록 설정 (지금은 없음)
                    /*else if (inventoryItemList[i].itemCount > 1) 
                        inventoryItemList[i].itemCount--;   */ 

                    ShowItem();
                    break;
                }
            }
        }

        stopKeyInput = false;
        go_OOC.SetActive(false);
    }

    public bool CheckItemsInRange() //필요한 아이템을 다 모았는지 체크하는 함수 (여기선 부적을 위해)
    {
        int[] itemCodes = new int[] { 10001, 10002, 10003, 10004 };
        bool[] foundItems = new bool[4];

        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            for (int j = 0; j < itemCodes.Length; j++)
            {
                if (inventoryItemList[i].itemID == itemCodes[j])
                {
                    foundItems[j] = true;
                    break;
                }
            }
        }

        foreach (bool found in foundItems)
        {
            if (!found) return false;
        }

        return true;
    }

}
