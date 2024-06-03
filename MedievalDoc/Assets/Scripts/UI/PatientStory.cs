using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PatientStory : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject storyObject;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float delay;
    [SerializeField] private float fadeoutDelay;


    private string fullText;
    private string currentText="";
    private Patient patient;

    [SerializeField] private float timeToFade;
    private bool fadeout=false;

    private void Start()
    {
        storyObject.SetActive(true);
        patient = GetComponent<Patient>();
        fullText = patient.Sickness.stories[Random.Range(0,patient.Sickness.stories.Count)];
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0,i);
            text.text = currentText;

            if (currentText == fullText)
            {
                StopText();
            }

            yield return new WaitForSeconds(delay);
        }
    }

    private void StopText()
    {
        StopCoroutine(Typing());
        Debug.Log("stoping text");
        fadeout = true;
        StartCoroutine(FadeOut());    
    }


    IEnumerator FadeOut()
    {
        float time = 0.0f;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= time / Mathf.Max(timeToFade, 0.000001f);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //else
        //{
        //    if (canvasGroup.alpha >= 0)
        //    {
        //        canvasGroup.alpha -= time / Mathf.Max(timeToFade, 0.000001f);

        //        if (canvasGroup.alpha == 0)
        //        {
        //            fadeout = false;
        //        }
        //    }
        //}

        yield return new WaitForSeconds(fadeoutDelay);
    }

    //private void RemoveDisplay()
    //{

    //}

    //private void Display()
    //{
    //    storyObject.SetActive(true);
    //    text.text = patient.Sickness.stories[Random.Range(0, patient.Sickness.stories.Count)];

    //    fadeout = true;
    //}

    //private void Update()
    //{
        //if (fadeout)
        //{
        //    Debug.Log("hfljk");
        //    //if (canvasGroup.alpha >= 0)
        //    //{
        //    //    canvasGroup.alpha -= timeToFade * Time.deltaTime;

        //    //    if (canvasGroup.alpha == 0)
        //    //    {
        //    //        fadeout = false;
        //    //    }
        //    //}
        //}
    //}
}
