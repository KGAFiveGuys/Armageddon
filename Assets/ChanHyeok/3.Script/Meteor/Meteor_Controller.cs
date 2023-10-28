using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor_Controller : MonoBehaviour
{
    private Rigidbody meteor_r;
    [SerializeField] private float FallingSpeed;
    [SerializeField] private int Wave = 1; //���߿� ���ӸŴ������� �޾ƿ���
    private void Start()
    {
        meteor_r = GetComponent<Rigidbody>();
        
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
        int layer = col.gameObject.layer;
        
        if (gameObject.activeSelf && layer == LayerMask.NameToLayer("Ground"))
        {
            if (gameObject.CompareTag("Meteor"))
            {
                Debug.Log("�⺻ ���׿� �浹");
                Meteor_Pooling.instance.ReturnToQueue(gameObject);
                DestroySound.instance.PlayDestroySound(0);
            }
            else if(gameObject.CompareTag("Dead")|| gameObject.CompareTag("Slide")|| gameObject.CompareTag("Slow"))
            {
                Debug.Log("Ư�� ���׿� �浹");
                Destroy(gameObject);
                DestroySound.instance.PlayDestroySound(1);
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
        else if (tag.Equals("Dead")|| tag.Equals("Slide")|| tag.Equals("Slow"))
        {
            FallingSpeed = 100f;
        }
    }

    
}
