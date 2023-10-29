using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderControl : MonoBehaviour
{
    [SerializeField]private GameObject UI_obj;
    private void Start()
    {
        UI_obj = GameObject.Find("Canvas");
        transform.SetParent(UI_obj.transform);
    }
}
