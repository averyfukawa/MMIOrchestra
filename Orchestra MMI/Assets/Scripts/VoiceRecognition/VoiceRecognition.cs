using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public bool instrumentOn = false;
    [SerializeField] private AudioRandomizer audioRandomizer;
    [SerializeField] private PlayerInteract resetInst;

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
            StartCoroutine(resetInst.ResetInstrument());
            instrumentOn = false;
        } 
        else
        {
            return;
        }
    }
}
