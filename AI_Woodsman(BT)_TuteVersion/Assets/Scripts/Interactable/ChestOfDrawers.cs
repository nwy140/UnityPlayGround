using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOfDrawers : Storage
{
    private void Start()
    {
        itemMax = 10; 
        obstacleSlug = "chest_of_drawers";
        obstacleName = "Chest of Drawers";
        obstacleType = "storage";
        //reateItemList("Pants");
        interactionPoint = transform.Find("InteractionPoint").gameObject;

        AddBuildingTransforms();

        for(int i = 0; i < itemMax; i++)
        {
            storedItems.Add(ItemDatabase.instance.FetchItemByID(2)); 
        }
    }
}