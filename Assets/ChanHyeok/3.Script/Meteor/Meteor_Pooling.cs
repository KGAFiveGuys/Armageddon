using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor_Pooling : MonoBehaviour
{
    public static Meteor_Pooling instance = null;

    [Header("생성위치 지정")]
    [SerializeField] private float Spawn_minX;
    [SerializeField] private float Spawn_maxX;
    [SerializeField] private float Spawn_minZ;
    [SerializeField] private float Spawn_maxZ;
    [SerializeField] private float Spawn_Y = 20f;
    private Vector3 Spawn_position;

    [Header("생성 딜레이 설정")]
    [SerializeField] private float minDelay = 0.3f;
    [SerializeField] private float maxDelay = 2f;

    [Space(50f)]

    [SerializeField] private GameObject meteor_prefeb;

    [SerializeField] private int Wave = 1; //나중에 게임매니저에서 받아오기

    private Queue<GameObject> ObjectPool = new Queue<GameObject>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            for (int i = 0; i < 30; i++)
            {
                CreateMeteor();
            }
            StartCoroutine(Start_Armageddon());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private GameObject CreateMeteor()
    {
        float X = Random.Range(Spawn_minX, Spawn_maxX);
        float Z = Random.Range(Spawn_minZ, Spawn_maxZ);
        Spawn_position = new Vector3(X, Spawn_Y, Z);
        GameObject meteor = Instantiate(meteor_prefeb, Spawn_position, Quaternion.identity);
        meteor.transform.SetParent(transform);
        meteor.SetActive(false);
        ObjectPool.Enqueue(meteor);
        return meteor;
    }

    public void ReturnToQueue(GameObject meteor)
    {
        meteor.gameObject.SetActive(false);
        float X = Random.Range(Spawn_minX, Spawn_maxX);
        float Z = Random.Range(Spawn_minZ, Spawn_maxZ);
        meteor.transform.position = new Vector3(X, Spawn_Y, Z);
        meteor.transform.SetParent(instance.transform);
        instance.ObjectPool.Enqueue(meteor);
    }

    public IEnumerator Start_Armageddon()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            DeQueue();
            yield return new WaitForSeconds(delay);

        }
    }
    public GameObject DeQueue()
    {
        if (ObjectPool.Count > 0) 
        {
            GameObject meteor = ObjectPool.Dequeue();

            meteor.SetActive(true);
            //meteor.gameObject.transform.SetParent(null);
            return meteor_prefeb;
        }
        else
        {
            return null;
        }
    }

}
