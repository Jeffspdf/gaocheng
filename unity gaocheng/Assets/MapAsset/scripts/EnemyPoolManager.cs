using UnityEngine;
using System.Collections.Generic;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;

    [Header("����Ԥ�����")]
    public GameObject[] enemyPrefabs; // ���п��ܵĵ���Ԥ����

    [Header("������")]
    [SerializeField] private int initialPoolSize = 5;

    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

    void Awake()
    {
        Instance = this;
        InitializePool();
    }

    // ��ʼ�������
    void InitializePool()
    {
        foreach (var prefab in enemyPrefabs)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(prefab, objectPool);
        }
    }

    // �ӳ��л�ȡ����
    public GameObject GetEnemyFromPool(Vector3 position, Quaternion rotation)
    {
        // ���ѡ��Ԥ��������
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject prefab = enemyPrefabs[randomIndex];

        if (poolDictionary[prefab].Count == 0)
        {
            // ����ؿ����½�
            GameObject newObj = Instantiate(prefab);
            newObj.SetActive(false);
            poolDictionary[prefab].Enqueue(newObj);
        }

        GameObject enemy = poolDictionary[prefab].Dequeue();
        enemy.transform.position = position;
        enemy.transform.rotation = rotation;
        enemy.SetActive(true);

        return enemy;
    }

    // ���յ��˵���
    public void ReturnToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        foreach (var pair in poolDictionary)
        {
            if (pair.Key.name == enemy.name.Replace("(Clone)", ""))
            {
                pair.Value.Enqueue(enemy);
                return;
            }
        }
    }
}