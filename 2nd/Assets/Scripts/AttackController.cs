using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    Camera mainCam;
    private void Start()
    {
        mainCam = Camera.main;//메잌 카메라
        //Camera.current = mainCam;
    }
    // Update is called once per frame
    void Update()
    {
        checkAwake();
    }
    private void checkAwake()
    {
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
    }
}
