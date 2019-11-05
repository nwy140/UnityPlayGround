using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : HarvestObject
{

    private void Start()
    {
        harvestSounds = new AudioClip[5];
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        destroyedObjectHealth = 5; 
        rigidBody.detectCollisions = false;
        rigidBody.useGravity = false; 
        for(int i = 0; i < harvestSounds.Length; i++)
        {
            harvestSounds[i] = Resources.Load("SFX/WoodChop/wood_chop_" + i.ToString()) as AudioClip;
        }
        Node node = Grid.instance.NodeFromWorldPoint(transform.position);
        node.walkable = false; 
        Vector3 interPoint = transform.position;
        interPoint.z += 1;
        interactionPoint = interPoint;
        gameObject.name = "tree";
        InteractablesManager.instance.trees.Add(gameObject);

        harvestObjectHealth = 5; 
    }

    private void Update()
    {
        if(harvestObjectDestroyed)
        {
            TreeFall(); 
        }
    }

    public void TreeFall()
    {
        rigidBody.detectCollisions = true;
        rigidBody.useGravity = true;
        GetComponent<Collider>().isTrigger = false; 
        rigidBody.AddForce(-transform.forward * 100f);
        audioSource.clip = Resources.Load("SFX/WoodChop/tree_fall_wood_0") as AudioClip;
        audioSource.Play();
        InteractablesManager.instance.trees.Remove(gameObject);
        harvestObjectDestroyed = false; 
    }


}