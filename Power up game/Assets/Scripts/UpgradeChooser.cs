using Unity.Multiplayer.Center.Common;
using UnityEngine;

public class UpgradeChooser : MonoBehaviour
{
    string[] upgrades = { "speedUp", "dmgUp", "extraCard", "hpUp", "lifeStealUp" };
    string c1;
    string c2;
    string c3;
    

    private void OnEnable()
    {
        c1 = upgrades[Random.Range(0, 5)];
        c2 = upgrades[Random.Range(0, 5)];
        c3 = upgrades[Random.Range(0, 5)];
    }
    public void Buy1()
    {
        if (c1 == upgrades[0])
        {
            Debug.Log(upgrades[0]);
        }
        else if (c1 == upgrades[1])
        {
            Debug.Log(upgrades[1]);
        }
        else if (c1 == upgrades[2])
        {
            Debug.Log(upgrades[2]);
        }
        else if (c1 == upgrades[3])
        {
            Debug.Log(upgrades[3]);
        }
        else if (c1 == upgrades[4])
        {
            Debug.Log(upgrades[4]);
        }
    }

    public void Buy2()
    {

    }

    public void Buy3()
    {

    }
}
