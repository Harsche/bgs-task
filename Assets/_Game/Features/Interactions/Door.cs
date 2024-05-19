using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Door : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) { return; }
        ToggleSprite(false);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) { return; }
        ToggleSprite(true);
    }

    private void ToggleSprite(bool value)
    {
        GetComponent<SpriteRenderer>().enabled = value;
    }
}
