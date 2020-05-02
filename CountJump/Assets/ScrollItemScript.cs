using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TypeItem
{
    PLATEFORME,
    BONUS
}

public class ScrollItemScript : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private Button button;

    [SerializeField] private EditorManagerScript editor;
    [SerializeField] private GameObject item;
    [SerializeField] private TypeItem type;

    private void Start()
    {
        button.onClick.AddListener(OnClickItem);
    }

    public void SetEditor(EditorManagerScript e)
    {
        editor = e;
    }

    public EditorManagerScript GetEditor()
    {
        return editor;
    }

    //action de clic sur le bouton
    public void OnClickItem()
    {
        editor.SetCurrentItem(item, type);
    }

    //récupérer le type d'item
    public TypeItem GetTypeItem()
    {
        return type;
    }
}
