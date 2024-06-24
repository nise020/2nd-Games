using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWapon : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 force;
    //����X�� = Vector
    bool right;
    bool isdone = false;
    
    private void Awake()
    {
        rigid=GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid.AddForce(force, ForceMode2D.Impulse);
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isdone =true;
        //wapon ������ �ݶ��̴� ���� �ε��� palyer�� �ڷ� �з���
        //�����ϱ� ���� �������� ���� ���ϰ� Layer�� ����
        //Edit - ProjectSeting - Physics2D - LayerCollision
    }
    // Update is called once per frame
    void Update()
    {
        if (isdone == true) { return; }
        transform.Rotate(new Vector3(0,0,
            right == true ? -360f :360)*Time.deltaTime);
        
    }

    public void setForce(Vector2 _force, bool _isRight) 
    {
        force = _force;//Vector2
        right = _isRight;//bool

    }
}
