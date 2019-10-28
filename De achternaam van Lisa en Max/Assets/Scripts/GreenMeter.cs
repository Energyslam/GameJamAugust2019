using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GreenMeter : MonoBehaviour
{
    [SerializeField]
    private float currentGreenAmount;
    public float minGreen, maxGreen, minToxic, maxToxic;
    private float totalAchievable;
    private bool startDecreasing = false;
    private int totalObjects = 0;

    bool hasWon;
    bool hasLost;
    bool fadeOutStart;

    [SerializeField]
    Animator animator;

    public static GreenMeter instance;

    private void Start()
    {
        instance = this;
        totalAchievable = maxGreen - minToxic;
        StartCoroutine(StartDecreasing());
    }

    private void FixedUpdate()
    {
        if (!startDecreasing)
        {
            currentGreenAmount = 0;
        }
        else
        {
            currentGreenAmount = Mathf.Clamp(currentGreenAmount, minToxic, maxGreen);
        }

        TransitionScreen();
    }

    private void TransitionScreen()
    {
        if (currentGreenAmount == minToxic)
        {
            animator.SetBool("ChangeScene", true);
            hasLost = true;
        }
        else if (currentGreenAmount == maxGreen)
        {
            animator.SetBool("ChangeScene", true);
            hasWon = true;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("1") && !fadeOutStart)
        {
            Debug.Log("fadeoutstart true");
            fadeOutStart = true;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && fadeOutStart)
        {
            Debug.Log("SLOEP");
            if (hasLost)
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                SceneManager.LoadScene("WinScreen");
                GameManager.instance.UpdateMaterial();
            }
        }
    }

    public float GetCurrentGreenAmount()
    {
        return currentGreenAmount;
    }

    public void CalculateAddToGreenAmount (float power)
    {
        //Debug.Log("Current Green" + currentGreenAmount);
        float dimReturns = 1f;
        if (startDecreasing)
            if (power < 0 && currentGreenAmount < 0)
            {
                //dimReturns = Mathf.Clamp(1f - Mathf.Abs(currentGreenAmount / 100f), 0.6f, 1f);
            }
            else if ( power > 0 && currentGreenAmount > 0)
            {
                dimReturns = Mathf.Clamp(1f - Mathf.Abs(currentGreenAmount / 100f / 1.7f), 0.4f, 1f);
            }
            currentGreenAmount += (power / 100) * (dimReturns);
        /*
       dimReturns = 1f-Mathf.Abs(currentGreenAmount / 100);
       currentGreenAmount += (power / 100) * (dimReturns);
       */

    }

    IEnumerator StartDecreasing()
    {
        yield return new WaitForSeconds(10);
        startDecreasing = true;
    }
}
