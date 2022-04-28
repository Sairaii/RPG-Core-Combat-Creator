using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;

        void Awake() 
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        void Update() 
        {
            GetComponent<Text>().text = health.GetPercentage().ToString("F1"); //'0' next to F indicates that how many decimals we want to be shown on the output
        }
    }
}