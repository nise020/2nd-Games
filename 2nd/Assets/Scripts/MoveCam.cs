using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    [SerializeField] Transform chaseTrs;


    void Update()
    {
        //[SerializeField]에 데이터가 chaseTrs를 따라다닌다
        //또는 이 방법 말고 카메라를 자식으로 넣어두면 따라다기기 기능이 되기도 한더ㅏ 
        Vector3 fixpos = chaseTrs.position;
        fixpos.z = transform.position.z;
        transform.position = fixpos;

    }
}
