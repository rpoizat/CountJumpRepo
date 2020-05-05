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
    }

    public float getValueBonus()
    {
        return newJumpSpeed;
    }

    public void SetPlayer(PlayerControleurScript p)
    {
        player = p;
    }

    public void instantiateValueBonus(int laValeur)
    {
        newJumpSpeed = laValeur;
    }

    public void Update()
    {
        text.text = "+" + newJumpSpeed;
    }
}
