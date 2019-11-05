using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingPlacement : MonoBehaviour
{
    //lists
    public List<GameObject> buildings = new List<GameObject>();
    //gameObjects
    public GameObject currentBuilding;
    //bools
    public bool hitBuilding;
    //singleton
    public static BuildingPlacement instance; 

    private void Start()
    {
        instance = this; 
    }

    private void Update()
    {
        PlaceBuilding(); 
    }

    private void PlaceBuilding()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            if (currentBuilding != null)
            {
                Node nodeToChange = Grid.instance.NodeFromWorldPoint(interactionInfo.point);
                Vector3 moveTransform = nodeToChange.nodeWorldPosition;
                moveTransform.y = 0.01f;
                currentBuilding.transform.position = moveTransform;

                if (Input.GetKeyUp(KeyCode.R))
                    currentBuilding.transform.Rotate(currentBuilding.transform.rotation.x, currentBuilding.transform.rotation.y + 90, currentBuilding.transform.rotation.z);

                if (Input.GetMouseButtonUp(0) && hitBuilding == false && !EventSystem.current.IsPointerOverGameObject())
                {
                    UnitManager.instance.buildMode = false;
                    Destroy(currentBuilding.GetComponent<Rigidbody>());
                    currentBuilding.GetComponent<Collider>().isTrigger = true;
                    for (int i = 0; i < currentBuilding.GetComponent<DynamicObstacle>().buildingTransforms.Count; i++)
                    {
                        Vector3 posToScan = currentBuilding.GetComponent<DynamicObstacle>().buildingTransforms[i].position;
                        Node nodeToScan = Grid.instance.NodeFromWorldPoint(posToScan);
                        nodeToScan.walkable = false;
                        currentBuilding.GetComponent<DynamicObstacle>().occupiedNodes.Add(nodeToScan);
                        Destroy(currentBuilding.GetComponent<DynamicObstacle>().buildingTransforms[i].gameObject);
                    }
                    if (UnitManager.instance.movingUnits.Count > 0)
                    {
                        for (int i = 0; i < UnitManager.instance.movingUnits.Count; i++)
                        {
                            for (int y = 0; y < UnitManager.instance.movingUnits[i].GetComponent<UnitTasks>().path.Length; y++)
                            {
                                for (int z = 0; z < currentBuilding.GetComponent<DynamicObstacle>().occupiedNodes.Count; z++)
                                {
                                    if (UnitManager.instance.movingUnits[i].GetComponent<UnitTasks>().path[y] ==
                                        currentBuilding.GetComponent<DynamicObstacle>().occupiedNodes[z].nodeWorldPosition)
                                    {
                                        //dynamic pathfinding has been disabled in this project for the sake of simplicity. Download
                                        //it from the dynamic pathfining video and integrate if you are interested! 
                                        break;
                                    }
                                }
                            }
                        }


                    }
                    GameObject building = currentBuilding;
                    building.name = currentBuilding.GetComponent<DynamicObstacle>().obstacleSlug;
                    AddStorageBuildingToStorageManager(building);
                    if (building.GetComponent<DynamicObstacle>().obstacleType == "storage")
                        building.tag = "Interactable"; 
                    building.layer = LayerMask.NameToLayer("Unwalkable");
                    building.transform.position = currentBuilding.transform.position;
                    Destroy(building.GetComponent<GroundSense>()); 
                    currentBuilding = null;
                }
            }

            if (interactionInfo.collider.transform.gameObject.name == "Building" && Input.GetKeyDown(KeyCode.B))
            {
                GameObject buildingToDestroy = interactionInfo.collider.transform.gameObject;

                for (int i = 0; i < buildingToDestroy.GetComponent<DynamicObstacle>().occupiedNodes.Count; i++)
                {
                    buildingToDestroy.GetComponent<DynamicObstacle>().occupiedNodes[i].walkable = true;

                }
                Destroy(buildingToDestroy);
            }
        }
    }

    private void AddStorageBuildingToStorageManager(GameObject storageBuilding)
    {
        if(storageBuilding.name == "wood_stockpile")
        {
            InteractablesManager.instance.woodStockpiles.Add(storageBuilding); 
        }
        else if (storageBuilding.name == "weapon_rack")
        {
            InteractablesManager.instance.toolRacks.Add(storageBuilding);

        }
        else if (storageBuilding.name == "wardrobe")
        {
            InteractablesManager.instance.wardrobes.Add(storageBuilding);

        }
        else if (storageBuilding.name == "armor_stand")
        {
            InteractablesManager.instance.armorStands.Add(storageBuilding);
        }
        else if (storageBuilding.name == "chest_of_drawers")
        {
            InteractablesManager.instance.chestOfDrawers.Add(storageBuilding);
        }
        else if (storageBuilding.name == "stool")
        {
            InteractablesManager.instance.stools.Add(storageBuilding);
        }
    }
}



