using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBodyTree : MonoBehaviour
{
    public FauxGravityAttractorTree attractor;
    private Transform myTransform;
    private Power power;
    public int level;
    int baseScale;
    void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().useGravity = false;
        myTransform = transform;
        power = GetComponent<Power>();
        if (this.gameObject.name == "Factory(Clone)")
        {
            baseScale = Mathf.FloorToInt(transform.localScale.x * 100);
        }
        else
        {
            baseScale = Mathf.FloorToInt(transform.localScale.x * 1000);
        }
    }

    void FixedUpdate()
    {
        attractor.Attract(myTransform);

        if (GetComponent<Power>())
        {
            if (this.gameObject.name == "Factory(Clone)")
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x + 0.00001f, this.transform.localScale.y + 0.00001f, this.transform.localScale.z + 0.00001f);
                level = Mathf.FloorToInt(transform.localScale.x * 100 - baseScale);
            }
            else if (this.gameObject.name == "EngeTree(Clone)")
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x + 0.000002f, this.transform.localScale.y + 0.000002f, this.transform.localScale.z + 0.000002f);
                level = Mathf.FloorToInt(transform.localScale.x * 1000 - baseScale);
            }
            else
            {   this.transform.localScale = new Vector3(this.transform.localScale.x + 0.000001f, this.transform.localScale.y + 0.000001f, this.transform.localScale.z + 0.000001f);
                level = Mathf.FloorToInt(transform.localScale.x * 1000 - baseScale);
            }

            if (power.GetCurrentLevel() < level && power.GetCurrentLevel() < power.power.Count - 1)
            {
                power.Upgrade();
            }
        }
    }
}
