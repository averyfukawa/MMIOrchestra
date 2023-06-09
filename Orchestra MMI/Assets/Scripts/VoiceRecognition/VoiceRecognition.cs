using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
//using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
    public bool instrumentOn;
    /*
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public bool instrumentOn = false;
    [SerializeField]private AudioRandomizer audioRandomizer;

    private void Start()
    {
        actions.Add("play", OnSpeechRecognized);
        actions.Add("instrument", OnSpeechRecognized);
        actions.Add("cello", OnSpeechRecognized);
        actions.Add("violin", OnSpeechRecognized);
        actions.Add("saxophone", OnSpeechRecognized);
        actions.Add("piano", OnSpeechRecognized);
        actions.Add("trumpet", OnSpeechRecognized);
        actions.Add("clarinet", OnSpeechRecognized);
        actions.Add("french horn", OnSpeechRecognized);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
        InvokeRepeating(nameof(ResetVoiceRecognisition), 1f, 1f);
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void OnSpeechRecognized()
    {
        instrumentOn = true;
    }

    private void ResetVoiceRecognisition()
    {
        if (instrumentOn && audioRandomizer.fixingInstrumentIndex == 101)
        {
            Debug.Log(audioRandomizer.fixingInstrumentIndex.ToString());
            instrumentOn = false;
        } 
        else
        {
            return;
        }
    }
    */

    /*
    private bool CheckPermissions()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            return true;
        }

        return AndroidPermissionsManager.IsPermissionGranted(Permission.Microphone);
    }

    public void SpeechCommand()
    {
        if (!CheckPermissions())
        {
            Debug.LogWarning("Missing permission to browse device gallery, please grant the permission first");

            // Your code to show in-game pop-up with the explanation why you need this permission (required for Google Featuring program)
            // This pop-up should include a button "Grant Access" linked to the function "OnGrantButtonPress" below
            return;
        }

        // Your code

        if (!instrumentOn)
        {
            instrumentOn = true;
        }
        else
        {
            return;
        }
        
        Debug.Log("Using Microphone...");
    }

    public void OnGrantMicrophone()
    {
        AndroidPermissionsManager.RequestPermission(new[] { Permission.Microphone }, new AndroidPermissionCallback(
            grantedPermission =>
            {
                // The permission was successfully granted, restart the change avatar routine
                SpeechCommand();
            },
            deniedPermission =>
            {
                // The permission was denied.
                // Show in-game pop-up message stating that the user can change permissions in Android Application Settings
                // if he changes his mind (also required by Google Featuring program)
            }));
    }
    */
}
