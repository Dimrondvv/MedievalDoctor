using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpgradeManager : MonoBehaviour
{
    private List<Tool> possessedTools; // Tools you have
    private List<Symptom> curableSymptoms; // Symptoms that are curable with your current equipment

    private void MakeMeChoose(List<Tool> fullToolList)
    {
        List<Tool> toolPool = fullToolList.Except(possessedTools).ToList(); // List of not earned yet items
        // Debug.Log(toolPool);
        // wyplu� np 3 mo�liwe toolsy do wyboru -> chyba okienko z wyborem po prostu
        // po wybraniu doda� toola do listy posiadanych i na jego podstawie zaktualizowa� curable symptomy 
        // po tym spawn toola? i guess
    }


    private List<Tool> tempListOfTools;
    private void Start() // i made it up UwU :3 :D :) ;p
    {
        /*possessedTools[0].name = "banan";     //no to tak to jest do pizdy, trzeba zrobi� <- tu debilu zacznij
        possessedTools[1].name = "gruszka";     //klase tymczasow� nawet z samym stringim i doprowadzi� to do dzia�ania
        possessedTools[2].name = "brzoskwinia"; //jak zacznie dzia�a� to poprzerabia� wszystkie toolsy tak �eby
        tempListOfTools[0].name = "drzewo";     //wpisywa�y si� na list�
        tempListOfTools[1].name = "banan";
        tempListOfTools[2].name = "pies";
        tempListOfTools[3].name = "gruszka";
        tempListOfTools[4].name = "hihiahah";
        tempListOfTools[5].name = "brzoskwinia";*/

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) // https://youtu.be/ujUKcNZ-rK4?si=9zcX_2YXuFKOEOUh
        {
            MakeMeChoose(tempListOfTools);
        }
    }
}
