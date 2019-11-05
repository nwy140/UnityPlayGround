using UnityEngine;
using System.Collections.Generic;

public class Equipment : MonoBehaviour
{
    #region Fields
    //gameObjects
    public GameObject avatar;
    public GameObject wornLegs;
    public GameObject wornChest;
    public GameObject wornHead;
    public GameObject wornHandRight; 
    //bools
    public bool addEquipment;
    public bool removeEquipment;
    //lists
    public List<Item> equippedItems = new List<Item>(); 
    //scripts
    private Stitcher stitcher;
    //ints
    private int totalEquipmentSlots; 
    #endregion

    #region Monobehaviour
    public void Awake()
    {
        stitcher = new Stitcher();
    }
    
    private void Start()
    {
        totalEquipmentSlots = 4; 

        for(int i = 0; i < totalEquipmentSlots; i++)
        {
            equippedItems.Add(new Item()); 
        }

        AddEquipmentToList(101);
        AddEquipmentToList(102);
        AddEquipmentToList(103);
        AddEquipmentToList(104);
    }

    public void AddEquipmentToList(int id)
    {
        for(int i = 0; i < equippedItems.Count; i++)
        {
            if(equippedItems[i].ItemID == -1)
            {
                equippedItems[i] = ItemDatabase.instance.FetchItemByID(id);
                break; 
            }
        }
    }

    public void AddEquipment(Item equipmentToAdd)
    {
        if (addEquipment)
        {
            if (equipmentToAdd.ItemType == "Chest")
            {
                wornChest = RemoveWorn(wornChest);
                wornChest = Wear(equipmentToAdd.ItemPrefab, wornChest);
                wornChest.name = equipmentToAdd.Slug; 
                addEquipment = false;
            }
            else if (equipmentToAdd.ItemType == "Legs")
            {
                wornLegs = RemoveWorn(wornLegs);
                wornLegs = Wear(equipmentToAdd.ItemPrefab, wornLegs);
                wornLegs.name = equipmentToAdd.Slug;
                addEquipment = false;
            }
            else if (equipmentToAdd.ItemType == "Head")
            {
                wornHead = RemoveWorn(wornHead);
                wornHead = Wear(equipmentToAdd.ItemPrefab, wornHead);
                wornHead.name = equipmentToAdd.Slug;
                addEquipment = false;
            }
            else if (equipmentToAdd.ItemType == "HandRight")
            {
                wornHandRight = RemoveWorn(wornHandRight);
                wornHandRight = Wear(equipmentToAdd.ItemPrefab, wornHandRight);
                wornHandRight.name = equipmentToAdd.Slug;
                addEquipment = false;
            }
        }
    }

    public void RemoveEquipment(Item equipmentToAdd)
    {
        if (removeEquipment)
        {
            if (equipmentToAdd.ItemType == "Chest")
            {
                wornChest = RemoveWorn(wornChest);
                removeEquipment = false;
            }
            else if (equipmentToAdd.ItemType == "Legs")
            {
                wornLegs = RemoveWorn(wornLegs);
                removeEquipment = false;
            }
            else if (equipmentToAdd.ItemType == "Head")
            {
                wornHead = RemoveWorn(wornHead);
                removeEquipment = false;
            }
            else if (equipmentToAdd.ItemType == "HandRight")
            {
                wornHandRight = RemoveWorn(wornHandRight);
                removeEquipment = false;
            }
        }
    }

    #endregion

    private GameObject RemoveWorn(GameObject wornClothing)
    {
        if (wornClothing == null)
            return null;
        GameObject.Destroy(wornClothing);
        return null; 
    }

    private GameObject Wear(GameObject clothing, GameObject wornClothing)
    {
        if (clothing == null)
            return null;
        clothing = (GameObject)GameObject.Instantiate(clothing);
        wornClothing = stitcher.Stitch(clothing, avatar);
        GameObject.Destroy(clothing);
        return wornClothing;
    }
}
