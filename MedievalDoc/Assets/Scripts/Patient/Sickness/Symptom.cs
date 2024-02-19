using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SymptomScriptable", menuName = "ScriptableObjects/Symptom", order = 2)]
public class Symptom : ScriptableObject
{
    public string symptomName;
    public Sprite symptomIcon;
    //todo: animacja
    public int damage; //how much hp does symptom take per tick
    public AddedOnRemoval addOnRemove;


    [System.Serializable]
    public class AddedOnRemoval
    {
        public Symptom symtpomAddedOnRemoval;
        public Symptom notPresentToAdd;
    }
}
