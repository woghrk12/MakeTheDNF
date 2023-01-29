using System;   
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}

public class ObjectPoolingManager : SingletonMonobehaviour<ObjectPoolingManager>
{
    #region Variables

    [SerializeField] private Pool[] pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, GameObject> categories = new Dictionary<string, GameObject>();

    #endregion Variables

    #region Unity Event

    protected override void Awake()
    {
        base.Awake();

        foreach (Pool t_pool in pools)
        {
            GameObject t_cateory = new GameObject(t_pool.tag);
            t_cateory.transform.SetParent(transform);
            categories.Add(t_pool.tag, t_cateory);

            poolDictionary.Add(t_pool.tag, new Queue<GameObject>());

            for (int i = 0; i < t_pool.size; i++) CreateNewObject(t_pool.tag, t_pool.prefab);
        }
    }

    #endregion Unity Event

    #region Methods

    public static GameObject Instantiate(string p_tag) => Instance.SpawnFromPool(p_tag, Vector3.zero, Quaternion.identity);
    public static GameObject Instantiate(string p_tag, Vector3 p_pos) => Instance.SpawnFromPool(p_tag, p_pos, Quaternion.identity);
    public static GameObject Instantiate(string p_tag, Vector3 p_pos, Quaternion p_rotation) => Instance.SpawnFromPool(p_tag, p_pos, p_rotation);
    public static void Destroy(GameObject p_obj) => Instance.ReturnToPool(p_obj);

    #endregion Methods

    #region Helper Methods

    private GameObject SpawnFromPool(string p_tag, Vector3 p_position, Quaternion p_rotation)
    {
        Queue<GameObject> t_poolQueue = poolDictionary[p_tag];

        if (t_poolQueue.Count <= 0)
        {
            Pool t_pool = Array.Find(pools, x => x.tag == p_tag);
            CreateNewObject(t_pool.tag, t_pool.prefab);
        }

        GameObject t_obj = t_poolQueue.Dequeue();

        t_obj.transform.SetPositionAndRotation(p_position, p_rotation);
        t_obj.SetActive(true);

        return t_obj;
    }

    private void ReturnToPool(GameObject p_obj)
    {
        p_obj.SetActive(false);
        poolDictionary[p_obj.name].Enqueue(p_obj);
    }

    private void CreateNewObject(string p_tag, GameObject p_prefab)
    {
        GameObject t_obj = Instantiate(p_prefab, transform);

        t_obj.name = p_tag;
        t_obj.transform.SetParent(categories[p_tag].transform);
        t_obj.SetActive(false); 

        poolDictionary[p_tag].Enqueue(t_obj);
    }

    #endregion Helper Methods
}
