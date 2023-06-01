using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioRandomizer : MonoBehaviour
{
    [SerializeField] private AudioController[] Instrument;
        
    [SerializeField] private float waitForTimer = 0.1f;
    [SerializeField] private float offTimer = 7.0f;
    [SerializeField] private float repeatTimer = 7.0f;

    private int previousNumber = 100;

    private void Start()
    {
        InvokeRepeating(nameof(CallDecreaseVol), offTimer, repeatTimer);
    }

    private int SetRandomInstrument()
    {
        int randomIndex = Random.Range(0, Instrument.Length);

        while (randomIndex == previousNumber)
        {
            randomIndex = Random.Range(0, Instrument.Length);
        }

        previousNumber = randomIndex;

        return randomIndex;
    }

    public void CallDecreaseVol()
    {
        StartCoroutine(DecreaseVolCoroutine(SetRandomInstrument()));
    }

    public IEnumerator DecreaseVolCoroutine(int chosenInstrument)
    {
        //Debug.Log("Started Decreasing volume " + Instrument[chosenInstrument].volume);
        while (Instrument[chosenInstrument].volume > 0)
        {
            if (Instrument[chosenInstrument].volume <= 0)
            {
                Debug.Log("Volume reached 0: " + Instrument[chosenInstrument].volume);
                yield break;
            }
            
            float clampedVolume = Mathf.Clamp(Instrument[chosenInstrument].volume -= Time.deltaTime, 0, 1);
            Instrument[chosenInstrument].volume = clampedVolume;
            //Debug.Log("Decreasing volume: " + Instrument[chosenInstrument].volume);

            yield return new WaitForSeconds(waitForTimer);
            //Debug.Log("Did a loop");
        }
    }
}
