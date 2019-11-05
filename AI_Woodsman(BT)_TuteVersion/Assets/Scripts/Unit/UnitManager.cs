using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;
    //lists
    public List<GameObject> movingUnits = new List<GameObject>();
    public List<GameObject> units = new List<GameObject>();
    //bools
    public bool buildMode;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Unit").Length; i++)
        {
            units.Add(GameObject.FindGameObjectsWithTag("Unit")[i]);
        }
    }




}
