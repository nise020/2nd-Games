using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemObjeact : MonoBehaviour
{
    InventoryManager inventoryManager;
    [SerializeField] string itemIdx;

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;

        #region Jason 메모
        //cItemDate testData = new cItemDate();
        //testData.idx = "00000001";
        //testData.sprit = GetComponent<SpriteRenderer>().sprite.name;
        //string value = JsonConvert.SerializeObject(testData);
        //"{"idx":"000000000","sprit":"coin(1)"}"
        #endregion

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //콜라이더가 닿았을때
        //NameToLayer-1개만 정의 할 수 있다
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))//닿은 대상이 플레이어면은?
        {
            if(inventoryManager.GetItem(itemIdx)==true)
            {

                Destroy(gameObject);

            }
            //인벤토리 매니저에게 내가 습득 되는지 확인

            BoxCollider2D box = collision.GetComponent<BoxCollider2D>();

            
        }
    }
    
}
