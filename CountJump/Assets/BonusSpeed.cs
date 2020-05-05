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

    public void setValueBonus(int laValeur)
    {
        speed = speed + laValeur;
    }

    public void instantiateValueBonus(int laValeur)
    {
        speed =  laValeur;
    }

    public float getValueBonus()
    {
        return speed;
    }

    public void Update()
    {
        text.text = "+" + speed;
    }
}
