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
    [HideInInspector] public int fixingInstrumentIndex = 101;

    private void Start()
    {
        InvokeRepeating(nameof(CallDecreaseVol), offTimer, repeatTimer);
    }

    private int SetRandomInstrument()
    {
        int randomIndex = Random.Range(0, Instrument.Length);

        while (randomIndex == previousNumber && randomIndex == fixingInstrumentIndex)
        {
            randomIndex = Random.Range(0, Instrument.Length);
        }

        previousNumber = randomIndex;

        return randomIndex;
    }

    public void CallDecreaseVol()
    {
        if (fixingInstrumentIndex == SetRandomInstrument())
        {
            return;
        }
        else if (fixingInstrumentIndex != SetRandomInstrument())
        {
            StartCoroutine(DecreaseVolCoroutine(SetRandomInstrument()));
            Debug.Log("Lowering volume of: " + Instrument[SetRandomInstrument()].name);
        }
        
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

            yield return new WaitForSeconds(waitForTimer);
        }
    }

    public IEnumerator IncreaseVolCoroutine(int chosenInstrument)
    {
        while (Instrument[chosenInstrument].volume < 1)
        {
            if (Instrument[chosenInstrument].volume >= 1)
            {
                Debug.Log("Volume reached max: " + Instrument[chosenInstrument].volume);
                yield break;
            }
            
            float clampedVolume = Mathf.Clamp(Instrument[chosenInstrument].volume += Time.deltaTime, 0, 1);
            Instrument[chosenInstrument].volume = clampedVolume;

            yield return new WaitForSeconds(waitForTimer);
        }
    }
}
