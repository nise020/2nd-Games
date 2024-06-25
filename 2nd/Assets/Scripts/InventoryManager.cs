using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] GameObject viewInventory;//�κ��丮��
    [SerializeField] GameObject fabItem;//�κ��丮�� ������ ������

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
        listInventory.Clear();//�ʱ�ȭ �۾�(��������)_

        Transform[] childs = viewInventory.transform.GetComponentsInChildren<Transform>();

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


    
