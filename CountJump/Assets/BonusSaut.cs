using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSaut : MonoBehaviour
{
    [SerializeField] private int nbSautBonus;

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
