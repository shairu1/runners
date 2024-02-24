using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject _backgroundPrefab;
    [SerializeField] private GameObject[] _startChunks;
    [SerializeField] private GameObject[] _middleChunks;
    [SerializeField] private GameObject[] _endChunks;

    [Header("Level")]
    [SerializeField] private Transform _mapParent;
    [SerializeField] private List<GameObject> _mapChuncks;

    public static Vector3[] PlayerPositionsOnLevel { private set; get; }


    private void Awake() => instance = this;
    
    public static void CreateLevel()
    {
        // clear map
        Utilities.RemoveAllChildObjects(instance._mapParent);
        instance._mapChuncks.Clear();

        // start chunk
        var go = CreateChunk(GetRandomStartChunk(), Vector3.zero);

        // player positions
        PlayerPositionsOnLevel = go.GetComponent<Chunk>().StartPositions;
        Vector3 position;
        
        // middle chunk
        for (int i = 0; i < GameManager.SizeMap; i++)
        {
            position = new Vector3(100 * (i + 1), 0, 0);
            CreateChunk(GetRandomMiddleChunk(), position);
        }

        // end chunk
        position = new Vector3(100 * (GameManager.SizeMap + 1), 0, 0);
        CreateChunk(GetRandomEndChunk(), position);

        bool flip = false;
        for (int i = 0; i < GameManager.SizeMap + 3; i++)
        {
            go = Instantiate(instance._backgroundPrefab, Vector3.zero, Quaternion.identity, instance._mapParent);
            go.transform.localPosition = new Vector3(100 * i, 0, 100);
            go.GetComponent<SpriteRenderer>().flipX = flip; flip = !flip;
        }
    }

    private static GameObject CreateChunk(GameObject chunk, Vector3 position)
    {
        var go = Instantiate(chunk, Vector3.zero, Quaternion.identity, instance._mapParent);
        go.transform.position = position;
        instance._mapChuncks.Add(go);
        return go;
    }

    private static GameObject GetRandomStartChunk()
    {
        return instance._startChunks[Random.Range(0, instance._startChunks.Length)];
    }

    private static GameObject GetRandomMiddleChunk()
    {
        return instance._middleChunks[Random.Range(0, instance._middleChunks.Length)];
    }

    private static GameObject GetRandomEndChunk()
    {
        return instance._endChunks[Random.Range(0, instance._endChunks.Length)];
    }
}
