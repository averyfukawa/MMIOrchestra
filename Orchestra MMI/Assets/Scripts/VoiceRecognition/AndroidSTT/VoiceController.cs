using System.Collections;
using System.Collections.Generic;
using TextSpeech;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class VoiceController : MonoBehaviour
{
    const string LANG_CODE = "en-US";

    [SerializeField] Text uiText;

    private void Start()
    {
        SetupLanguage(LANG_CODE);
#if UNITY_ANDROID
        SpeechToText.Instance.onPartialResultsCallback = OnPartialSpeechResult;
#endif
        SpeechToText.Instance.onResultCallback = OnFinalSpeechResult;
        
        StartListening();

        CheckPermission();
    }

#if UNITY_ANDROID
    private void CheckPermission()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
    }
#endif

    void SetupLanguage(string code)
    {
        SpeechToText.Instance.Setting(code);
    }

    #region Speech to Text

    public void StartListening()
    {
        SpeechToText.Instance.StartRecording();
    }

    public void StopListening()
    {
        SpeechToText.Instance.StopRecording();
    }

    private void OnFinalSpeechResult(string result)
    {
        uiText.text = result;
        if (result != null)
        {
            gameObject.transform.localScale += new Vector3(3, 0, 3);
        }
    }

    private void OnPartialSpeechResult(string result)
    {
        uiText.text = result;
        if (result != null) 
        {
            gameObject.transform.localScale += new Vector3(3, 0, 3);
        }
    }

    #endregion
}
