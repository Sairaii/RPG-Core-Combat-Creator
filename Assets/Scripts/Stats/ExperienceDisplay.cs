using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;

        void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        void Update()
        {
            GetComponent<Text>().text = experience.GetPoints().ToString("F0") + " / " + experience.GetMaxExperience().ToString("F0"); //'0' next to F indicates that how many decimals we want to be shown on the output
        }
    }
}
