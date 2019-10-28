using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    public List<float> power;
    private int level;

    [SerializeField]
    private float currentPower;

    private void Start()
    {
        currentPower = power[(int)currentPower];
    }

    public void Upgrade()
    {
        level++;
        currentPower = power[level];
    }

    public float GetCurrentPower()
    {
        return currentPower;
    }

    public float GetCurrentLevel()
    {
        return level;
    }

    private void FixedUpdate()
    {
        GreenMeter.instance.CalculateAddToGreenAmount(currentPower);
    }


}
