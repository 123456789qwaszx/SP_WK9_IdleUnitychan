using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public EquipmentData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.equipmentName}\n{data.equipmentValue[0].value}";
        return str;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.interaction.itemData = data;
        CharacterManager.Instance.Player.interaction.addItem?.Invoke();
        PoolManager.Instance.Push(gameObject);
    }

}
