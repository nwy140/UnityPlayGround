//this is an extended version of GameGrind's Item class. The original can be found here: https://www.youtube.com/watch?v=x24t4DCxGh8&list=PLivfKP2ufIK78r7nzfpIEH89Nlnb__RRG&index=2
using UnityEngine;

[System.Serializable]
public class Item
{
    public int ItemID;

    public string ItemName;
    public string ItemDescription;
    public string Slug;
    public string ItemType;
    public GameObject ItemPrefab;

    public string ClothingType; //work, combat, social, naked 

    public bool Stackable;

    public Sprite ItemIcon;

    //constructor for no equipment
    public Item(int itemID, string itemName, string itemDescription, string slug, bool stackable, string itemType, string clothingType)
    {
        this.ItemID = itemID;
        this.ItemName = itemName;
        this.ItemDescription = itemDescription;
        this.Slug = slug;
        this.Stackable = stackable;
        this.ItemType = itemType;
        this.ClothingType = clothingType; 
    }

    //constructor for world item
    public Item(int itemID, string itemName, string itemDescription, string slug)
    {
        this.ItemID = itemID;
        this.ItemName = itemName;
        this.ItemDescription = itemDescription;
        this.Slug = slug;
    }

    //constructor for clothing/armor
    public Item(int itemID, string itemName, string itemDescription, string slug, bool stackable, string itemType, GameObject itemPrefab, string clothingType)
    {
        this.ItemID = itemID;

        this.ItemName = itemName;
        this.ItemDescription = itemDescription;
        this.Slug = slug;

        this.Stackable = stackable;

        this.ItemIcon = Resources.Load<Sprite>("Icons/" + slug);
        this.ClothingType = clothingType; 
        this.ItemType = itemType;
        this.ItemPrefab = itemPrefab;
    }

    //constructor for weapons
    public Item(int itemID, string itemName, string itemDescription, string slug, bool stackable, string itemType, GameObject itemPrefab)
    {
        this.ItemID = itemID;

        this.ItemName = itemName;
        this.ItemDescription = itemDescription;
        this.Slug = slug;

        this.Stackable = stackable;

        this.ItemIcon = Resources.Load<Sprite>("Icons/" + slug);

        this.ItemType = itemType;
        this.ItemPrefab = itemPrefab;
    }

    public Item()
    {
        this.ItemID = -1;
    }

}