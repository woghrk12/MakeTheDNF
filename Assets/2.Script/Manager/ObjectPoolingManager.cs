using System;   
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : SingletonMonobehaviour<ObjectPoolingManager>
{
    #region Variables

    private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
    private Dictionary<string, Transform> categories = new Dictionary<string, Transform>();
    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    #endregion Variables

    #region Methods

    public void AddPool(string p_tag, GameObject p_obj, int p_size)
    {
        Transform t_category = new GameObject(p_tag).transform;
        t_category.SetParent(transform);

        prefabs.Add(p_tag, p_obj);
        categories.Add(p_tag, t_category);
        poolDictionary.Add(p_tag, new Queue<GameObject>());

        for (int i = 0; i < p_size; i++) CreateNewObject(p_tag, p_obj);
    }

    public GameObject Instantiate(string p_tag) => SpawnFromPool(p_tag, Vector3.zero, Quaternion.identity);
    public GameObject Instantiate(string p_tag, Vector3 p_pos) => SpawnFromPool(p_tag, p_pos, Quaternion.identity);
    public GameObject Instantiate(string p_tag, Vector3 p_pos, Quaternion p_rotation) => SpawnFromPool(p_tag, p_pos, p_rotation);
    public void Destroy(GameObject p_obj) => ReturnToPool(p_obj);

    #endregion Methods

    #region Helper Methods

    private GameObject SpawnFromPool(string p_tag, Vector3 p_position, Quaternion p_rotation)
    {
        if (!poolDictionary.TryGetValue(p_tag, out Queue<GameObject> t_poolQueue)) return null;
        if (t_poolQueue.Count <= 0) CreateNewObject(p_tag, prefabs[p_tag]);

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
        t_obj.transform.SetParent(categories[p_tag]);
        t_obj.SetActive(false); 

        poolDictionary[p_tag].Enqueue(t_obj);
    }

    #endregion Helper Methods
}
