using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class TreeScore : MonoBehaviour
{
    // Start is called before the first frame update
    public float scoreChange = 1f;
    public AudioClip broccoli;
    public GameObject treeCollisionParticles;
   // Manager manager;
    void Start()
    {
        //manager = GameObject.Find("Manager").GetComponent<Manager>();
        Manager.treeAmount += 1;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Manager.treeAmount -= 1;
            GameObject treeParticles = Instantiate(treeCollisionParticles, transform.position, transform.rotation);
            //treeParticles.transform.parent = transform;
            AudioSource.PlayClipAtPoint(broccoli, transform.position);
            AnimationStart.instance.StartAnimation();
            float mag = GetComponent<Power>().GetCurrentLevel() * 2 + 1f;
            CameraShaker.Instance.ShakeOnce(mag, 4f, .1f, 1f);
            Destroy(this.gameObject, 0.25f);
        }

        if (collision.gameObject.name == "EngeTree(Clone)" || collision.gameObject.name == "Broccoli(Clone)")
        {
            if (collision.transform.localScale.y > this.transform.localScale.y && collision.transform.localScale.y < 0.2)
            {
                collision.transform.localScale *= 1.01f;
                Destroy(this.gameObject);
            }

            if (collision.transform.localScale.y < this.transform.localScale.y)
            {
                Destroy(collision.gameObject);
                if (this.transform.localScale.y < 0.2) { this.transform.localScale *= 1.01f; }
            }
        }
    }
}
