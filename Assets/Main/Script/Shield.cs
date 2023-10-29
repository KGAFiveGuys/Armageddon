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

	private IEnumerator currentShield;
	public void TriggerItemEffect()
	{
		collider.enabled = false;
		renderer.enabled = false;

		if (currentShield != null)
			StopCoroutine(currentShield);

		currentShield = SetInvincible(duration);
		StartCoroutine(currentShield);
	}

	[SerializeField] private float elapsedTime;
	private IEnumerator SetInvincible(float duration)
	{
		playerController.IsInvincible = true;
		playerController.ToggleShield(true);

		elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		playerController.ToggleShield(false);
		playerController.IsInvincible = false;
		Destroy(gameObject);
	}
}
