using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor_Movement : MonoBehaviour
{
    private Rigidbody meteor_r;
    [SerializeField] private float FallingSpeed;
    [SerializeField] private int Wave = 1; //나중에 게임매니저에서 받아오기
    private void Start()
    {
        meteor_r = GetComponent<Rigidbody>();
        SettingSpeed();
    }

    private void FixedUpdate()
    {
        Falling();
    }

    private void Falling()
    {
        meteor_r.AddForce(Vector3.down * FallingSpeed * Time.deltaTime);
    }

    private void SettingSpeed()
    {
        if (name.Equals("Meteor_Defalut"))
        {
            FallingSpeed = 150f + (50 * Wave);
        }
        else if (name.Equals("Meteor_Dead")|| name.Equals("Meteor_Slide")|| name.Equals("Meteor_Slow"))
        {
            FallingSpeed = 100f;
        }
    }
}
