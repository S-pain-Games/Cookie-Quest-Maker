using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class SoundEmitterBehaviour : MonoBehaviour
{
    private AudioSource _source;


    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        _source.volume = volume;
        _source.PlayOneShot(clip);
        StartCoroutine(StopOnFinish(clip));
    }

    private IEnumerator StopOnFinish(AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length + 0.05f);
        Destroy(gameObject); // Pooling
    }
}
