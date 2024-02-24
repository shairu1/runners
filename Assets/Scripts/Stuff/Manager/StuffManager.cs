
using UnityEngine;


public class StuffManager : MonoBehaviour
{
    private static StuffManager instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject _fireballPrefab;
    [SerializeField] private GameObject _iceballPrefab;
    [SerializeField] private GameObject _bangPrefab;

    private void Awake() => instance = this;
    
    public static GameObject CreateFireball(Vector3 position, Vector3 direction, Space space)
    {
        if (space == Space.Self)
        {
            direction.x = position.x + direction.x;
            direction.y = position.y + direction.y;
        }

        GameObject go = Instantiate(instance._fireballPrefab, position, Quaternion.identity);
        float angle = 180 + Mathf.Atan2(position.y - direction.y, position.x - direction.x) * Mathf.Rad2Deg;
        go.transform.eulerAngles = new Vector3(0, 0, angle);
        return go;
    }

    public static GameObject CreateIceball(Vector3 position, Vector3 direction, Space space)
    {
        if (space == Space.Self)
        {
            direction.x = position.x + direction.x;
            direction.y = position.y + direction.y;
        }

        GameObject go = Instantiate(instance._iceballPrefab, position, Quaternion.identity);
        float angle = 180 + Mathf.Atan2(position.y - direction.y, position.x - direction.x) * Mathf.Rad2Deg;
        go.transform.eulerAngles = new Vector3(0, 0, angle);
        return go;
    }

    public static GameObject CreateBang(Vector3 position)
    {
        GameObject go = Instantiate(instance._bangPrefab, position, Quaternion.identity);
        return go;
    }
}
