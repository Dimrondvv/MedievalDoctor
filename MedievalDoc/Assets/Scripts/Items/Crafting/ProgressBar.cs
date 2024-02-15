using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image barImage;

    private void Update()
    {
        barImage.transform.parent.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void StartProgressBar(float timeToCraft)
    {
        barImage.transform.parent.gameObject.SetActive(true);
        StartCoroutine(ProgressBarFill(timeToCraft));
    }
    public void StopProgressBar()
    {
        StopCoroutine(ProgressBarFill(0));
        barImage.transform.parent.gameObject.SetActive(false);
    }
    IEnumerator ProgressBarFill(float timeToCraft)
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
