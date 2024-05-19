using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class Fade : MonoBehaviour
    {
        [SerializeField] private Image _black;
        [SerializeField] private float _fadeDuration = 0.25f;
        [SerializeField] private FadeOnAwake _fadeOnAwake;
        private Tweener _fadeTweener;

        public float FadeDuration => _fadeDuration;

        private void Awake()
        {
            switch (_fadeOnAwake)
            {
                case FadeOnAwake.None:
                    break;
                case FadeOnAwake.FadeIn:
                    FadeIn();
                    break;
                case FadeOnAwake.FadeOut:
                    FadeOut();
                    break;
            }
        }


        public void FadeIn()
        {
            _black.color = Color.black;
            FadeScreen(0);
        }

        public void FadeOut()
        {
            _black.color = Color.clear;
            FadeScreen(1);
        }

        private void FadeScreen(float finalValue)
        {
            if (_fadeTweener != null && _fadeTweener.IsActive() && _fadeTweener.IsPlaying()) { _fadeTweener.Kill(); }
            _fadeTweener = _black.DOFade(finalValue, _fadeDuration).SetLink(gameObject);
        }

        private enum FadeOnAwake
        {
            None,
            FadeIn,
            FadeOut
        }
    }

}