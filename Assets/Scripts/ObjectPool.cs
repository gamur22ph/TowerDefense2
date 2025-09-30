using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    /// The design Dictionary<string, List<GameObject>> takes string as an id(Name of the prefab), and every id is a list of gameobjects.
    /// Getting a list of GameObjects in a unique id has a lookup of O(1) and Iterating through the list when reusing gameobjects is O(n)
    [SerializeField] Dictionary<string, List<GameObject>> objectsToPool;

    void Awake()
    {
        Instance = this;
        objectsToPool = new Dictionary<string, List<GameObject>>();
    }

    // OBJECT POOLING

    public GameObject AddObject(GameObject gameObject)
    {
        // If there is no GameObject of the same name, add pooled object list to dictionary.
        if (!objectsToPool.ContainsKey(gameObject.name))
        {
            objectsToPool.Add(gameObject.name, new List<GameObject>());
        }

        // Add gameobject to pool
        GameObject newGameObject = Instantiate(gameObject);
        objectsToPool[gameObject.name].Add(newGameObject);
        newGameObject.SetActive(false);
        return newGameObject;
    }

    public GameObject GetObject(GameObject gameObject)
    {

        if (!objectsToPool.ContainsKey(gameObject.name))
        {
            objectsToPool.Add(gameObject.name, new List<GameObject>());
        }

        // find inactive pooled object for use.
        for (int i = 0; i < objectsToPool[gameObject.name].Count; i++)
        {
            if (!objectsToPool[gameObject.name][i].activeInHierarchy)
            {
                objectsToPool[gameObject.name][i].SetActive(true);
                return objectsToPool[gameObject.name][i];
            }
        }
        GameObject newGameObject = Instantiate(gameObject);
        objectsToPool[gameObject.name].Add(newGameObject);
        newGameObject.SetActive(true);
        return newGameObject;
    }

    public GameObject GetObject(GameObject gameObject, Transform parent)
    {
        if (!objectsToPool.ContainsKey(gameObject.name))
        {
            objectsToPool.Add(gameObject.name, new List<GameObject>());
        }

        for (int i = 0; i < objectsToPool[gameObject.name].Count; i++)
        {
            if (!objectsToPool[gameObject.name][i].activeInHierarchy)
            {
                objectsToPool[gameObject.name][i].SetActive(true);
                objectsToPool[gameObject.name][i].transform.parent = parent;
                return objectsToPool[gameObject.name][i];
            }
        }
        GameObject newGameObject = Instantiate(gameObject, parent);
        objectsToPool[gameObject.name].Add(newGameObject);
        newGameObject.SetActive(true);
        return newGameObject;
    }

    public GameObject GetObject(GameObject gameObject, Vector3 initialPosition, Quaternion initialRotation)
    {
        if (!objectsToPool.ContainsKey(gameObject.name))
        {
            objectsToPool.Add(gameObject.name, new List<GameObject>());
        }

        for (int i = 0; i < objectsToPool[gameObject.name].Count; i++)
        {
            if (!objectsToPool[gameObject.name][i].activeInHierarchy)
            {
                objectsToPool[gameObject.name][i].SetActive(true);
                objectsToPool[gameObject.name][i].transform.SetPositionAndRotation(initialPosition, initialRotation);
                return objectsToPool[gameObject.name][i];
            }
        }
        GameObject newGameObject = Instantiate(gameObject, initialPosition, initialRotation);
        objectsToPool[gameObject.name].Add(newGameObject);
        newGameObject.SetActive(true);
        return newGameObject;
    }
}

