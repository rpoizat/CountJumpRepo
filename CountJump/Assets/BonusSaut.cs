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
    }

    public int getValueBonus()
    {
        return nbSautBonus;
    }

    public void Update()
    {
        text.text = "+" + nbSautBonus;
    }

    public void instantiateValueBonus(int laValeur)
    {
        nbSautBonus = laValeur;
    }
}
