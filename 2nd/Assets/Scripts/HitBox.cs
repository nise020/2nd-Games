using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitBox : MonoBehaviour
{
    public enum ehitBoxType//�ۿ� ����� ��𼭵� �˻� ����
    {
        //�����̸� �浹 ���� ������ ��� ����
        WallCheck,
        BodyCheck,

    }

    [SerializeField] ehitBoxType hitBoxType;
    MoveController moveController;
    private void Awake()
    {
        moveController = GetComponentInParent<MoveController>();//�θ𼱾�
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        moveController.TriggerEnter(hitBoxType, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        moveController.TriggerExit(hitBoxType, collision);
    }

}
