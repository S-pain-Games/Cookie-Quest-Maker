using System;
using System.Collections;
using UnityEngine;

public class TransitionBehaviour : MonoBehaviour
{
    public Action inCompleted;
    public Action outCompleted;

    [SerializeField] private GameObject _fadeScreen;

    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _midPos;
    [SerializeField] private Transform _endPos;

    [MethodButton]
    public void TransitionIn()
    {
        StopAllCoroutines();
        StartCoroutine(TransitionInCoroutine());
    }

    [MethodButton]
    public void TransitionOut()
    {
        StopAllCoroutines();
        StartCoroutine(TransitionOutCoroutine());
    }

    public IEnumerator TransitionInCoroutine()
    {
        _fadeScreen.transform.position = _midPos.position;

        var wait = new WaitForSeconds(0.005f);

        while ((_fadeScreen.transform.position - _endPos.position).magnitude > 50f)
        {
            _fadeScreen.transform.position = Vector3.Lerp(_fadeScreen.transform.position, _endPos.position, 0.1f);
            yield return wait;
        }

        inCompleted?.Invoke();
    }

    public IEnumerator TransitionOutCoroutine()
    {
        _fadeScreen.transform.position = _startPos.position;

        var wait = new WaitForSeconds(0.005f);

        while ((_fadeScreen.transform.position - _midPos.position).magnitude > 50f)
        {
            _fadeScreen.transform.position = Vector3.Lerp(_fadeScreen.transform.position, _midPos.position, 0.1f);
            yield return wait;
        }

        outCompleted?.Invoke();
    }

    public void ClearAllCallbacks()
    {
        inCompleted = null;
        outCompleted = null;
    }
}
