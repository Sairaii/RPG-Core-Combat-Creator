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
            GetComponent<Text>().text = health.GetHealthPoints().ToString("F0") + " / " + health.GetMaxHealth().ToString("F0"); //'0' next to F indicates that how many decimals we want to be shown on the output
        }
    }
}
