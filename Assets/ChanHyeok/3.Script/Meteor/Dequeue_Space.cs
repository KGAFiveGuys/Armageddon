using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dequeue_Space : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Meteor_Pooling.instance.Start_Armageddon());
    }

    private void Update()
    {
        Meteor_Pooling.instance.DeQueue();
    }
}
