using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectPooler
{

    public static Dictionary<string, Component> poolLookup = new Dictionary<string, Component>();
    public static Dictionary<string, Queue<Component>> poolDictinary = new Dictionary<string, Queue<Component>>();


    //Put in Pool
    public static void EnqueueObject<T>(T item, Transform PoolParent, string name) where T : Component
    {
        //if(!item.gameObject.activeSelf) return;

        item.transform.SetParent(PoolParent);
        item.transform.position = Vector3.zero;
        item.gameObject.SetActive(false);
        poolDictinary[name].Enqueue(item);
    }

    public static void EnqueueObject<T>(T item, string key) where T : Component
    {
        //if (!item.gameObject.activeSelf) return;

        //item.transform.SetParent(PoolParent);
        item.transform.position = Vector3.zero;
        item.gameObject.SetActive(false);
        poolDictinary[key].Enqueue(item);
    }

    //Get Out Pool
    public static T DequeueObject<T>(string key,T pooledItemPrefab=null, Transform Parent = null) where T : Component
    {
        if (!poolDictinary.ContainsKey(key))
        {
            poolDictinary.Add(key, new Queue<Component>());

            poolLookup.Add(key, pooledItemPrefab);

            T pooledInstance = Object.Instantiate(pooledItemPrefab, Parent);
            pooledInstance.name = pooledItemPrefab.name;
            pooledInstance.gameObject.SetActive(true);
            //poolDictinary[key].Enqueue((T)pooledInstance);

            return pooledInstance;
        }

        if (poolDictinary[key].TryDequeue(out var item))
        {
            item.gameObject.SetActive(true);
            return (T)item;
        }

        return (T)EnqueueNewInstance(poolLookup[key], Parent);
    }
    private static T EnqueueNewInstance<T>(T item, Transform Parent = null) where T : Component
    {
        T newInstance = Object.Instantiate(item, Parent);
        newInstance.name = item.name;
        newInstance.transform.position = Vector3.zero;
        newInstance.gameObject.SetActive(true);
        return newInstance;
    }

    public static void SetupPool<T>(T pooledItemPrefab,Transform poolParent, int poolSize, string dictionaryEntry) where T : Component
    {
        poolDictinary.Add(dictionaryEntry, new Queue<Component>());

        poolLookup.Add(dictionaryEntry, pooledItemPrefab);

        for(int i = 0; i < poolSize; i++)
        {
            T pooledInstance = Object.Instantiate(pooledItemPrefab, poolParent);
            pooledInstance.name = pooledItemPrefab.name;
            pooledInstance.gameObject.SetActive(false);
            poolDictinary[dictionaryEntry].Enqueue((T)pooledInstance);
        }
    }   
}
