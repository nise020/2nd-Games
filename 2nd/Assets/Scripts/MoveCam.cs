using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    [SerializeField] Transform chaseTrs;


    void Update()
    {
        //ī�޶� chaseTrs�� ����ٴѴ�
        //�Ǵ� ī�޶� �ڽ����� �־�θ� ����ٱ�� ������� �Ǳ⵵ �Ѵ��� 
        Vector3 fixpos = chaseTrs.position;
        fixpos.z = transform.position.z;
        transform.position = fixpos;

    }
}
