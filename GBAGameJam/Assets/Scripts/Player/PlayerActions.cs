using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] Inventory inventoryRef;
    [SerializeField] PlayerMovement playerMoveRef;
    Item itemInUse = null;

    [Header("Barrel Vars")]
    [SerializeField] Transform throwPoint;
    [SerializeField] GameObject barrelObj;
    [SerializeField] float throwForce;
    [SerializeField] float rollSpeed = 3f;
    [SerializeField] float destroyTime = 8f;

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
        if (context.performed)
        {
            itemInUse = inventoryRef.ReturnItem();
            print("use item");
            if (itemInUse != null)
            {
                // call item effect function
                if (itemInUse.itemName.Contains("Barrel"))
                {
                    ThrowBarrel();
                }
            }
        }
    }

    #region Item: Kong barrel
    bool canThrowBarrel = true;
    private void ThrowBarrel()
    {
        if (canThrowBarrel)
        {
            GameObject tempBarrel = Instantiate(barrelObj, throwPoint.position, Quaternion.identity);
            Vector2 throwDir = new Vector2(transform.localScale.x, 1f).normalized;
            tempBarrel.GetComponent<Rigidbody2D>().AddForce(throwDir * throwForce, ForceMode2D.Impulse);
            tempBarrel.GetComponent<Rigidbody2D>().AddTorque(rollSpeed * -transform.localScale.x, ForceMode2D.Impulse);
            Destroy(tempBarrel, destroyTime);
        }
    }
    #endregion

}
