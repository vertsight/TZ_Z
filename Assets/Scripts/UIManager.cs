using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour
{
    public Button dropButton;
    private ItemInteraction itemInteraction;

    [Inject]
    public void Construct(ItemInteraction _itemInteraction)
    {
        itemInteraction = _itemInteraction;
        dropButton.onClick.AddListener(() => itemInteraction.DropItem());
        dropButton.gameObject.SetActive(false);
    }

    public void ShowDropButton() => dropButton.gameObject.SetActive(true);
    public void HideDropButton() => dropButton.gameObject.SetActive(false);
}

