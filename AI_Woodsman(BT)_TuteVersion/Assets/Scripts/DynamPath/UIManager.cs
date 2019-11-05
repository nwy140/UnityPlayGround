using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //lists
    public List<GameObject> buildOptions = new List<GameObject>();
    public List<GameObject> jobOptions = new List<GameObject>();
    //bools
    public bool activateBuildButtons;
    public bool activateJobOptions;
    public bool changeButtons; 
    //singleton
    public static UIManager instance; 

    private void Start()
    {
        changeButtons = true; 
        instance = this; 
    }

    private void Update()
    {
        if(changeButtons)
        {
            ActivateBuildOptions();
            ActivateJobOptions();
        }
    }

    public void ActivateBuildOptions()
    {
        if(activateBuildButtons)
        {
            for(int i = 0; i < buildOptions.Count; i++)
            {
                buildOptions[i].SetActive(true); 
            }
        }
        else if (!activateBuildButtons)
        {
            for (int i = 0; i < buildOptions.Count; i++)
            {
                buildOptions[i].SetActive(false);
            }
        }
        changeButtons = false;
    }

    public void ActivateJobOptions()
    {
        if (activateJobOptions)
        {
            for (int i = 0; i < jobOptions.Count; i++)
            {
                jobOptions[i].SetActive(true);
            }
        }
        else if (!activateJobOptions)
        {
            for (int i = 0; i < jobOptions.Count; i++)
            {
                jobOptions[i].SetActive(false);
            }
        }
        changeButtons = false; 
    }

}
