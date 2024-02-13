using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftProgressBar : MonoBehaviour
{
    [SerializeField] private Image barImage;
    private bool isCrafting;
    private float progress = 0;
    
    public void StartProgressBar(float timeToCraft)
    {
        barImage.transform.parent.gameObject.SetActive(true);
        StartCoroutine(ProgressBar(timeToCraft));
    }
    public void StopProgressBar()
    {
        barImage.transform.parent.gameObject.SetActive(false);
    }
    IEnumerator ProgressBar(float timeToCraft)
    {
        float time = 0.0f;
        while(time < timeToCraft)
        {
            barImage.fillAmount = time / Mathf.Max(timeToCraft, 0.000001f);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
