using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] GameObject viewInventory;//�κ��丮��
    [SerializeField] GameObject fabItem;//�κ��丮�� ������ ������

    public Transform CanvarsInvenTory => canvarsInvenTory;

    [SerializeField] Transform canvarsInvenTory;

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
    private void initInventoru() //�κ��丮 �ʱ�ȭ
    {
        listInventory.Clear();//�ʱ�ȭ �۾�(��������)_������ �뷮�� ũ�� ����

        Transform[] childs = viewInventory.transform.GetComponentsInChildren<Transform>();
        //Array ���� <- ������ ���� �ʴ´�

        listInventory.AddRange(childs);
        listInventory.RemoveAt(0);//0��(����) �����
        //AddRange<-2�� �̻� ���Ҷ�
        //Add<-1���� ������ ���Ҷ�

        #region ���ǻ���+�޸�
        //Transform[] childs = viewInventory.transform.GetComponentsInChildren<Transform>();
        //�ڽĵ� ����//[]<-Array ����
        //GetComponents<-�ڽĵ��� ����(����) ����<-s�� �Ⱥ��̸� �ѹ� ����ϰ� �����
        //������ �����ϱ� ������ +1�� �ȴ�
        #endregion
    }
    /// <summary>
    /// �κ��丮�� ���������� ����,���� ������ ����
    /// </summary>
    public void InActiveInventory()
    {
        #region �ٸ�����
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
    /// ����ִ� �κ��丮 ��ȣ�� ����,-1�� ���ϵǸ� ��� �ִ� ������ ����
    /// </summary>
    /// <returns>����ִ� ������ ���Թ�ȣ</returns>
    private int getEmptyItemSlot() 
    {
        int count = listInventory.Count;
        for(int iNum = 0; iNum < count; iNum++) 
        {
            Transform trsSlot = listInventory[iNum];
            if (trsSlot.childCount == 0) 
            {
                return iNum;
            }
        }
        return -1;
    }

    public bool GetItem(string _idx) 
    {
        int slotNum = getEmptyItemSlot();
        if (slotNum == -1) 
        {
            return false;
        }

        GameObject go = Instantiate(fabItem, listInventory[slotNum]);
        //������Ʈ���� �ʴ� _idx��ȣ�� ���� ���� �����;�
        ItemUi goSc = go.GetComponent<ItemUi>();
        goSc.SetItem(_idx);
        return true;
        //Resources<-���� �����ϸ� ��ȣȭ �Ǽ� ������ �Ұ��ϴ�
        //1�� ����
    }
}


    
