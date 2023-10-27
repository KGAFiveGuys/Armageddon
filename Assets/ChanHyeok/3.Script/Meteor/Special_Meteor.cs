using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special_Meteor : MonoBehaviour
{
    [Header("생성위치 지정")]
    [SerializeField] private float Spawn_minX;
    [SerializeField] private float Spawn_maxX;
    [SerializeField] private float Spawn_minZ;
    [SerializeField] private float Spawn_maxZ;
    [SerializeField] private float Spawn_Y = 15f;
    private Vector3 Spawn_position;

    [SerializeField] private GameObject[] Meteors;


    private void Start()
    {
        StartCoroutine(Spawn_Special_Meteor());
    }

    private GameObject CreateSpecialMeteor()
    {
        int index = Random.Range(0, 3); // 0=Dead,1=Slide,2=Slow

        float X = Random.Range(Spawn_minX, Spawn_maxX);
        float Z = Random.Range(Spawn_minZ, Spawn_maxZ);
        Spawn_position = new Vector3(X, Spawn_Y, Z);

        GameObject special = Instantiate(Meteors[index],Spawn_position,Quaternion.identity);
        special.transform.SetParent(gameObject.transform);
        return special;
    }

    private IEnumerator Spawn_Special_Meteor()
    {
        while (true)
        {
            CreateSpecialMeteor();
            int delay = Random.Range(5, 11);
            yield return new WaitForSeconds(delay);
        }
    }

}
