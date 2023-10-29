using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Default,
    Dead,
    Slow,
    Slide,
}

public class TileAction : MonoBehaviour
{
    public TileType currentTile = 0;

    [SerializeField] private Collider Player;
    [SerializeField] private Collider Meteo;
    [SerializeField] private MeshRenderer TileMat;
    [SerializeField] private Material[] Mats = new Material[4];
    [SerializeField] private MeshCollider TilePhysicMat;
    [SerializeField] private PhysicMaterial[] PhysicMats = new PhysicMaterial[3];

    [SerializeField] private int TimeMin = 3;
    [SerializeField] private int TimeMax = 5;
    private float timeFlow = 0;

    [SerializeField] private GameObject ItemShieldPrefab;
    [SerializeField] private GameObject ItemRemoverPrefab;
    [SerializeField] private Vector3 itemPositionOffset = Vector3.up * 2;

    private void Start()
    {
        TryGetComponent(out Player);
        TryGetComponent(out Meteo);
        TryGetComponent(out TileMat);
        TryGetComponent(out TilePhysicMat);
    }

    //private void Update()
    //{
    //    //����Ÿ�� ��� ����
    //    Invoke(currentTile.ToString(), 0);
    //}

    private void ChangeTile(string tagname)
    {
        switch (tagname)
        {
            case "Slow":
                currentTile = TileType.Slow;
                TileMat.material = Mats[2];
                TilePhysicMat.material = PhysicMats[1];
                TimeCheck();
                break;
            case "Dead":
                currentTile = TileType.Dead;
                TileMat.material = Mats[1];
                TimeCheck();
                break;
            case "Slide":
                currentTile = TileType.Slide;
                TileMat.material = Mats[3];
                TilePhysicMat.material = PhysicMats[2];
                TimeCheck();
                break;
        }
    }

    //private void Default()
    //{
    //    TileMat.material = Mats[0];
    //    TilePhysicMat.material = PhysicMats[0];
    //}

    //private void Dead()
    //{
    //    TileMat.material = Mats[1];
    //    TimeCheck();
    //}

    //private void Slow()
    //{
    //    TileMat.material = Mats[2];
    //    TilePhysicMat.material = PhysicMats[1];
    //    TimeCheck();
    //}

    //private void Slide()
    //{
    //    TileMat.material = Mats[3];
    //    TilePhysicMat.material = PhysicMats[2];
    //    TimeCheck();
    //}

    private void OnCollisionEnter(Collision collision)
    {
        //�浹�� Ÿ�Ϻ�ȯ
        if (collision.collider.tag != "Player")
        {
            //�̹� Ÿ���� ��ȯ������ ���� ���׿��� ���������
            if (currentTile.ToString() != TileType.Default.ToString())
            {
                timeFlow = 0;
            }
            ChangeTile(collision.collider.tag);
        }
	}

	private void OnCollisionStay(Collision collision)
	{
        // OnCollisionEnter�� ���
        // �÷��̾ �̹� Tile�� �ö� ���¿��� Tile�� ���� �Ǹ� ������� ����
        if (currentTile == TileType.Dead && collision.collider.tag.Equals("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
			if (!playerController.IsInvincible)
			{
                collision.gameObject.GetComponent<PlayerController>().Die();
            }
        }
    }

    private IEnumerator currentTimer;
    private IEnumerator CheckTime()
	{
        float elapsdeTime = 0f;
        int Timer = Random.Range(TimeMin, TimeMax);
		while (elapsdeTime < Timer)
		{
            elapsdeTime += Time.deltaTime;
            yield return null;
		}

        // Ư�� Ÿ���̸� ����� �� ������ ����
		if (!currentTile.Equals(TileType.Default))
		{
            if (Random.Range(0, 2) == 0)
                Instantiate(ItemShieldPrefab, transform.position + itemPositionOffset, Quaternion.identity);
            else
                Instantiate(ItemRemoverPrefab, transform.position + itemPositionOffset, Quaternion.identity);
        }

        currentTile = TileType.Default;
        TileMat.material = Mats[0];
        TilePhysicMat.material = PhysicMats[0];
    }

    //Ÿ�̸��Լ�(�����ð� �� �⺻Ÿ�Ϸ� ��ȯ)
    private void TimeCheck()
    {
		//timeFlow += Time.deltaTime;   
		//if (timeFlow >= Timer)
		//{
		//    currentTile = TileType.Default;
		//    timeFlow = 0;
		//}

		if (currentTimer != null)
            StopCoroutine(currentTimer);

        currentTimer = CheckTime();
        StartCoroutine(currentTimer);
    }
}
