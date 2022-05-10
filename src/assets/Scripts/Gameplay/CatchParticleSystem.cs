using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchParticleSystem : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private void OnEnable()
    {
        particleSystem = this.GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (!particleSystem.isPlaying)
            gameObject.SetActive(false); //destroy self when done playing
    }
}
