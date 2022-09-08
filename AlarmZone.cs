using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class AlarmZone : MonoBehaviour 
{
    [SerializeField] private UnityEvent _cameIn;
    [SerializeField] private UnityEvent _cameOut;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent<CharacterController2D>(out CharacterController2D controller)) {
            _cameIn.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.TryGetComponent<CharacterController2D>(out CharacterController2D controller)) {
            _cameOut.Invoke();
        }
    }
}
