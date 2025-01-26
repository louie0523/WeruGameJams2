using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconAllow : MonoBehaviour
{
    public GameObject myIcon;

    private Renderer[] renderers;

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    private void Update()
    {
        if (myIcon.activeSelf)
        {
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = true;
            }
        }
    }
}
