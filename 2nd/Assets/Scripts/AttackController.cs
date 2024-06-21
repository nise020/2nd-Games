using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    Camera mainCam;
    [SerializeField] Transform trshand;//Hand
    [SerializeField] GameObject objWapon;
    [SerializeField] Transform trsWapon;
    private void Start()
    {
        mainCam = Camera.main;//메인 카메라
        //Camera.current = mainCam;
    }
    // Update is called once per frame
    void Update()
    {
        checkAim();
        checkCreat();
    }
    private void checkAim()
    {
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mouseWorldPos);
        Vector2 playerPos = transform.position;
        Vector2 fixedPos = mouseWorldPos - playerPos;

        //fixedPos.x > 0 또는 transform.localScale.x -1 ->왼쪽, 1 ->오른쪽
        # region 단순 베기
        //float angle = Quaternion.FromToRotation(Vector3.up, fixedPos).eulerAngles.z;//각도,xyzw
        //trshand.rotation = Quaternion.Euler(0, 0, angle);//단순 베기
        #endregion

        float angle = Quaternion.FromToRotation(transform.localScale.x < 0 
            ? Vector3.right : Vector3.left, fixedPos).eulerAngles.z;//각도,xyzw
        //Debug.Log(angle);
        //마우스 커서 보고 각도 조절하기
        trshand.rotation = Quaternion.Euler(0, 0, angle);
        #region trshand의 또 다른 코드
        //trshand.rotation = Quaternion.EulerRotation()
        //trshand.eulerAngles
        #endregion
    }

    private void checkCreat() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            Creat();
        }
    }
    private void Creat() 
    {
        //GameObject g0 = Instantiate(objWapon, );
    }
}
