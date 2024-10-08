using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>(); // List to hold the items
    public int selectedIndex = 0; // Keeps track of the currently selected item
    public Text selectedItemText; // UI Text to display the selected item (optional)

    void Start()
    {
        UpdateSelectedItemUI();
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        SelectPreviousItem();
    //    }
    //    else if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        SelectNextItem();
    //    }
    //}

    public void AddItem(Item newItemToAdd)
    {
        items.Add(newItemToAdd);
    }

    public void SelectPreviousItem()
    {
        if(items.Count <= 0)
        {
            selectedIndex = 0;
        }
        else
        {
            selectedIndex--;

            if (selectedIndex < 0)
            {
                selectedIndex = items.Count - 1; // Cycle to the last item
            }

            UpdateSelectedItemUI();
        }
        
    }

    public void SelectNextItem()
    {
        if(items.Count <= 0)
        {
            selectedIndex = 0;
        }
        else
        {
            selectedIndex++;

            if (selectedIndex >= items.Count)
            {
                selectedIndex = 0; // Cycle back to the first item
            }

            UpdateSelectedItemUI();
        }
    }
    public Item ReturnItem()
    {
        if(items.Count > 0)
        {
            return items[selectedIndex];
        }
        else
        {
            return null;
        }
    }

    public int ReturnPrevIndex()
    {
        int prevIndex = selectedIndex;
        if (items.Count <= 1)
        {
            return -1;
        }
        else
        {
            prevIndex--;

            if (prevIndex <= 0)
            {
                return (items.Count-1); // Cycle back to the first item
            }
            return prevIndex;
        }
    }
    public int ReturnNextIndex()
    {
        int nextIndex = selectedIndex;
        if (items.Count <= 1)
        {
            return -1;
        }
        else
        {
            nextIndex++;
            if (nextIndex >= items.Count)
            {
                return 0; // Cycle back to the first item
            }
            return nextIndex;
        }
    }

    public int ReturnCurrIndex()
    {
        return selectedIndex;
    } 

    public int ReturnItemCount()
    {
        return items.Count;
    }

    public Item ReturnItemAtIndex(int index)
    {
        if (index < 0 || index >= items.Count)
            return null;

        return items[index];
    }

    void UpdateSelectedItemUI()
    {
        if (selectedItemText != null && items.Count > 0)
        {
            //selectedItemText.text = "Selected Item: " + items[selectedIndex];
        }
    }
}
