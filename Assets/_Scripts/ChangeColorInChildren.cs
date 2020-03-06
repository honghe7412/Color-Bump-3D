using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorInChildren : MonoBehaviour
{
    public int numObjects = 1;
    public float time = 0.3f;
    public int currentIndex = 0;
    public float distanceToCamera = 20f;
    private Material obstacleMaterial, defaultMaterial;

    private void Start()
    {
        obstacleMaterial = GameController.instance.obstacleMaterial;
        defaultMaterial = GameController.instance.defaultMaterial;
    }

    private void Update()
    {
        if (transform.position.z - Camera.main.transform.position.z < distanceToCamera)
        {
            enabled = false;
            StartCoroutine(DoJob());
        }
    }

    private IEnumerator DoJob()
    {
        numObjects = Mathf.Clamp(numObjects, 0, transform.childCount);

        while (true)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                ChangeMaterial(i, defaultMaterial);
            }

            for (int i = currentIndex; i < currentIndex + numObjects; i++)
            {
                ChangeMaterial(i % transform.childCount, obstacleMaterial);
            }

            currentIndex = (currentIndex + numObjects) % transform.childCount;
            yield return new WaitForSeconds(time);
        }
    }

    private void ChangeMaterial(int index, Material material)
    {
        MeshRenderer[] meshRenderers = transform.GetChild(index).GetComponentsInChildren<MeshRenderer>();
        foreach(var renderer in meshRenderers)
        {
            renderer.material = material;
        }
    }
}
