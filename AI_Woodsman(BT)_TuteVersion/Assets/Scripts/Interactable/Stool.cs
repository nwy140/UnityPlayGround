using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stool : DynamicObstacle
{
    private void Start()
    {
        obstacleName = "Stool";
        obstacleSlug = "stool";
        obstacleType = "storage";

        interactionPoint = transform.Find("InteractionPoint").gameObject; 

        buildingTransforms = new List<Transform>();

    }
}
