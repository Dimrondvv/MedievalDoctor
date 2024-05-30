using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFade : MonoBehaviour
{
    Transform playerPosition;
    [SerializeField] float timeToFade;
    bool isRunning = false;
    float alpha = 1;
    List<Collider> fadedOutWalls;
    void Start()
    {
        playerPosition = App.Instance.GameplayCore.PlayerManager.PickupController.transform;
        fadedOutWalls = new List<Collider>();
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, (playerPosition.position - transform.position));
        RaycastHit hit;
        Gizmos.color = Color.yellow;
        Debug.DrawRay(transform.position, (playerPosition.position - transform.position));
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag == "High Wall" && !isRunning)
            {
                MeshRenderer renderer = hit.collider.gameObject.GetComponent<MeshRenderer>();
                fadedOutWalls.Add(hit.collider);
                StartCoroutine(FadeOutWall(renderer));
            }
        }
        List<Collider> wallsToRemove = new List<Collider>();
        foreach(var wall in fadedOutWalls)
        {
            if(wall != hit.collider)
            {
                StartCoroutine(FadeInWall(wall.gameObject.GetComponent<MeshRenderer>()));
                wallsToRemove.Add(wall);
            }
        }
        foreach(var wall in wallsToRemove)
        {
            fadedOutWalls.Remove(wall);
        }
    }

    IEnumerator FadeOutWall(MeshRenderer wallRenderer)
    {
        isRunning = true;
        Color defaultColor = wallRenderer.material.color;
        float time = 0.0f;
        while (alpha > 0)
        {
            alpha -= time / Mathf.Max(timeToFade, 0.000001f);
            wallRenderer.material.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, alpha);
            time += Time.deltaTime;
            Debug.Log(alpha);
            yield return new WaitForEndOfFrame();
        }
        isRunning = false;
    }
    IEnumerator FadeInWall(MeshRenderer wallRenderer)
    {
        isRunning = true;
        Color defaultColor = wallRenderer.material.color;
        float time = 0.0f;
        while (alpha < 1)
        {
            alpha += time / Mathf.Max(timeToFade, 0.000001f);
            wallRenderer.material.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, alpha);
            time += Time.deltaTime;
            Debug.Log(alpha);
            yield return new WaitForEndOfFrame();
        }
        isRunning = false;
    }
}
