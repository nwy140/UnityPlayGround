using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{
    //storage objects
    public List<GameObject> woodStockpiles = new List<GameObject>();
    public List<GameObject> armorStands = new List<GameObject>();
    public List<GameObject> toolRacks = new List<GameObject>();
    public List<GameObject> wardrobes = new List<GameObject>();
    public List<GameObject> chestOfDrawers = new List<GameObject>();
    //environment
    public List<GameObject> trees = new List<GameObject>();
    public List<GameObject> boulders = new List<GameObject>();
    //furniture
    public List<GameObject> stools = new List<GameObject>();

    public static InteractablesManager instance; 

    private void Awake()
    {
        instance = this; 
    }
}