using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Patron", menuName = "ScriptableObjects/Patron")]
public class Patron : ScriptableObject
{
    public string patronName;
    public string description;
    public Image patronIcon;
    public GameObject patronPrefab;
}

