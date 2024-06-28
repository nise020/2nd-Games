using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//�巡�װ� ������ �κ��丮 ������

public class ItemUi : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler
{

    Transform canvas;//�巡�� �Ҷ� ����UI �ڷ� �׷����� ���� �����ϱ� ���� ��� �̿��� ĵ����
    Transform beforeParant;//Ȥ�� �߸��� ��ġ�� ����ϰ� �Ǹ� ���ƿ��� ���� ��ġ
    //����ĳ��Ʈ Ÿ��
    CanvasGroup canvasGroup;//�ڽĵ��� ���� �����ϴ� ������Ʈ
    Image imgItem;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();     
    }
    void Start()
    {
        canvas = InventoryManager.Instance.CanvarsInvenTory;
    }

    /// <summary>
    /// idx �ѹ��� ���� ������ �ش� �ƴ����� json���� ���� �˻��Ͽ� ã��
    /// �ش� ������ �����Ϳ��� �ʿ��� �������� �����ͼ� �ش� ��ũ��Ʈ�� ä����
    /// </summary>
    /// <param name="_idx">������ �ε��� �ѹ�</param>
    public void SetItem(string _idx) 
    {
        string spritName = JsonManager.instance.GetNameFromIdx(_idx);//��������Ʈ
        
        imgItem = GetComponent<Image>();
        imgItem.sprite = SpritManager.instance.GetSprite(spritName); 
    }
    /// <summary>
    /// �巡�� ���� ��� -> IBeginDragHandler
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        beforeParant = transform.parent;

        transform.SetParent(canvas);

        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position =eventData.position;//�巡����
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(transform.parent ==canvas) 
        {
            transform.position =beforeParant.position;
            transform.SetParent(beforeParant);
            //�߸��� ��ġ�� ���� �θ� ��ġ�� ����
        }
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        //�κ���� ������ �������� ���� �ϴ� ����� �� �ڵ尡 �� �ִ� iTem�� �θ��� Slot�� �ƴ϶�
        //�� ���� �θ��� Inventory�� ���� �ʿ�
    }
}
