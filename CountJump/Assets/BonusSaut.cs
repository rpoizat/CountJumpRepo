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
        if(other.gameObject.name == "Joueur")
        {
            PlayerControleurScript joueur = other.GetComponent<PlayerControleurScript>();
            joueur.AddJumps(nbSautBonus);
        }
        gameObject.SetActive(false);
    }

    public void setValueBonus(int laValeur)
    {
        nbSautBonus = nbSautBonus + laValeur;
        Debug.Log("On augmente le nombre de saut du bonus");
    }

    public void Update()
    {
        text.text = "+" + nbSautBonus;
    }
}
