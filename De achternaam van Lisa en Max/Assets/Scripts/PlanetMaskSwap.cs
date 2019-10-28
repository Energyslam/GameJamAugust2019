using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMaskSwap : MonoBehaviour
{
    [SerializeField]
    Material mat;

    int maskValue = 0; //determines when the mask changes
    bool hasChanged = false;
    

    // Start is called before the first frame update
    void Start()
    {
        mat.DisableKeyword("MASK0");
        mat.DisableKeyword("MASK1");
        mat.DisableKeyword("MASK2");
        mat.DisableKeyword("MASK3");
        mat.DisableKeyword("MASK4");
        mat.EnableKeyword("MASK5");
        mat.DisableKeyword("MASK6");
        mat.DisableKeyword("MASK7");
        mat.DisableKeyword("MASK8");
        mat.DisableKeyword("MASK9");
        mat.DisableKeyword("MASK10");
    }

    // Update is called once per frame
    void Update()
    {
        int previousMaskValue = maskValue;

        if (GreenMeter.instance.GetCurrentGreenAmount() > 95)
        {
            maskValue = 5;
        }
        else
        {
            maskValue = (int)(GreenMeter.instance.GetCurrentGreenAmount() / 20);
        }


        if (maskValue < previousMaskValue || maskValue > previousMaskValue)
        {
            ChangeMask();
            Debug.Log("maskValue" + maskValue);
        }
        
    }

    void ChangeMask()
    {
        switch (maskValue)
        {
            case -5:
                mat.DisableKeyword("MASK1");
                mat.EnableKeyword("MASK0");
                break;
            case -4:
                mat.DisableKeyword("MASK0");
                mat.DisableKeyword("MASK2");
                mat.EnableKeyword("MASK1");
                break;
            case -3:
                mat.DisableKeyword("MASK1");
                mat.DisableKeyword("MASK3");
                mat.EnableKeyword("MASK2");
                break;
            case -2:
                mat.DisableKeyword("MASK2");
                mat.DisableKeyword("MASK4");
                mat.EnableKeyword("MASK3");
                break;
            case -1:
                mat.DisableKeyword("MASK3");
                mat.DisableKeyword("MASK5");
                mat.EnableKeyword("MASK4");
                break;
            case 0:
                mat.DisableKeyword("MASK4");
                mat.DisableKeyword("MASK6");
                mat.EnableKeyword("MASK5");
                break;
            case 1:
                mat.DisableKeyword("MASK5");
                mat.DisableKeyword("MASK7");
                mat.EnableKeyword("MASK6");
                break;
            case 2:
                mat.DisableKeyword("MASK6");
                mat.DisableKeyword("MASK8");
                mat.EnableKeyword("MASK7");
                break;
            case 3:
                mat.DisableKeyword("MASK7");
                mat.DisableKeyword("MASK9");
                mat.EnableKeyword("MASK8");
                break;
            case 4:
                mat.DisableKeyword("MASK8");
                mat.DisableKeyword("MASK10");
                mat.EnableKeyword("MASK9");
                break;
            case 5:
                mat.DisableKeyword("MASK10");
                mat.EnableKeyword("MASK11");
                break;
            default:
                Debug.Log("er is iets fout gegaan in de planet shader");
                break;

        }
    }
}
