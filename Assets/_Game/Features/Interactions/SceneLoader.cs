using System.Collections;
using Game.Player;
using Game.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Fade _fadePrefab;
    [SerializeField] private float _stopInputTimeSpan = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) { return; }
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        Player player = Player.Instance;
        player.StartCoroutine(player.StopInputForSeconds(_stopInputTimeSpan));
        Fade fade = Instantiate(_fadePrefab);
        fade.FadeOut();
        yield return new WaitForSeconds(fade.FadeDuration);
        SceneManager.LoadScene(sceneToLoad);
    }
}
