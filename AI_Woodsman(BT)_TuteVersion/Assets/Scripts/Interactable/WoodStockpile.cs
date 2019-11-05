using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodStockpile : Storage
{
    private void Start()
    {
        obstacleSlug = "wood_stockpile";
        obstacleName = "Wood Stockpile";
        obstacleType = "storage";
         CreateItemList("Log");

        interactionPoint = transform.Find("InteractionPoint").gameObject; 

        AddBuildingTransforms();
    }
}
