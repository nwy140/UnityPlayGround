using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JobAndBuildUI : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(BuildingPlacement.instance.currentBuilding == null && GameObjectIsBuildOption())
        {
            for(int i = 0; i < BuildingPlacement.instance.buildings.Count; i++)
            {
                if (gameObject.name == BuildingPlacement.instance.buildings[i].name)
                {
                    BuildingPlacement.instance.currentBuilding = Instantiate(BuildingPlacement.instance.buildings[i]);
                    UnitManager.instance.buildMode = true; 
                }
            }
        }
        else if(gameObject.name == "Build")
        {
            UIManager.instance.activateBuildButtons = !UIManager.instance.activateBuildButtons;
            UIManager.instance.activateJobOptions = false;
            UIManager.instance.changeButtons = true;
        }
        else if (gameObject.name == "Job")
        {
            UIManager.instance.activateJobOptions = !UIManager.instance.activateJobOptions;
            UIManager.instance.activateBuildButtons = false;
            UIManager.instance.changeButtons = true;
        }
        if(SelectionManager.instance.currentlySelectedUnits.Count > 0 && GameObjectIsJobOption())
        {
            if (gameObject.name == "woodcutter")
            {
                for (int i = 0; i < SelectionManager.instance.currentlySelectedUnits.Count; i++)
                {
                    SelectionManager.instance.MakeAllJobBoolsFalse();
                    SelectionManager.instance.currentlySelectedUnits[i].GetComponent<UnitTasks>().isWoodcutter = true;
                }
            }
            else if (gameObject.name == "miner")
            {
                for (int i = 0; i < SelectionManager.instance.currentlySelectedUnits.Count; i++)
                {
                    SelectionManager.instance.MakeAllJobBoolsFalse();
                    SelectionManager.instance.currentlySelectedUnits[i].GetComponent<UnitTasks>().isMiner = true;
                }
            }
        }
        else if (SelectionManager.instance.currentlySelectedUnits.Count == 0 && GameObjectIsJobOption())
        {
            InfoPanel.instance.infoText.text = "There are no selected units!\n(Left click or left click and drag over unit to select.)";
        }
    }

    #region JobAndBuildUI helper methods
    private bool GameObjectIsBuildOption()
    {
        for (int i = 0; i < BuildingPlacement.instance.buildings.Count; i++)
        {
            if (gameObject.name == BuildingPlacement.instance.buildings[i].name)
            {
                return true; 
            }
        }
        return false; 
    }

    private bool GameObjectIsJobOption()
    {
        for (int i = 0; i < UIManager.instance.jobOptions.Count; i++)
        {
            if (gameObject.name == UIManager.instance.jobOptions[i].name)
            {
                return true;
            }
        }
        return false;
    }
    #endregion 
}