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
    [SerializeField] private Image cloud;
    [SerializeField] private float delay;
    [SerializeField] private float fadeoutDelay;
    [SerializeField] private float cloudIncrease;
    [SerializeField] private float cloudDelay;

    private string fullText;
    private string currentText="";
    private Patient patient;

    [SerializeField] private float timeToFade;

    public void StoryTime()
    {
        cloud.transform.localScale = new Vector3(0, 0, 0);
        storyObject.SetActive(true);
        patient = GetComponent<Patient>();
        fullText = patient.Sickness.stories[Random.Range(0,patient.Sickness.stories.Count)];
        StartCoroutine(Cloud());
    }
    IEnumerator Cloud()
    {
        float increase = 0.0f;
        while(cloud.transform.localScale.x < 1)
        {
            cloud.transform.localScale = new Vector3(increase,increase,increase);
            increase += cloudIncrease;
            yield return new WaitForSeconds(cloudDelay);
        }
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        StopCoroutine(Cloud());
        while(currentText != fullText)
        {
            for (int i = 0; i <= fullText.Length; i++)
            {
                currentText = fullText.Substring(0, i);
                text.text = currentText;

                yield return new WaitForSeconds(delay);
            }
        }
        StopText();
    }

    private void StopText()
    {
        StopCoroutine(Typing());
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
        yield return new WaitForSeconds(fadeoutDelay);
    }
}
