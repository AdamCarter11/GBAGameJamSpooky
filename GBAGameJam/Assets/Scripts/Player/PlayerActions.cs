using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] Inventory inventoryRef;
    [SerializeField] PlayerMovement playerMoveRef;
    [SerializeField] Animator animatorRef;
    Item itemInUse = null;
    private bool isSwitchingItem;

    [Header("Barrel Vars")]
    [SerializeField] Transform throwPoint;
    [SerializeField] GameObject barrelObj;
    [SerializeField] float throwForce;
    [SerializeField] float rollSpeed = 3f;
    [SerializeField] float destroyTime = 8f;

    [Header("Mario hat vars")]
    [SerializeField] GameObject headBounceObj;

    [Header("SwordVars")]
    [SerializeField] bool canSwing = true;

    public void OpenInventory(InputAction.CallbackContext context)
    {
        
        if(context.performed)
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                playerMoveRef.canMove = false;
                isSwitchingItem = true;
            }
            else
            {
                Time.timeScale = 1;
                playerMoveRef.canMove = true;
                isSwitchingItem = false;
            }
        }
        

        // open inventory display
    }

    public void ChangeItemLeft(InputAction.CallbackContext context)
    {
        if (!isSwitchingItem && Time.timeScale != 0) return;
        if (context.performed)
        {
            inventoryRef.SelectPreviousItem();
            print("Left item");
            ActivateItem();
        }
    }
    public void ChangeItemRight(InputAction.CallbackContext context)
    {
        if (!isSwitchingItem && Time.timeScale != 0) return;
        if (context.performed)
        {
            inventoryRef.SelectNextItem();
            print("Right item");
            ActivateItem();
        }
    }

    public void UseItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            itemInUse = inventoryRef.ReturnItem();
            print("use item" + itemInUse.itemName);
            if (itemInUse != null)
            {
                if (itemInUse.itemName.Contains("Barrel"))
                {
                    ThrowBarrel();
                }
                if (itemInUse.itemName == "Dragon sword" && canSwing)
                {
                    SwingSword();
                }
            }
        }
    }

    private void ActivateItem()
    {
        itemInUse = inventoryRef.ReturnItem();
        if (itemInUse != null)
        {
            if (itemInUse.itemName.Contains("Bouncy"))
            {
                EquipMarioHat(true);
            }
            else
            {
                EquipMarioHat(false);
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
            StartCoroutine(BarrelCooldown());
        }
    }
    IEnumerator BarrelCooldown()
    {
        canThrowBarrel = false;
        yield return new WaitForSeconds(itemInUse.cooldown);
        canThrowBarrel = true;
    }
    #endregion

    #region Item: Mario hat
    private void EquipMarioHat(bool equip)
    {
        if (equip)
            headBounceObj.SetActive(true);
        else
            headBounceObj.SetActive(false);
    }
    #endregion

    #region Item Sword
    private void SwingSword()
    {
        print("Swing Sword");
        animatorRef.SetBool("Attacking", true);
        StartCoroutine(SwingCooldown());
    }

    IEnumerator SwingCooldown()
    {
        canSwing = false;
        playerMoveRef.canMove = false;
       
        yield return new WaitForSeconds(.5f);
        canSwing = true;
        animatorRef.SetBool("Attacking", false);
        playerMoveRef.canMove = true;
    }
    #endregion
}
