using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wapon : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 forse;
    bool right;

    private void Awake()
    {
        rigid=GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid.AddForce(forse, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,
            right == true ? -360f :360)*Time.deltaTime);
        
    }
}
