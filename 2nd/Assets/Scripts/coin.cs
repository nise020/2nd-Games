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
        //�ݶ��̴��� �������
        //NameToLayer-1���� ���� �� �� �ִ�
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))//���� ����� �÷��̾����?
        {
            inventoryManager.GetItem(itemIdx);
            //�κ��丮 �Ŵ������� ���� ���� �Ǵ��� Ȯ��

            BoxCollider2D box = collision.GetComponent<BoxCollider2D>();

            
        }
    }
    
}
