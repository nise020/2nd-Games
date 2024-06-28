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

        #region Jason �޸�
        //cItemDate testData = new cItemDate();
        //testData.idx = "00000001";
        //testData.sprit = GetComponent<SpriteRenderer>().sprite.name;
        //string value = JsonConvert.SerializeObject(testData);
        //"{"idx":"000000000","sprit":"coin(1)"}"
        #endregion

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�ݶ��̴��� �������
        //NameToLayer-1���� ���� �� �� �ִ�
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))//���� ����� �÷��̾����?
        {
            if(inventoryManager.GetItem(itemIdx)==true)
            {

                Destroy(gameObject);

            }
            //�κ��丮 �Ŵ������� ���� ���� �Ǵ��� Ȯ��

            BoxCollider2D box = collision.GetComponent<BoxCollider2D>();

            
        }
    }
    
}
