using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarm;
    private Coroutine _fadeIn;
    private Coroutine _fadeOut;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CharacterController2D>(out CharacterController2D controller)) {
            _alarm.Play();
            
            if (_fadeOut != null)
                StopCoroutine(_fadeOut);
            
            _fadeIn = StartCoroutine(FadeInSound());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CharacterController2D>(out CharacterController2D controller)) {

            if (_fadeIn != null)
                StopCoroutine(_fadeIn);
            
            _fadeOut = StartCoroutine(FadeOutSound());
        }
    }

    private IEnumerator FadeInSound()
    {
        if (_alarm.volume == 1)
            yield break;

        float fadeDuration = 5f;

        while (_alarm.volume != 1) {
            _alarm.volume = Mathf.MoveTowards(_alarm.volume, 1f, Time.deltaTime / fadeDuration);
            yield return null;
        }
    }

    private IEnumerator FadeOutSound()
    {
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
