using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTree : MonoBehaviour
{
    public int numObjects;

    public float xScale;
    public float yScale;
    public float meteorScale;

    public float treeDelay;
    public float engeTreeDelay;
    public float factoryDelay;
    public float batteryDelay;
    public float meteorDelay;
    public float firstMeteorDelay;
    
    [SerializeField]
    GameObject treePrefab;
    [SerializeField]
    GameObject factoryPrefab;
    [SerializeField]
    GameObject batteryPrefab;
    [SerializeField]
    GameObject meteorPrefab;
    [SerializeField]
    GameObject engeTreePrefab;

    void Start()
    {
        StartCoroutine(spawnObject(treePrefab, treeDelay));
        StartCoroutine(spawnObject(engeTreePrefab, engeTreeDelay));
        StartCoroutine(spawnObject(factoryPrefab, factoryDelay));
        StartCoroutine(spawnObject(batteryPrefab, batteryDelay));
        StartCoroutine(firstMeteor());
    }

    IEnumerator firstMeteor()
    {
        yield return new WaitForSeconds(firstMeteorDelay);
        StartCoroutine(spawnObject(meteorPrefab, meteorDelay));
    }

    IEnumerator spawnObject(GameObject prefab, float delay)
    {
        Vector3 spawnPosition = Vector3.zero;
        if (prefab == meteorPrefab)
        {
            if (Manager.trees.Count == 0)
            {
                spawnPosition = Random.onUnitSphere * ((this.transform.localScale.x / xScale) + prefab.transform.localScale.y * yScale * meteorScale) + this.transform.position;
            }
            else { spawnPosition = (Manager.trees[Random.Range(0, Manager.trees.Count)].transform.position - transform.position).normalized * yScale * meteorScale; }//Random.onUnitSphere * ((this.transform.localScale.x / xScale) + prefab.transform.localScale.y * yScale * meteorScale) + this.transform.position;
        } else { spawnPosition = Random.onUnitSphere * ((this.transform.localScale.x / xScale) + prefab.transform.localScale.y * yScale) + this.transform.position; }
            Quaternion spawnRotation = Quaternion.identity;
            GameObject newTree = Instantiate(prefab, spawnPosition, spawnRotation) as GameObject;
            newTree.transform.parent = transform;
            newTree.transform.LookAt(this.transform);
            newTree.transform.Rotate(-90, 0, 0);
            newTree.AddComponent<FauxGravityBodyTree>();
            newTree.GetComponent<FauxGravityBodyTree>().attractor = GetComponent<FauxGravityAttractorTree>();

        if (prefab == treePrefab)
            Manager.trees.Add(newTree);

        if (prefab == engeTreePrefab)
            Manager.trees.Add(newTree);

        else if (prefab == factoryPrefab)
            Manager.factories.Add(newTree);

        yield return new WaitForSeconds(delay);

        if (prefab == treePrefab || prefab == engeTreePrefab)
        {
            if (Manager.trees.Count >= 25)
            {
                StartCoroutine(checkTrees());
                yield break;
            }
        }
        if (prefab == meteorPrefab)
        {
            StartCoroutine(spawnObject(prefab, Random.Range(meteorDelay * 0.5f, meteorDelay * 1.5f)));
        }

        else
        { 
            StartCoroutine(spawnObject(prefab, delay));
        }

    }

    IEnumerator checkTrees()
    {
        yield return new WaitForSeconds(10f);
        if (Manager.trees.Count < 25)
        {
            StartCoroutine(spawnObject(treePrefab, treeDelay * 5f));
        } else { StartCoroutine(checkTrees()); }

    }
}
