using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool{

        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    public Dictionary < string, Queue< GameObject >> poolDictionary;
    public static ObjectPooler Instance;
    private void Awake()=>Instance = this;
   
    void Start(){

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach( Pool pool in pools){

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for ( int i = 0; i < pool.size; i++){

                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation){
        
        if(!poolDictionary.ContainsKey(tag)){

            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }
        
        GameObject ObjToSpawn = poolDictionary[tag].Dequeue();

        ObjToSpawn.SetActive(true);
        ObjToSpawn.transform.position = position;
        ObjToSpawn.transform.rotation = rotation;
        ObjToSpawn.GetComponent<IPooledObject>().OnObjectSpawn();

        poolDictionary[tag].Enqueue(ObjToSpawn);
        
        return ObjToSpawn;
    }

}
