using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    public static JsonManager instance;

    List<cItemDate> itemDatas;
   
    //[SerializeField] TextAsset itemData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }

        inItJason();
        
    }
    private void inItJason() 
    {
        TextAsset itemData = Resources.Load("ItemData") as TextAsset;
        itemDatas = JsonConvert.DeserializeObject<List<cItemDate>>(itemData.ToString());

        //�̷��� ��� ����
        //itemData = (TextAsset)Resources.Load("ItemData");
        //itemData = Resources.Load<TextAsset>("ItemData");//null

        //Ȯ���� �Է¤���/������/ItemData<-���ϸ�


    }
    

    public string GetNameFromIdx(string _idx) 
    {
        if (itemDatas == null) return string.Empty;

        return itemDatas.Find(x => x.idx ==_idx).sprit;

        //��������Ʈ �Ŵ����� ���� �̸����� ��������Ʈ�� �����ü� �յ���
    }
}
