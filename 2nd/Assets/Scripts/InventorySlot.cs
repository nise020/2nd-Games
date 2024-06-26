using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;//IPointerEnterHandler �����͵�
using UnityEngine.UI;

//�������� �巡�� �ϰų� �巡���ؼ� ������ �� ��ũ��Ʈ�� �巡�� 
//�������� �ľ��Ͽ� �ڽ����� �з�����

public class InventorySlot : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler, IDropHandler
//IPointerDownHandler, IPointerUpHandler <- ���콺�� �ش翵���� ���ų� ������ ���
{
    //EventSystem�� ���̶�Ű�� �־�� �����(Canvers ������)


    Image imgSlot;
    RectTransform rect;

    private void Awake()
    {
        imgSlot = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// ���콺�� �κ��丮 ���� ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)//�̷� �Լ��� ����ϸ� ����ȭ�� ����
    {
        //���콺�� �κ��丮 ���� ����
        imgSlot.color = Color.red;
    }
    /// <summary>
    /// ���콺�� �κ��丮 ���� ������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //���콺�� �κ��丮 ���� ������
        imgSlot.color = Color.white;
    }
    /// <summary>
    /// �̺�Ʈ �ý������� ���� �׷��� �Ǵ� ����� �� ��ũ��Ʈ ������
    /// ��ӵǰ� �Ǹ� �ش� ��� ������Ʈ�� ���� �ڽ����� �����մϴ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null) 
        {
            eventData.pointerDrag.transform.SetParent(transform);//�θ�� ����
            eventData.pointerDrag.transform.position = rect.position;//�ش���ġ�� �̵�
        }
    }
}
