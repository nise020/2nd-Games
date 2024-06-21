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
        mainCam = Camera.main;//���� ī�޶�
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

        //fixedPos.x > 0 �Ǵ� transform.localScale.x -1 ->����, 1 ->������
        # region �ܼ� ����
        //float angle = Quaternion.FromToRotation(Vector3.up, fixedPos).eulerAngles.z;//����,xyzw
        //trshand.rotation = Quaternion.Euler(0, 0, angle);//�ܼ� ����
        #endregion

        float angle = Quaternion.FromToRotation(transform.localScale.x < 0 
            ? Vector3.right : Vector3.left, fixedPos).eulerAngles.z;//����,xyzw
        //Debug.Log(angle);
        //���콺 Ŀ�� ���� ���� �����ϱ�
        trshand.rotation = Quaternion.Euler(0, 0, angle);
        #region trshand�� �� �ٸ� �ڵ�
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
