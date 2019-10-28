using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Battery : MonoBehaviour
{
    float hoverValue = 1.0f;
    public float batteryValue = 50;
    public AudioClip batterySound;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<Gasoline>().AddGas(batteryValue);
            CameraShaker.Instance.ShakeOnce(2f, 2f, .5f, .5f);
            AudioSource.PlayClipAtPoint(batterySound, transform.position, 0.3f);
            Destroy(gameObject);
        }
    }

    /* Update is called once per frame
    void FixedUpdate()
    {
        //this.transform.localPosition = new Vector3(0, Mathf.Sin(Time.time * hoverValue), 0);

        Vector3 currentRot = transform.localRotation.eulerAngles;
        currentRot.y++;
        currentRot.z++;
        transform.localRotation = Quaternion.Euler(currentRot);

    } */
}
