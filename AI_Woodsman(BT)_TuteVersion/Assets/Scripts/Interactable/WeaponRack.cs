using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRack : Storage
{
    private void Start()
    {
        itemMax = 4; 
        obstacleSlug = "weapon_rack";
        obstacleName = "Weapon Rack"; 
        obstacleType = "storage"; 
       // CreateItemList("Axe");
        interactionPoint = transform.Find("InteractionPoint").gameObject;
        for (int i = 0; i < itemMax; i++)
        {
            storedItems.Add(ItemDatabase.instance.FetchItemByID(6));
        }
        AddBuildingTransforms();
    }
}
