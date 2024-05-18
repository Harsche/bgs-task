using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField] private UnityEvent _onInteract;

    public void Interact()
    {
        _onInteract?.Invoke();
    }
}