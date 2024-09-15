using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] Inventory inventoryRef;
    [SerializeField] PlayerMovement playerMoveRef;
    Item itemInUse = null;

    public void OpenInventory(InputAction.CallbackContext context)
    {
        
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            playerMoveRef.canMove = false;
        }
        else
        {
            Time.timeScale = 1;
            playerMoveRef.canMove = true;
        }

        // open inventory display
    }

    public void ChangeItemLeft(InputAction.CallbackContext context)
    {
        if (playerMoveRef.canMove) return;
        if (context.performed)
        {
            inventoryRef.SelectPreviousItem();
            print("Left item");
        }
    }
    public void ChangeItemRight(InputAction.CallbackContext context)
    {
        if (playerMoveRef.canMove) return;
        if (context.performed)
        {
            inventoryRef.SelectNextItem();
            print("Right item");
        }
    }

    public void UseItem(InputAction.CallbackContext context)
    {
        itemInUse = inventoryRef.ReturnItem();
        if(itemInUse != null)
        {
            // call item effect function

        }
    }
}
