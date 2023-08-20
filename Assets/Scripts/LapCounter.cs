using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LapCounter : MonoBehaviour
{
    private int Lap_number = 0;

    public Text lapText;


    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Lap_number++;
            lapText.text ="Lap: " + Lap_number.ToString();
        }
    }
}
