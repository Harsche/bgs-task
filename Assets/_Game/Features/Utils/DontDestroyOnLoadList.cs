using UnityEngine;

public class DontDestroyOnLoadList : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;

    private void Awake()
    {
        foreach (var obj in gameObjects)
        {
            DontDestroyOnLoad(obj);
        }
    }
}
