using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor_Controller : MonoBehaviour
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
                DestroySound.instance.PlayDestroySound(0);
                Debug.Log("기본 메테오 충돌");
                Meteor_Pooling.instance.ReturnToQueue(gameObject);
            }
            else if(gameObject.CompareTag("Dead")|| gameObject.CompareTag("Slide")|| gameObject.CompareTag("Slow"))
            {
                DestroySound.instance.PlayDestroySound(1);
                Debug.Log("특수 메테오 충돌");
                Destroy(gameObject);
            }
        }
        else if (gameObject.activeSelf && col.gameObject.CompareTag("Player"))
        {
            
            Destroy(col.gameObject); // 플레이어 다이메소드로 바꿔주세요

            if (gameObject.CompareTag("Meteor"))
            {
                DestroySound.instance.PlayDestroySound(0);
                Meteor_Pooling.instance.ReturnToQueue(gameObject);
                Debug.Log("플레이어 기본 메테오 충돌");
            }
            else if (gameObject.CompareTag("Dead") || gameObject.CompareTag("Slide") || gameObject.CompareTag("Slow"))
            {
                DestroySound.instance.PlayDestroySound(1);
                Destroy(gameObject);
                Debug.Log("플레이어 특수 메테오 충돌");
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
