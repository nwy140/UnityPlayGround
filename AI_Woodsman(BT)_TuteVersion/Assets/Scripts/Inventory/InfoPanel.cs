using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    //singleton 
    public static InfoPanel instance;
    //gameobjects
    public Text infoText; 

    private void Start()
    {
        infoText = transform.Find("Text").GetComponent<Text>(); 
        instance = this; 
    }
}
