using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCardButtonHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text itemName, itemDescription, stacksCount;

    [SerializeField]
    private Image itemIcon;

    [SerializeField]
    private Button thisButton;

    private int itemID;

    private void Start()
    {
        thisButton.onClick.AddListener(SelectItem);
    }

    public void SetUpItemCard(BaseItem item)
    {
        itemName.text = item.GetItemDetails().itemName;
        itemDescription.text = item.GetItemDetails().itemDescription;
        stacksCount.text = item.itemCurrentStack + " / " + (item.GetItemMaxStack() - 1);
        itemIcon.sprite = item.GetItemDetails().itemSprite;

        itemID = item.itemID;
    }

    private void SelectItem()
    {
        ItemsHandler.Instance.AddItemToPlayer(itemID);
        GameUIHandler.Instance.ToggleLevelUpScreen(false);
        GameManager.Instance.ChangeGameState(GameState.Playing);
    }


}
