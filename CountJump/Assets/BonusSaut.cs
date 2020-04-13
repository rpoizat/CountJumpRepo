using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusSaut : MonoBehaviour
{
    [SerializeField] private int nbSautBonus;
    [SerializeField] private Text text;

    private void Start()
    {
        text.text = "+" + nbSautBonus;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
