using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VibrationType
{
    Default,
    Special,
}

[System.Serializable]
public class Explosion
{
    [field:SerializeField] public VibrationType VibrationType { get; set; }
    [field: SerializeField] public float Duration { get; set; }
    [field: SerializeField] public AnimationCurve IntensityCurve { get; set; }
}

public class Meteor_Controller : MonoBehaviour
{
    [SerializeField] private Explosion explosion;

    private PlayerController playerController;
    private Rigidbody meteor_r;
    [SerializeField] private float FallingSpeed;
    [SerializeField] private int Wave = 1; //���߿� ���ӸŴ������� �޾ƿ���

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
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
                SFX_Audio.instance.Play_BoomDestory_Sound();
                DestroySound.instance.PlayDestroySound(0);
                Debug.Log("�⺻ ���׿� �浹");
                Meteor_Pooling.instance.ReturnToQueue(gameObject);
                

				if (playerController != null)
                    playerController.TriggerVibration(explosion);
            }
            else if(gameObject.CompareTag("Dead")|| gameObject.CompareTag("Slide")|| gameObject.CompareTag("Slow"))
            {
                SFX_Audio.instance.Play_BoomDestory_special_Sound();
                DestroySound.instance.PlayDestroySound(1);
                Debug.Log("Ư�� ���׿� �浹");
                Destroy(gameObject);

                if (playerController != null)
                    playerController.TriggerVibration(explosion);
            }
        }
        else if (gameObject.activeSelf && col.gameObject.CompareTag("Player"))
        {
            // �����̸� ������� �ʰ� ���׿� ����
            if (playerController.IsInvincible)
			{
                Destroy(gameObject);
                return;
            }

            col.gameObject.GetComponent<PlayerController>().Die();

            if (gameObject.CompareTag("Meteor"))
            {
                DestroySound.instance.PlayDestroySound(0);
                Meteor_Pooling.instance.ReturnToQueue(gameObject);
                Debug.Log("�÷��̾� �⺻ ���׿� �浹");
            }
            else if (gameObject.CompareTag("Dead") || gameObject.CompareTag("Slide") || gameObject.CompareTag("Slow"))
            {
                DestroySound.instance.PlayDestroySound(1);
                Destroy(gameObject);
                Debug.Log("�÷��̾� Ư�� ���׿� �浹");
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
