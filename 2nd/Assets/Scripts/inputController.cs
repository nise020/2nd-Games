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
        if (Input.GetKeyDown(KeyCode.I) && inventoryManager != null)//입력시 해당 캔버스 생성
        {
            inventoryManager.InActiveInventory();
        }
    }
}
