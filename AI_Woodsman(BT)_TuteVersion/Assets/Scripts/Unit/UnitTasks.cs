using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class UnitTasks : MonoBehaviour
{
    #region variables
    //ints and floats
    float speed = 5f;
    int targetIndex;
    public float twoSecs = 2f;
    private int walkSpeedId;
    //vector3s and 2s
    public Vector3 currentWaypoint;
    public Vector3[] path;
    public Vector2 screenPos;
    public Vector3 target;
    private Vector3 closestHarvestObject;
    //quaternions
    public Quaternion rot;
    //gameObjects 
    public GameObject currentInteractable;
    public GameObject objectToCollect; 
    //components
    public Renderer renderer;
    private Equipment equipmentScript; 
    public Animator animator;
    //lists
    public List<Item> backpack = new List<Item>();
    //booleans
    public bool pathReached;
    [Task]
    public bool isWoodcutter;
    [Task]
    public bool isMiner;
    [Task]
    public bool isIdle;
    [Task]
    public bool hasRightPants;
    [Task]
    public bool hasRightEquipment;
    [Task]
    public bool hasRightTop;
    [Task]
    public bool hasRightGear;
    #endregion

    private void Start()
    {
        //components
        equipmentScript = GetComponent<Equipment>();
        animator = GetComponent<Animator>();
        renderer = transform.Find("Human").GetComponent<Renderer>();
        //bools
        isIdle = true;
        //vector3s 
        closestHarvestObject = new Vector3(999, 999, 999);
        //methods
       // EquipItem("Legs", "brown_pants");
     
    }

    #region movement tasks
    [Task]
    public void StandUp()
    {
        animator.SetBool("Sitting", false);
        Task.current.Succeed();
    }
    [Task]
    public void GoToDestination()
    {
        if (pathReached)
        {
            Task.current.Succeed();
        }
        //need some way to determine if the path will never be reached here so I can have a fail condition. 
        //one possibility to estimate how long it should take the unit to reach destination x, and then create a fail 
        //condition if that time is exceeded by some reasonable amount. 
    }
    [Task]
    public void FaceInteractable()
    {
        Vector3 dir = currentInteractable.transform.position - transform.position;
        dir.y = 0;
        rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 10f * Time.deltaTime);
        if ((rot.eulerAngles - transform.rotation.eulerAngles).sqrMagnitude < .01)
        {
            Task.current.Succeed();
        }
    }

    #endregion

    #region movement helper methods (including Sebastian Lague's pathfinding methods (slightly modified))
    //slightly modified method from Sebasian Lague's pathfining system
    IEnumerator FollowPath()
    {
        currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    pathReached = true;
                    animator.SetFloat("WalkSpeed", 0f);
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            animator.SetFloat("WalkSpeed", 3f);
            transform.rotation = Quaternion.LookRotation(currentWaypoint - transform.position);
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }
    //slightly modified method from Sebasian Lague's pathfining system
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            pathReached = false;
            path = newPath;
            targetIndex = 0;
            UnitManager.instance.movingUnits.Add(this.gameObject);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
    #endregion

    #region harvest tasks
    [Task]
    public void ResetClosestHavestObjectVariable()
    {
        closestHarvestObject = new Vector3(999, 999, 999);
        Task.current.Succeed(); 
    }
    [Task]
    public void SetDestinationToNearestHarvestObject()
    {
        if (isWoodcutter)
            FindClosestHarvestObject(InteractablesManager.instance.trees, "There are no trees to fell, Sire!");
        else if (isMiner)
            FindClosestHarvestObject(InteractablesManager.instance.boulders, "There are no rocks to mine, Sire!");
    }
    [Task]
    public void HarvestObject()
    {
        if (isWoodcutter)
        {
            animator.SetBool("Chop", true);
        }
        else if (isMiner)
        {

        }
        //action succeeds when current interactable (whether it's a reed, crop, rock or tree) is destroyed. 
        if (currentInteractable.GetComponent<HarvestObject>().harvestObjectHealth <= 0)
        {
            animator.SetBool("Chop", false);
            Task.current.Succeed();
        }
    }
    [Task]
    public void ProcessHarvestObject()
    {
        if (isWoodcutter && currentInteractable != null)
        {
            animator.SetBool("DownChop", true);
        }
        if (currentInteractable == null)
        {
            animator.SetBool("DownChop", false);
            Task.current.Succeed();
        }
    }
    [Task]
    public void TriggerPickUpAnimation()
    {
        animator.SetTrigger("Interact");
        Task.current.Succeed();
    }
    [Task]
    public void CollectHarvestedObject()
    {
        twoSecs -= Time.deltaTime;
        if (twoSecs <= 0)
        {
            twoSecs = 2f;
            Task.current.Succeed();
        }
    }
    [Task]
    public void DropLog()
    {
        StoreAllItemsOfType("processed_tree");
        Task.current.Succeed(); 
    }
    [Task]
    public void WaitForTreeToFall()
    {
        twoSecs -= Time.deltaTime;
        if (twoSecs <= 0)
        {
            twoSecs = 2f;
            Task.current.Succeed();
        }
    }
    #endregion

    #region harvest helper methods
    public void FindClosestHarvestObject(List<GameObject> listOfHarvestObjects, string cannotPerform)
    {
        if (listOfHarvestObjects.Count > 0)
        {
            for (int i = 0; i < listOfHarvestObjects.Count; i++)
            {
                if (Vector3.Distance(transform.position, listOfHarvestObjects[i].transform.position) <
                    Vector3.Distance(transform.position, closestHarvestObject))
                {
                    currentInteractable = listOfHarvestObjects[i];
                    closestHarvestObject = listOfHarvestObjects[i].GetComponent<HarvestObject>().interactionPoint;
                }
            }
            target = closestHarvestObject;
            PathRequestManager.RequestPath(transform.position, target, OnPathFound);
            pathReached = false;
            Task.current.Succeed();
        }
        else
        {
            InfoPanel.instance.infoText.text = cannotPerform;
            SelectionManager.instance.MakeAllJobBoolsFalse();
            isIdle = true;
            Task.current.Fail();
        }
    }

    public void AxeChop()
    {
        currentInteractable.GetComponent<HarvestObject>().harvestObjectHealth--;
        currentInteractable.GetComponent<HarvestObject>().audioSource.clip = currentInteractable.GetComponent<Tree>().harvestSounds[Random.Range(0, 5)];
        currentInteractable.GetComponent<HarvestObject>().audioSource.Play();
        if (currentInteractable.GetComponent<HarvestObject>().harvestObjectHealth == 0)
        {
            currentInteractable.GetComponent<HarvestObject>().harvestObjectDestroyed = true;
        }
    }

    //this is found in the events in the 'ProcessTree' animation file. 
    public void AxeDownChop()
    {
        currentInteractable.GetComponent<HarvestObject>().destroyedObjectHealth--;
        currentInteractable.GetComponent<HarvestObject>().audioSource.clip = currentInteractable.GetComponent<Tree>().harvestSounds[Random.Range(0, 5)];
        currentInteractable.GetComponent<HarvestObject>().audioSource.Play();
        if (currentInteractable.GetComponent<HarvestObject>().destroyedObjectHealth == 0)
        {
            Node nodeToScan = Grid.instance.NodeFromWorldPoint(currentInteractable.transform.position);
            nodeToScan.walkable = true;
            Destroy(currentInteractable);
            GameObject log = Instantiate(Resources.Load("processed_tree")) as GameObject;
            log.name = "processed_tree";
            log.transform.position = closestHarvestObject;
            objectToCollect = log;
        }
    }
    #endregion 

    #region idle tasks
    [Task]
    public void StandAround()
    {
        if (InteractablesManager.instance.stools.Count > 0)
        {
            Task.current.Fail();
        }
    }
    [Task]
    public void FindStool()
    {
        if (InteractablesManager.instance.stools.Count > 0)
        {
            FindInteractable(InteractablesManager.instance.stools, "stool");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
    [Task]
    public void SitOnStool()
    {
        animator.SetBool("Sitting", true);

    }
    #endregion

    #region gear and inventory tasks
    [Task]
    public void SetDestinationToNearbyEquipmentStorage(string equipmentType)
    {
        if (equipmentType == "pants")
        {
            FindInteractable(InteractablesManager.instance.chestOfDrawers, "You expect me to work without pants!?");
        }
        else if (equipmentType == "axe")
        {
            FindInteractable(InteractablesManager.instance.toolRacks, "I can't chop a tree down without an axe!");
        }
        else if (equipmentType == "wood")
        {
            FindInteractable(InteractablesManager.instance.woodStockpiles, "There is nowhere to store this log!");
        }
    }
    [Task]
    public void CheckGear(string equipmentSlug, int equipmentIndex)//string pantsSlug, string topSlug, string righHandSlug)
    {
        if (equipmentScript.equippedItems[equipmentIndex].Slug == equipmentSlug)
        {
            hasRightGear = true;
            Task.current.Succeed();
        }
        else if (equipmentScript.equippedItems[equipmentIndex].Slug != equipmentSlug)
        {
            hasRightGear = false;
            Task.current.Fail();
        }
    }
    [Task]
    public void TakeClothingOrEquipmentAndEquip(string itemType)
    {
        if (currentInteractable.GetComponent<Storage>().storedItems.Count > 0)
        {
            for (int i = 0; i < equipmentScript.equippedItems.Count; i++)
            {
                if (equipmentScript.equippedItems[i].ItemType == itemType)
                {
                    equipmentScript.equippedItems[i] = currentInteractable.GetComponent<Storage>().storedItems[0];
                    currentInteractable.GetComponent<Storage>().storedItems.Remove(currentInteractable.GetComponent<Storage>().storedItems[0]);
                    equipmentScript.addEquipment = true;
                    equipmentScript.AddEquipment(equipmentScript.equippedItems[i]);
                    break;
                }
            }
            hasRightGear = true;
            Task.current.Succeed();
        }
        else
        {
            InfoPanel.instance.infoText.text = "I refuse to work without " + itemType + "!";
            SelectionManager.instance.MakeAllJobBoolsFalse();
            isIdle = true;
            Task.current.Fail();
        }
    }
    #endregion

    #region gear and inventory helper methods
    private void FindInteractable(List<GameObject> storageUnitList, string failStatement)
    {
        if (storageUnitList.Count > 0)
        {
            GameObject storage = storageUnitList[0];
            target = storage.GetComponent<DynamicObstacle>().interactionPoint.transform.position;
            currentInteractable = storage;
            PathRequestManager.RequestPath(transform.position, target, OnPathFound);
            pathReached = false;
            Task.current.Succeed();
        }
        else
        {
            InfoPanel.instance.infoText.text = failStatement;
            SelectionManager.instance.MakeAllJobBoolsFalse();
            isIdle = true;
            Task.current.Fail();
        }
    }

    public void AddWorldItemToBackpack()
    {
        Item itemToAddToBackpack = ItemDatabase.instance.FetchItemBySlug(objectToCollect.name);

        backpack.Add(itemToAddToBackpack);

        Destroy(objectToCollect);
    }

    public void StoreAllItemsOfType(string typeOfItemToStore)
    {
        for (int i = 0; i < backpack.Count; i++)
        {
            if (backpack[i].Slug == typeOfItemToStore)
            {
                currentInteractable.GetComponent<Storage>().storedItems.Add(backpack[i]);
                currentInteractable.GetComponent<Storage>().addingItem = true;
                backpack.Remove(backpack[i]);
            }
        }
    }

    public void EquipItem(string itemType, string itemSlug)
    {
        for (int i = 0; i < equipmentScript.equippedItems.Count; i++)
        {
            if (equipmentScript.equippedItems[i].ItemType == itemType)
            {
                equipmentScript.equippedItems[i] = ItemDatabase.instance.FetchItemBySlug(itemSlug); 
                equipmentScript.addEquipment = true;
                equipmentScript.AddEquipment(equipmentScript.equippedItems[i]);
                break;
            }
        }
    }
    #endregion

}
