using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSense : MonoBehaviour
{
    private Tree treeScript; 

    private void Start()
    {
        treeScript = transform.parent.GetComponent<Tree>(); 
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            treeScript.audioSource.clip = Resources.Load("SFX/WoodChop/tree_fall_leaves_0") as AudioClip;
            treeScript.audioSource.Play(); 
        }
    }
}
