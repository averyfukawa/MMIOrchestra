using FMODUnity;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private EventInstance instance;
    public EventReference fmodEvent;
    public GameObject fmodSounds;
    
    [Range(0f, 1f)]
    public float volume;

    private void Start()
    {
        instance = RuntimeManager.CreateInstance(fmodEvent);
        RuntimeManager.AttachInstanceToGameObject(instance, fmodSounds.GetComponent<Transform>());
        instance.setParameterByName("Volume", volume);
        instance.start();
    }

    private void Update()
    {
        instance.setParameterByName("Volume", volume);
    }

    private void OnDestroy()
    {
        StopAllPlayEvents();
    }

    private void StopAllPlayEvents()
    {
        Bus playerBus = RuntimeManager.GetBus("bus:/");
        playerBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
