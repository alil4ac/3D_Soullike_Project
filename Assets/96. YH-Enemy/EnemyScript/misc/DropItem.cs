using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField]
    int itemType; //무기 0, 헬멧 1, 상의 2, 장갑3, 하의4
    [SerializeField]
    int itemIndex; //각 아이템 타입의 정리된 순서(천, 가죽, 경갑 이런거)

    bool isPicked=false;

    void Start()
    {
        isPicked = false;
        itemType = Random.Range(0, (int)ItemColumn.EItemType.END);
        itemIndex = Random.Range(0,3);

        Debug.Log("아이템 타입 : " + itemType + "\n" + "아이템 인덱스 : " + itemIndex);
    }

   

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerHit"))
        {
            UIManager.Instance.ActivePickItem();
            if (Input.GetKeyDown(KeyCode.E)&&isPicked==false&&UIManager.Instance.IsActiveInteraction==false)
            {
                isPicked=true;
                CharacterManager.Instance.GetItem(itemType, itemIndex);
                UIManager.Instance.DisablePickItem();
                
                UIManager.Instance.OpenPickUpItemLayer(itemType, itemIndex);
                Destroy(gameObject);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("PlayerHit"))
        {
            UIManager.Instance.DisablePickItem();
        }
    }

}
