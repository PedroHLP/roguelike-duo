using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsHandler : MonoBehaviour
{
    public static ItemsHandler Instance;

    public List<BaseItem> allGameItems;
    public List<BaseItem> allCurrentPlayerItems;

    private List<int> currentSelectedItems;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        foreach (BaseItem item in allGameItems)
        {
            item.itemCurrentStack = 0;
        }
    }

    public List<BaseItem> GetItemsToShowOnLevelUp(int quantity)
    {
        currentSelectedItems = new List<int>();

        List<BaseItem> itemsToShow = new List<BaseItem>();

        for (int i = 0; i < quantity; i++)
        {
            BaseItem itemToAdd = GetValidItem();

            foreach (BaseItem item in allCurrentPlayerItems)
            {
                if (item.itemID == itemToAdd.itemID)
                {
                    itemToAdd = item;
                    break;
                }

            }

            itemsToShow.Add(itemToAdd);
        }

        return itemsToShow;
    }

    private BaseItem GetValidItem()
    {
        int itemToGet = UnityEngine.Random.Range(0, allGameItems.Count);

        if (currentSelectedItems.Contains(itemToGet))
        {
            return GetValidItem();
        }

        foreach (BaseItem item in allCurrentPlayerItems)
        {
            if (allGameItems[itemToGet] == item)
            {
                if (item.itemCurrentStack >= item.GetItemMaxStack())
                {
                    return GetValidItem();
                }
            }
        }

        currentSelectedItems.Add(itemToGet);
        return allGameItems[itemToGet];
    }

    public void AddItemToPlayer(int id)
    {
        bool itemFound = false;
        foreach (BaseItem item in allCurrentPlayerItems)
        {
            if (item.itemID == id)
            {
                itemFound = true;

                item.itemCurrentStack++;
                item.OnStackUp();
            }
        }

        if (!itemFound)
        {
            foreach (BaseItem item in allGameItems)
            {
                if (item.itemID == id)
                {
                    allCurrentPlayerItems.Add(item);
                    item.OnGetItem();
                    item.itemCurrentStack++;
                }
            }
        }
    }
}
