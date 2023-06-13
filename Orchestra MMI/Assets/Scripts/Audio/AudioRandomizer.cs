using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioRandomizer : MonoBehaviour
{
    [SerializeField] private AudioController[] Instrument;

    [SerializeField] private float decreaseVolTimer = 0.1f;
    [SerializeField] private float increaseVolTimer = 0.1f;
    [SerializeField] private float offTimer = 7.0f;
    [SerializeField] private float repeatTimer = 13.0f;

    private int previousNumber = 100;
    [HideInInspector] public int fixingInstrumentIndex = 101;
    [SerializeField] private Animator[] animators;

    [SerializeField] private GameObject stoppingParticle;

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
        }
        
    }

    public IEnumerator DecreaseVolCoroutine(int chosenInstrument)
    {
        Instantiate(stoppingParticle, Instrument[chosenInstrument].transform.position, transform.rotation);
        yield return new WaitForSeconds(5f);

        while (Instrument[chosenInstrument].volume > 0)
        {
            if (Instrument[chosenInstrument].volume <= 0.1f)
            {
                yield break;
            }

            float clampedVolume = Mathf.Clamp(Instrument[chosenInstrument].volume -= Time.deltaTime, 0, 1);
            Instrument[chosenInstrument].volume = clampedVolume;

            if (chosenInstrument != 1)
                animators[chosenInstrument].SetFloat("Speed", 0f);
            else if (chosenInstrument == 1)
            {
                animators[chosenInstrument].SetFloat("par_playSpeed", 1f);
                animators[chosenInstrument].Play("LidClosing");
            }

            yield return new WaitForSeconds(decreaseVolTimer);
        }
    }

    public IEnumerator IncreaseVolCoroutine(int chosenInstrument)
    {
        while (Instrument[chosenInstrument].volume < 1)
        {
            if (Instrument[chosenInstrument].volume >= 1)
            {
                yield break;
            }
            
            float clampedVolume = Mathf.Clamp(Instrument[chosenInstrument].volume += Time.deltaTime, 0, 1);
            Instrument[chosenInstrument].volume = clampedVolume;
            
            if (chosenInstrument != 1)
                animators[chosenInstrument].SetFloat("Speed", 1f);
            else if (chosenInstrument == 1)
            {
                animators[chosenInstrument].SetFloat("par_playSpeed", 1f);
                animators[chosenInstrument].Play("LidOpeningAnim");
            }

            yield return new WaitForSeconds(increaseVolTimer);
        }
    }
}
