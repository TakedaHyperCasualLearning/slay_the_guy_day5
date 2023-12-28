using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private Dictionary<int, List<GameObject>> objectPool = new Dictionary<int, List<GameObject>>();
    private bool isNewCreate = false;

    public ObjectPool(GameEvent gameEvent)
    {
        gameEvent.ReleaseObject += ReleaseObject;
    }

    public GameObject GetGameObject(GameObject prefab)
    {
        int hashCode = prefab.GetHashCode();
        if (objectPool.ContainsKey(hashCode))
        {
            List<GameObject> gameObjectList = objectPool[hashCode];
            for (int i = 0; i < gameObjectList.Count; i++)
            {
                GameObject gameObject = gameObjectList[i];
                if (!gameObject.activeSelf)
                {
                    gameObject.SetActive(true);
                    return gameObject;
                }
            }

            GameObject newGameObject = GameObject.Instantiate(prefab);
            gameObjectList.Add(newGameObject);
            isNewCreate = true;
            return newGameObject;
        }

        GameObject newGameObject2 = GameObject.Instantiate(prefab);
        List<GameObject> newGameObjectList = new List<GameObject>();
        newGameObjectList.Add(newGameObject2);
        objectPool.Add(hashCode, newGameObjectList);
        isNewCreate = true;
        return newGameObject2;
    }

    private void ReleaseObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public bool IsNewCreate { get => isNewCreate; set => isNewCreate = value; }
}
