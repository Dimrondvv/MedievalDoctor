using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Data
{
    public class ImportJsonData : MonoBehaviour
    {
        SicknessRootObject sicknessODS;

        // Start is called before the first frame update
        void Start()
        {
            sicknessODS = App.Instance.GameplayCore.SaveManager.LoadGameData<SicknessRootObject>("Config_1.json");
            Debug.Log(sicknessODS);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
