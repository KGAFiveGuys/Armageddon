using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoyObject : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
