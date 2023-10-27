using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo
{
    public GameObject TilePrefabs;
    public int Xcount;
    public int Zcount;
}

public class TileBatch : MonoBehaviour
{
    [SerializeField] private ObjectInfo[] TileInfo = null;
    public Queue<GameObject> tileQueue = new Queue<GameObject>();

    private void Start()
    {
        tileQueue = InsertQueue(TileInfo[0]);
    }

    private Queue<GameObject> InsertQueue(ObjectInfo info)
    {
        Queue<GameObject> queue = new Queue<GameObject>();
        for (int x = 0; x < info.Xcount; x++)
        {
            for (int z = 0; z < info.Xcount; z++)
            {
                GameObject tileClone = Instantiate(info.TilePrefabs, transform.position, Quaternion.identity);
                tileClone.SetActive(true);
                tileClone.transform.position = new Vector3(-9 + 6 * x, 0, 9 - 6 * z);
                queue.Enqueue(tileClone);
            }
        }
        return queue;
    }


}
