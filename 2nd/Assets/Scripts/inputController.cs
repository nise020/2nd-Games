using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputController : MonoBehaviour
{

    InventoryManager inventoryManager;
    
    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
    }

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && inventoryManager != null)//�Է½� �ش� ĵ���� ����
        {
            inventoryManager.InActiveInventory();
        }
    }
}
