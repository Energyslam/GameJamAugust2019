using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryScore : MonoBehaviour
{
    public float scoreChange = -1f;
    public AudioClip kaboom;
    bool canPlay;
    // Manager manager;
    void Start()
    {
        // manager = GameObject.Find("Manager").GetComponent<Manager>();
        canPlay = true;
        Manager.factoryAmount = Manager.factoryAmount + 1;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Manager.factoryAmount -= 1;
            if (canPlay) { AudioSource.PlayClipAtPoint(kaboom, transform.position); }
            canPlay = false;
            AnimationStart.instance.StartAnimation();
            float mag = GetComponent<Power>().GetCurrentLevel() * 2 + 1f;
            CameraShaker.Instance.ShakeOnce(mag, 4f, .1f, 1f);
            Destroy(this.gameObject, 0.25f);
        } else if(collision.gameObject.name == "Broccoli(Clone)" || collision.gameObject.name == "EngeTree(Clone)")
        {
            Destroy(collision.gameObject);
        } else if (collision.gameObject.name == "Factory(Clone)")
        {
            if (collision.transform.localScale.y > this.transform.localScale.y && collision.transform.localScale.y < 0.2)
            {
                collision.transform.localScale *= 1.01f;
                Destroy(this.gameObject);
            }
            if (collision.transform.localScale.y < this.transform.localScale.y) {
                Destroy(collision.gameObject);
                if (this.transform.localScale.y < 0.2) { this.transform.localScale *= 1.01f; }
            }
        }
    }
}
