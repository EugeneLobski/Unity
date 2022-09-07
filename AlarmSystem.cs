using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSystem : MonoBehaviour {
    [SerializeField] private AudioSource _alarm;
    private Coroutine _fadeIn;
    private Coroutine _fadeOut;

    public void TurnOn() {
        _alarm.Play();

        if (_fadeOut != null)
            StopCoroutine(_fadeOut);

        _fadeIn = StartCoroutine(FadeInSound());
    }

    public void TurnOff() {
        if (_fadeIn != null)
            StopCoroutine(_fadeIn);

        _fadeOut = StartCoroutine(FadeOutSound());
    }

    private IEnumerator FadeInSound() {
        if (_alarm.volume == 1)
            yield break;

        float fadeDuration = 5f;

        while (_alarm.volume != 1) {
            _alarm.volume = Mathf.MoveTowards(_alarm.volume, 1f, Time.deltaTime / fadeDuration);
            yield return null;
        }
    }

    private IEnumerator FadeOutSound() {
        if (_alarm.volume == 0) {
            _alarm.Stop();
            yield break;
        }

        float fadeDuration = 5f;

        while (_alarm.volume != 0) {
            _alarm.volume = Mathf.MoveTowards(_alarm.volume, 0f, Time.deltaTime / fadeDuration);
            yield return null;
        }
    }
}
