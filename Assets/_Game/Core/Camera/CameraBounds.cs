using Game.Player;
using UnityEngine;

public class CameraBounds : MonoBehaviour {
    private void Start() {
        Player.Instance.SetCameraBounds(GetComponent<CompositeCollider2D>());
    }
}