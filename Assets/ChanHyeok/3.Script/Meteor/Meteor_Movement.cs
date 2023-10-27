using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor_Movement : MonoBehaviour
{
    private Rigidbody meteor_r;
    private SphereCollider meteor_col;
    [SerializeField] private float FallingSpeed;
    [SerializeField] private int Wave = 1; //���߿� ���ӸŴ������� �޾ƿ���
    private void Start()
    {
        meteor_r = GetComponent<Rigidbody>();
        meteor_col = GetComponent<SphereCollider>();
        
        SettingSpeed();
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            Falling();
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        
        if (gameObject.activeSelf)
        {
            if (gameObject.CompareTag("Meteor"))
            {
                Debug.Log("a");
                Meteor_Pooling.instance.ReturnToQueue(gameObject);
            }
        }
    }
    private void Falling()
    {
        meteor_r.AddForce(Vector3.down * FallingSpeed * Time.deltaTime);
    }

    private void SettingSpeed()
    {
        if (tag == "Meteor")
        {
            FallingSpeed = 150f + (100f * Wave);
        }
        else if (name.Equals("Meteor_Dead")|| name.Equals("Meteor_Slide")|| name.Equals("Meteor_Slow"))
        {
            FallingSpeed = 100f;
        }
    }
}
