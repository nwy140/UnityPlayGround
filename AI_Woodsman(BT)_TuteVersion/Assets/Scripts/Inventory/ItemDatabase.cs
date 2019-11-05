using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();
    public static ItemDatabase instance; 
    private void Awake()
    {
        //naked
        itemList.Add(new Item(101, "Naked Chest", "Bare chest", "naked_chest", false, "Chest", "naked"));
        itemList.Add(new Item(102, "Naked Legs", "Bare legs", "naked_legs", false, "Legs", "naked"));
        itemList.Add(new Item(103, "Naked Head", "Bare head", "naked_head", false, "Head", "naked"));
        itemList.Add(new Item(104, "Empty Right Hand", "Holding nothing in right hand", "empty_handR", false, "HandRight", "naked"));
        //gear
        itemList.Add(new Item(0, "Steel Cuirass", "Standard Cuirass", "cuirass", false, "Chest", (GameObject)Resources.Load("Gear/cuirass"), "combat"));
        itemList.Add(new Item(1, "Black Steel Cuirass", "Rare cuirass", "black_cuirass", false, "Chest", (GameObject)Resources.Load("Gear/black_cuirass"), "combat"));
        itemList.Add(new Item(2, "Brown Pants", "Brown cotton pants", "brown_pants", false, "Legs", (GameObject)Resources.Load("Gear/brown_pants"), "work"));
        itemList.Add(new Item(3, "White Pants", "White cotton pants", "white_pants", false, "Legs", (GameObject)Resources.Load("Gear/white_pants"), "work"));
        itemList.Add(new Item(4, "Steel Helmet", "Standard helmet", "helmet", false, "Head", (GameObject)Resources.Load("Gear/helmet"), "combat"));
        itemList.Add(new Item(5, "Black Steel Helmet", "Rare Helmet", "black_helmet", false, "Head", (GameObject)Resources.Load("Gear/black_helmet"), "combat"));
        //weapons
        itemList.Add(new Item(6, "Axe", "Woodcutter's axe", "axe", false, "HandRight", (GameObject)Resources.Load("Gear/axe")));
        //world item
        itemList.Add(new Item(400, "Log", "Log from a felled tree", "processed_tree"));
        instance = this; 
    }

    public Item FetchItemByID(int id)
    {
        for (int i = 0; i < itemList.Count; i++)
        {

            if (itemList[i].ItemID == id)
            {
                return itemList[i];
            }
        }

        return null;
    }

    public Item FetchItemBySlug(string slugName)
    {
        for (int i = 0; i < itemList.Count; i++)
        {

            if (itemList[i].Slug == slugName)
            {
                return itemList[i];
            }
        }

        return null;
    }


}
