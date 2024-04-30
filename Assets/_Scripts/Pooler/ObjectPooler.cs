using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectPooler
{

    public static Dictionary<string, Component> poolLookup = new Dictionary<string, Component>();
    public static Dictionary<string, Queue<Component>> poolDictinary = new Dictionary<string, Queue<Component>>();

    public static void EnqueueObject<T>(T item, Transform PoolParent, string name) where T : Component
    {
        if(!item.gameObject.activeSelf) return;

        item.transform.SetParent(PoolParent);
        item.transform.position = Vector3.zero;
        poolDictinary[name].Enqueue(item);
        item.gameObject.SetActive(false);
    }

    public static T DequeueObject<T>(string key) where T : Component
    {
        //return (T)poolDictinary[key].Dequeue();

        if (poolDictinary[key].TryDequeue(out var item))
        {
            return (T)item;
        }
        return (T)EnqueueNewInstance(poolLookup[key], key);
    }

    public static T EnqueueNewInstance<T>(T item, string key) where T : Component
    {
        T newInstance = Object.Instantiate(item);
        newInstance.gameObject.SetActive(false);
        newInstance.transform.position = Vector3.zero;
        poolDictinary[key].Enqueue(newInstance);
        return newInstance;
    }

    public static void SetupPool<T>(T pooledItemPrefab,Transform poolParent, int poolSize, string dictionaryEntry) where T : Component
    {
        poolDictinary.Add(dictionaryEntry, new Queue<Component>());

        poolLookup.Add(dictionaryEntry, pooledItemPrefab);

        for(int i = 0; i < poolSize; i++)
        {
            T pooledInstance = Object.Instantiate(pooledItemPrefab, poolParent);
            pooledInstance.gameObject.SetActive(false);
            poolDictinary[dictionaryEntry].Enqueue((T)pooledInstance);
        }
    }   
}
