using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : DynamicObstacle
{
    public List<GameObject> storedItemGOs = new List<GameObject>();
    public List<Item> storedItems = new List<Item>();
    public bool addingItem = false;
    public bool removingItem = false; 
    public int itemCount;
    public int itemMax;
    public string itemType; 

    private void Start()
    {
    }

    private void Update()
    {
        AddingItem();
        RemovingItem();

    }

    public void CreateItemList(string item)
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains(item))
                storedItemGOs.Add(child.gameObject);
        }
        itemCount = 0;
        itemMax = storedItemGOs.Count;
    }

   public void AddingItem()
    {
        if (addingItem && !(itemCount >= itemMax))
        {
            for (int i = 0; i < storedItemGOs.Count; i++)
            {
                if (storedItemGOs[i].activeSelf == false)
                {
                    storedItemGOs[i].SetActive(true);
                    itemCount++;
                    break;
                }
            }
            addingItem = false;
        }
    }

    public void RemovingItem()
    {
        if (removingItem && itemCount != 0)
        {
            for (int i = storedItemGOs.Count - 1; i > -1; i--)
            {
                if (storedItemGOs[i].activeSelf == true)
                {
                    storedItemGOs[i].SetActive(false);
                    itemCount--;
                    break;
                }
            }
            removingItem = false;
        }
    }


}
