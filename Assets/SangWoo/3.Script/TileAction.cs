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

    [SerializeField] private int Timer = 3;
    private float timeFlow = 0;


    private void Start()
    {
        TryGetComponent(out Player);
        TryGetComponent(out Meteo);
        TryGetComponent(out TileMat);
        TryGetComponent(out TilePhysicMat);
    }

    private void Update()
    {
        //현재타일 모드 실행
        Invoke(currentTile.ToString(), 0);
    }

    private void ChangeTile(string tagname)
    {
        switch (tagname)
        {
            case "Slow":
                currentTile = TileType.Slow;
                break;
            case "Dead":
                currentTile = TileType.Dead;
                break;
            case "Slide":
                currentTile = TileType.Slide;
                break;
        }
    }

    private void Default()
    {
        TileMat.material = Mats[0];
        TilePhysicMat.material = PhysicMats[0];
    }

    private void Dead()
    {
        TileMat.material = Mats[1];
        TimeCheck();
    }

    private void Slow()
    {
        TileMat.material = Mats[2];
        TilePhysicMat.material = PhysicMats[1];
        TimeCheck();
    }

    private void Slide()
    {
        TileMat.material = Mats[3];
        TilePhysicMat.material = PhysicMats[2];
        TimeCheck();
    }


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
            //**메테오 없애줭
        }

        ////Dead : 플레이어 사망
        //if (currentTile == TileType.Dead && collision.collider.tag.Equals("Player"))
        //{
        //    //**플레이어 사망처리 해줭
        //}

        ////Slow : 마찰력 증가
        //if (currentTile == TileType.Slow && collision.collider.tag.Equals("Player"))
        //{
        //    Friction.material.dynamicFriction = frictionForce;
        //}

        ////Slide : 마찰력 zero
        //if (currentTile == TileType.Slide && collision.collider.tag.Equals("Player"))
        //{
        //    Friction.material.dynamicFriction = 0;
        //    //**플레이어 조작 불가 해줭
        //}
    }

    //타이머함수(일정시간 후 기본타일로 변환)
    private void TimeCheck()
    {
        timeFlow += Time.deltaTime;
        if (timeFlow >= Timer)
        {
            currentTile = TileType.Default;
            timeFlow = 0;
            //if (Friction.material.dynamicFriction != 0.6f)
            //{
            //    Friction.material.dynamicFriction = 0.6f;
            //}
        }
    }
}
