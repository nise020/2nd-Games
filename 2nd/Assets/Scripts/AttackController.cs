using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    Camera mainCam;
    [SerializeField] Transform trshand;//Hand
    [SerializeField] GameObject objWapon;//오브젝트
    [SerializeField] Transform trsWapon;//생성 포인트
    [SerializeField] Transform trsobDynamic;//생성탭
    [SerializeField] Vector2 throwForce = new Vector2 (10,0f);

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
            CreatWapon();
        }
    }
    private void CreatWapon() 
    {
        GameObject go = Instantiate(objWapon, trsWapon.position, trsWapon.rotation,trsobDynamic);//칼 생성
        ThrowWapon gos = go.GetComponent<ThrowWapon>();
        bool isRight = transform.localScale.x < 0 ? true : false;//.x < 0 경우 true or false
        Vector2 fixedforce = throwForce;
        if (isRight==false) 
        {
            fixedforce = -throwForce;//x 10 , y 0
        }
        gos.setForce(trsWapon.rotation * fixedforce, isRight);//회전

    }

}
