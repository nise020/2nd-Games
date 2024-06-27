using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class coin : MonoBehaviour
{
    InventoryManager inventoryManager;
    [SerializeField] string itemIdx;

    private void Awake()
    {
        inventoryManager = InventoryManager.Instance;

        //cItemDate testData = new cItemDate();
        //testData.idx = "00000001";
        //testData.sprit = GetComponent<SpriteRenderer>().sprite.name;
        //string value = JsonConvert.SerializeObject(testData);
        //"{"idx":"000000000","sprit":"coin(1)"}"

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //콜라이더가 닿았을때
        //NameToLayer-1개만 정의 할 수 있다
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))//닿은 대상이 플레이어면은?
        {
            inventoryManager.GetItem(itemIdx);
            //인벤토리 매니저에게 내가 습득 되는지 확인

            BoxCollider2D box = collision.GetComponent<BoxCollider2D>();

            
        }
    }
    
}
