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

        //이렇게 사용 가능
        //itemData = (TextAsset)Resources.Load("ItemData");
        //itemData = Resources.Load<TextAsset>("ItemData");//null

        //확장자 입력ㄴㄴ/폴더명/ItemData<-파일명


    }
    

    public string GetNameFromIdx(string _idx) 
    {
        if (itemDatas == null) return string.Empty;

        return itemDatas.Find(x => x.idx ==_idx).sprit;

        //스프라이트 매니저를 만들어서 이름으로 스츠라이트를 가져올수 잇도록
    }
}
