using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class Shield : MonoBehaviour, IGameItem
{
	private PlayerController playerController;
	[SerializeField] private float duration = 3f;

	private SphereCollider collider;
	private MeshRenderer renderer;

	private void Awake()
	{
		TryGetComponent(out collider);
		TryGetComponent(out renderer);

		playerController = FindObjectOfType<PlayerController>();
	}

	public void TriggerItemEffect()
	{
		playerController.ToggleShield(duration);
		Destroy(gameObject);
	}
}
