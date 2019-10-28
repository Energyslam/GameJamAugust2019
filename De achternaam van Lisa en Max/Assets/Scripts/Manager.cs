using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;

    public Image green, red;
    public Text tree, fact;
    static public List<GameObject> trees = new List<GameObject>();
    static public List<GameObject> factories = new List<GameObject>();

    static public int treeAmount = 0;
    static public int factoryAmount = 0;

    [SerializeField]
    private int treesc, factoriesc;

    void FixedUpdate()
    {
        treesc = trees.Count;
        factoriesc = factories.Count;

        tree.text = "x " + treesc.ToString();
        fact.text = "x " + factoriesc.ToString();

        for (int i = 0; i < trees.Count - 1; i++)
        {
            if (trees[i] == null)
            {
                trees.RemoveAt(i);
            }
        }

        for (int i = 0; i < factories.Count - 1; i++)
        {
            if (factories[i] == null)
            {
                factories.RemoveAt(i);
            }
        }

        if (GreenMeter.instance.GetCurrentGreenAmount() > 0)
        {
            red.fillAmount = 0;
            green.fillAmount = GreenMeter.instance.GetCurrentGreenAmount() / 100;
            
        }
        else if (GreenMeter.instance.GetCurrentGreenAmount() < 0)
        {
            green.fillAmount = 0;
            float value = GreenMeter.instance.GetCurrentGreenAmount() / 100;
            value = -value;
            red.fillAmount = value;
        }
        else
        {
            green.fillAmount = 0;
            red.fillAmount = 0;
        }
    }
}
