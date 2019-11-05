using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : HarvestObject
{

    private void Start()
    {
        harvestSounds = new AudioClip[5];
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        destroyedObjectHealth = 5;
        rigidBody.detectCollisions = false;
        rigidBody.useGravity = false;
        for (int i = 0; i < harvestSounds.Length; i++)
        {
            harvestSounds[i] = Resources.Load("SFX/Stone/rock_hit_" + i.ToString()) as AudioClip;
        }
        Node node = Grid.instance.NodeFromWorldPoint(transform.position);
        node.walkable = false;
        Vector3 interPoint = transform.position;
        interPoint.z += 2;
        interactionPoint = interPoint;
        gameObject.name = "boulder";
        InteractablesManager.instance.boulders.Add(gameObject);

        harvestObjectHealth = 5;
    }

    private void Update()
    {
        if (harvestObjectDestroyed)
        {
            RockSmash();
        }
    }

    public void RockSmash()
    {
        //rigidBody.detectCollisions = true;
       // rigidBody.useGravity = true;
        GetComponent<Collider>().isTrigger = false;
       // rigidBody.AddForce(-transform.forward * 100f);
        audioSource.clip = Resources.Load("SFX/Stone/boulder_destroy") as AudioClip;
        audioSource.Play();
        InteractablesManager.instance.boulders.Remove(gameObject);
        harvestObjectDestroyed = false;
    }


}