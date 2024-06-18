using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitBox : MonoBehaviour
{
    public enum ehitBoxType//밖에 만들면 어디서든 검색 가능
    {
        //같은이름 충돌 방지 때문에 취급 주의
        WallCheck,
        BodyCheck,

    }

    [SerializeField] ehitBoxType hitBoxType;
    MoveController moveController;
    private void Awake()
    {
        moveController = GetComponentInParent<MoveController>();//부모선언
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
