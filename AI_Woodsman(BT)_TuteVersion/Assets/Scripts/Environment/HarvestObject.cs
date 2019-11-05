using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestObject : MonoBehaviour
{
    //vector3
    public Vector3 interactionPoint;
    //bools 
    public bool harvestObjectDestroyed;
    //int
    public int harvestObjectHealth;
    public int destroyedObjectHealth;
    //components
    public AudioSource audioSource;
    public Rigidbody rigidBody;
    //audio clip
    public AudioClip[] harvestSounds;
}