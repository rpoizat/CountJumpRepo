using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusSpeed : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private float speed;

    private void Start()
    {
        text.text = "+" + speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Joueur")
        {
            var joueur = other.GetComponent<PlayerControleurScript>();
            joueur.AddSpeed(speed);
            gameObject.SetActive(false);
        }
    }
}
