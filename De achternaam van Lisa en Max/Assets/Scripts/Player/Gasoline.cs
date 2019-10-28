using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gasoline : MonoBehaviour
{
    public float gasAmount = 50;
    public float maxGasAmount = 100;
    public float isThisLoss = 0.05f;

    private int bigCount, smallCount;

    public GameObject smallTrail1, smallTrail2, bigTrail1, bigTrail2;

    public AudioSource rev;

    public Image batteryUI;

    public float damage;

    public void AddGas(float value)
    {
        gasAmount += value;
    }

    private void Update()
    {
        if (gasAmount > maxGasAmount)
        {
            gasAmount = maxGasAmount;
        }

        batteryUI.fillAmount = gasAmount / maxGasAmount;

        if (gasAmount > 0.1f)
        {
            gasAmount -= isThisLoss;

            if (smallCount == 0)
            {
                StopAllCoroutines();
                smallCount++;
            }

            smallTrail1.SetActive(true);
            smallTrail2.SetActive(true);

            StartCoroutine(SmallOn());
        }

        else 
        {
            if (bigCount == 0)
            {
                StopAllCoroutines();
                bigCount++;
            }

            bigTrail1.SetActive(true);
            bigTrail2.SetActive(true); 

            StartCoroutine(BigOn());
            GreenMeter.instance.CalculateAddToGreenAmount(damage);

            if (!rev.isPlaying)
            {
                rev.Play();
            }
        }
    }

    private IEnumerator BigOn()
    {
        yield return new WaitForSeconds(3);

        smallTrail1.SetActive(false);
        smallTrail2.SetActive(false);

        smallCount = 0;
    }

    private IEnumerator SmallOn()
    {
        if (rev.isPlaying)
        {
            rev.Stop();
        }

        yield return new WaitForSeconds(0);

        bigTrail1.SetActive(false);
        bigTrail2.SetActive(false);

        bigCount = 0;
    }
}
