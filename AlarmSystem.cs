using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _alarm;
    [SerializeField] private float _fadeInDuration = 5f;
    [SerializeField] private float _fadeOutDuration = 3f;

    private Coroutine _fadeIn;
    private Coroutine _fadeOut;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;

    public void TurnOn() {
        _alarm.Play();

        if (_fadeOut != null)
            StopCoroutine(_fadeOut);

        _fadeIn = StartCoroutine(FadeSound(_maxVolume, _fadeInDuration));
    }

    public void TurnOff() {
        if (_fadeIn != null)
            StopCoroutine(_fadeIn);

        _fadeOut = StartCoroutine(FadeSound(_minVolume, _fadeOutDuration));
    }
    
    private IEnumerator FadeSound(float targetVolume, float fadeDuration) {
        if (_alarm.volume == targetVolume) {
            if (_alarm.volume == _minVolume)
                _alarm.Stop();
            yield break;
        }

        while (_alarm.volume != targetVolume) {
            _alarm.volume = Mathf.MoveTowards(_alarm.volume, targetVolume, Time.deltaTime / fadeDuration);
            yield return null;
        }
    }
}
