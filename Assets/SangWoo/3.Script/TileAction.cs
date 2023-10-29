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
    //    //현재타일 모드 실행
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
        //충돌시 타일변환
        if (collision.collider.tag != "Player")
        {
            //이미 타일이 변환됬을때 같은 메테오가 떨어질경우
            if (currentTile.ToString() != TileType.Default.ToString())
            {
                timeFlow = 0;
            }
            ChangeTile(collision.collider.tag);
        }
	}

	private void OnCollisionStay(Collision collision)
	{
        // OnCollisionEnter의 경우
        // 플레이어가 이미 Tile에 올라간 상태에서 Tile이 변경 되면 적용되지 않음
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

        // 특수 타일이면 사라질 때 아이템 생성
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

    //타이머함수(일정시간 후 기본타일로 변환)
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
