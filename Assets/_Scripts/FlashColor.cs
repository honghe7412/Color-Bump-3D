using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashColor : MonoBehaviour {

    public float time = 0.2f;

    public float distanceToCamera = -1f;
    public float distanceToPlayer = -1f;

    private Material obstacleMaterial, defaultMaterial;

    private void Start()
    {
        obstacleMaterial = GameController.instance.obstacleMaterial;
        defaultMaterial = GameController.instance.defaultMaterial;
    }

    private void Update()
    {
        if (distanceToCamera != -1 && transform.position.z - Camera.main.transform.transform.position.z < distanceToCamera ||
            distanceToPlayer != -1 && transform.position.z - PlayerController.instance.transform.position.z < distanceToPlayer)
        {
            enabled = false;
            StartCoroutine(DoJob());
        }
    }

    private int index;
    private IEnumerator DoJob()
    {
        while (true)
        {
            ChangeMaterial(index % 2 == 0 ? obstacleMaterial : defaultMaterial);
            index++;
            yield return new WaitForSeconds(time);
        }
    }

    private void ChangeMaterial(Material material)
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in meshRenderers)
        {
            renderer.material = material;
        }
    }
}
