using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuUi : MonoBehaviour
{
    [SerializeField] Button closeBnt;

    private void Start()
    {
        closeBnt.onClick.AddListener(closeLevelMenu);
    }

    private void closeLevelMenu()
    {
        this.gameObject.SetActive(false);
    }

}
