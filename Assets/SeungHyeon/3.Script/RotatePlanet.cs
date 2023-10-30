using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(50f * Time.deltaTime, 0, 0));
    }
}
