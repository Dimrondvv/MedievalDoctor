using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsPaper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        App.Instance.GameplayCore.DaySummaryManager.onNewDay.AddListener(test);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void test() { Debug.Log("DUPSON"); }
}
