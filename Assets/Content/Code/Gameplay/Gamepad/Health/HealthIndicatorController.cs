using System;
using System.Collections;
using System.Collections.Generic;
using Content.Code.Gameplay.Gamepad;
using UnityEngine;

public class HealthIndicatorController : MonoBehaviour, IHealthIndicatorController
{
    [SerializeField] private Renderer[] orderedHealthIndicators;

    public void SetHealthIndicator(int health)
    {
        Debug.Log("Setting health indicator to " + health);
        
        for (int i = 0; i < orderedHealthIndicators.Length; i++)
        {
            if (i < health)
            {
                Debug.Log("Setting health indicator " + i + " to green");
                orderedHealthIndicators[i].material.SetColor("_EmissionColor", new Color(0.27f, 0.59f, 0.13f) * 1.5f);
                orderedHealthIndicators[i].material.EnableKeyword("_EMISSION");

            }
            else
            {
                orderedHealthIndicators[i].material.SetColor("_EmissionColor", Color.black);
                orderedHealthIndicators[i].material.EnableKeyword("_EMISSION");

            }
        }
    }
}
