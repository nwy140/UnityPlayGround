using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DynamicObstacle : MonoBehaviour
{
    //strings
    public string obstacleType;//storage, defense, craft_station... 
    public string obstacleSlug;
    public string obstacleName;
    //gameObjects
    public GameObject interactionPoint; 
    //lists
    public List<Node> occupiedNodes = new List<Node>();
    public List<Transform> buildingTransforms = new List<Transform>();

    public void AddBuildingTransforms()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Cube"))
                buildingTransforms.Add(child);
        }
    }
}
