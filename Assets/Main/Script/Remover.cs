using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class Remover : MonoBehaviour, IGameItem
{
	private SphereCollider collider;
	private MeshRenderer renderer;

	private void Awake()
	{
		TryGetComponent(out collider);
		TryGetComponent(out renderer);
	}

	public void TriggerItemEffect()
	{
		collider.enabled = false;
		renderer.enabled = false;

		// 메테오 전부 삭제
		foreach (var meteor in FindObjectsOfType<Meteor_Controller>())
		{
			if (meteor.gameObject.CompareTag("Meteor"))
				Meteor_Pooling.instance.ReturnToQueue(meteor.gameObject);
			else
				Destroy(meteor.gameObject);
		} 

		Destroy(gameObject);
	}
}
