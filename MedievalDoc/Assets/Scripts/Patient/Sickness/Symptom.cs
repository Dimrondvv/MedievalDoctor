using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SymptomScriptable", menuName = "ScriptableObjects/Symptom", order = 2)]
public class Symptom : ScriptableObject
{
    public string symptomName;
    public Sprite symptomIcon;
    //todo: animacja
    [Header("Damage patient takes per tick")]
    public int damage; //how much hp does symptom take per tick
    [Header("Points added on release, should be negative \nto take away score, score/modifier of as hp and money")]
    public int score; //How much points player gets when symptom is present on patient release (should be negative if symptom does not reward)
    public List<Localization> possibleLocalizations;
    public bool doesRemoveLocalization;
    [MyBox.ConditionalField(nameof(doesRemoveLocalization))] public Localization localizationRemoved;
    public AddedOnRemoval addOnRemove;
    public bool isHidden;

    [System.Serializable]
    public class AddedOnRemoval
    {
        public Symptom symtpomAddedOnRemoval;
        public Symptom notPresentToAdd;
    }
}
