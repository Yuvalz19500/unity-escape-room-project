using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room4Chest : MonoBehaviour
{
    [SerializeField] private int[] itemsIds; 
    
    public bool CheckIfHasItems()
    {
        List<int> playerInv = ingameGlobalManager.instance.currentPlayerInventoryList;
        bool hasItems = true;

        if (playerInv.Count == 0)
        {
            return false;
        }
        
        foreach (int itemId in itemsIds)
        {
            foreach (int invItem in playerInv)
            {
                if (invItem == itemId)
                {
                    hasItems = true;
                    break;
                }

                hasItems = false;
            }

            if (!hasItems)
            {
                break;
            }
        }

        return hasItems;
    }
}
