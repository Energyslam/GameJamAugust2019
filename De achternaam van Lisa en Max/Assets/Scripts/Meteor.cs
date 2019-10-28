using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public ParticleSystem meteorParticles;
    public GameObject particleSpawnLocation;
    public AudioClip meteorImpact;
    GameObject player;
    bool hasPlayed;
    Quaternion particleRotation = new Quaternion(0f, 90f, 0f, 0f);
    void Start()
    {
        player = GameObject.Find("Player");   
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Player")
        {
            Destroy(this.gameObject);
        }
        if (hasPlayed == false)
        {
            meteorParticles.Play();
            GameObject tempGO = new GameObject("tempMeteorImpact");
            AudioSource tempAudioSource = tempGO.AddComponent<AudioSource>();
            tempGO.AddComponent<DestroySelf>();
            tempAudioSource.playOnAwake = false;
            tempAudioSource.clip = meteorImpact;
            tempAudioSource.volume = 0.3f;
            //tempAudioSource.spatialBlend = 0.5f;
            // tempAudioSource.minDistance = 1f;
            tempAudioSource.Play();
            float distancePlayer = (player.transform.position - this.transform.position).magnitude;
            Debug.Log("Distance to player" + distancePlayer);
            CameraShaker.Instance.ShakeOnce((50f-distancePlayer), (8f - distancePlayer /10), .1f, 2f);
            //Instantiate(tempGO, particleSpawnLocation.transform.position, Quaternion.identity);
            Destroy(this.gameObject, 1f);
        }
        hasPlayed = true;
            //AudioSource.PlayClipAtPoint(meteorImpact, particleSpawnLocation.transform.position);
        if (collision.gameObject.name == "Broccoli(Clone)" || collision.gameObject.name == "Factory(Clone)" || collision.gameObject.name == "BatteryModel Variant(Clone)" || collision.gameObject.name == "EngeTree(Clone)")
        {
            Destroy(collision.gameObject);
        }
    }
}
