using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Panda;
public class PlayGroundTasks : MonoBehaviour
{
    [Task]
    void DestroyGameObject() {
        Destroy(gameObject,0.1f);
    }
}
