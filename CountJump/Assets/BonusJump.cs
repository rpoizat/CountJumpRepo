using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusJump : MonoBehaviour
{
    [SerializeField] private float newJumpSpeed;
    [SerializeField] private Text text;
    [SerializeField] private PlayerControleurScript player;

    private void Start()
    {
        text.text = "+" + newJumpSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        player.setJumpSpeed(newJumpSpeed);
        gameObject.SetActive(false);
    }

    public void setValueBonus(int laValeur)
    {
        newJumpSpeed = newJumpSpeed + laValeur;
        Debug.Log("On augmente la valeur de la force de saut du bonus");
    }

    public void SetPlayer(PlayerControleurScript p)
    {
        player = p;
    }

    public void Update()
    {
        text.text = "+" + newJumpSpeed;
    }
}
