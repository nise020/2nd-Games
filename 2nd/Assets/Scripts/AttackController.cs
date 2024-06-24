using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    Camera mainCam;
    [SerializeField] Transform trshand;//Hand
    [SerializeField] GameObject objWapon;//������Ʈ
    [SerializeField] Transform trsWapon;//���� ����Ʈ
    [SerializeField] Transform trsobDynamic;//������
    [SerializeField] Vector2 throwForce = new Vector2 (10,0f);

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
            CreatWapon();
        }
    }
    private void CreatWapon() 
    {
        GameObject go = Instantiate(objWapon, trsWapon.position, trsWapon.rotation,trsobDynamic);//Į ����
        ThrowWapon gos = go.GetComponent<ThrowWapon>();
        bool isRight = transform.localScale.x < 0 ? true : false;//.x < 0 ��� true or false
        Vector2 fixedforce = throwForce;
        if (isRight==false) 
        {
            fixedforce = -throwForce;//x 10 , y 0
        }
        gos.setForce(trsWapon.rotation * fixedforce, isRight);//ȸ��

    }

}
