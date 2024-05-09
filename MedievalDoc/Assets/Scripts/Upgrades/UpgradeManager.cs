using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] public List<Upgrade> upgrades;

    // Start is called before the first frame update
    void Start()
    {
        App.Instance.GameplayCore.RegisterUpgradeManager(this);
    }
    private void OnDestroy()
    {
        App.Instance.GameplayCore.UnregisterUpgradeManager();
    }
    
}
