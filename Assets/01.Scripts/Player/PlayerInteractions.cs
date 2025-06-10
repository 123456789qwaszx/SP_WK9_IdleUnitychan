using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public EquipTool curEquip;
    public Transform equipParent;

    public EquipmentData itemData;

    public Action addItem;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Item"))
        {
            curInteractGameObject = collision.collider.gameObject;
            curInteractable = curInteractGameObject.GetComponent<IInteractable>();

            curInteractable.OnInteract();
        }
    }


    public void EquipNew(EquipmentData data)
    {
        UnEquip();
        curEquip = Instantiate(data.equipPrefab, equipParent).GetComponent<EquipTool>();
    }

    public void UnEquip()
    {
        if (curEquip != null)
        {
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}
