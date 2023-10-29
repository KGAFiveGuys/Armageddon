using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestory : MonoBehaviour
{
	private float delayTime = 5f;
	private float originScaleX;
	private float originScaleY;
	private float originScaleZ;

	private void Awake()
	{
		originScaleX = transform.localScale.x;
		originScaleY = transform.localScale.y;
		originScaleZ = transform.localScale.z;
	}

	private void OnEnable()
	{
		StartCoroutine(Destroy(delayTime));
	}

	private IEnumerator Destroy(float delayTime)
	{
		float elapsedTime = 0f;
		float currentScaleX = 0f;
		float currentScaleY = 0f;
		float currentScaleZ = 0f;
		while (elapsedTime < delayTime)
		{
			elapsedTime += Time.deltaTime;

			currentScaleX = originScaleX * (1 - (elapsedTime / delayTime) / 2);
			currentScaleY = originScaleY * (1 - (elapsedTime / delayTime) / 2);
			currentScaleZ = originScaleZ * (1 - (elapsedTime / delayTime) / 2);
			transform.localScale = new Vector3(currentScaleX, currentScaleY, currentScaleZ);

			yield return null;
		}
		Destroy(gameObject);
	}
}
