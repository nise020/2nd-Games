using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] GameObject viewInventory;//인벤토리뷰
    [SerializeField] GameObject fabItem;//인벤토리에 생설될 프리팹

    List<Transform> listInventory = new List<Transform>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        initInventoru();
    }

    private void initInventoru() //인벤토리 초기화
    {
        listInventory.Clear();//초기화 작업(오류방지)_

        Transform[] childs = viewInventory.transform.GetComponentsInChildren<Transform>();

        listInventory.AddRange(childs);
        listInventory.RemoveAt(0);//0번(본인) 지우기
        //AddRange<-2개 이상 더할때
        //Add<-1개의 데이터 더할때

        #region 주의사항+메모
        //Transform[] childs = viewInventory.transform.GetComponentsInChildren<Transform>();
        //자식들 관리//[]<-Array 구조
        //GetComponents<-자식들을 전부(복수) 관리<-s를 안붙이면 한번 기능하고 사라짐
        //본인을 포함하기 때문에 +1이 된다
        #endregion
    }
    /// <summary>
    /// 인벤토리가 열려있으면 닫힘,닫혀 있으면 열림
    /// </summary>
    public void InActiveInventory()
    {
        #region 다른형태
        //if (viewInventory.activeSelf==true)//!viewInventory
        //{
        //    viewInventory.SetActive(false);
        //}
        //else
        //{
        //    viewInventory.SetActive(true);
        //}
        #endregion
        viewInventory.SetActive(!viewInventory.activeSelf);//bool
    }
    /// <summary>
    /// 비어있는 인벤토리 번호를 리턴,-1이 리턴되면 비어 있는 슬롯이 없다
    /// </summary>
    /// <returns>비어있는 아이템 슬롯번호</returns>
    private int getEmptyItem() 
    {
        int count = listInventory.Count;
        for(int iNum = 0; iNum < count; iNum++) 
        {
            Transform trs = listInventory[iNum];
            if (trs.childCount == 0) 
            {
                return iNum;
            }
        }
        return -1;
    }
}


    
