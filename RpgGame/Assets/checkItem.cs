using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkItem : MonoBehaviour
{

    // ID de l'item actuel
    public int itemID;

    // Membre de notre personnage
    public GameObject bodyPart;

    // Liste de nos items (Objets se trouvant dans le membre de notre personnage)
    [SerializeField]
    public List<GameObject> itemList = new List<GameObject>();

    void Update()
    {
        if (transform.childCount > 0)
        {
            itemID = gameObject.GetComponentInChildren<ItemOnObject>().item.itemID;
        }
        else
        {
            itemID = 0;

            for (int i = 0; i < itemList.Count; i++)
            {
                itemList[i].SetActive(false);
            }
        }

        //Si le jeu d�tecte plusieurs items dans le membre du personnage on les d�sactives tous sauf celui ou ceux "r�ellement �quip�s"
        if (bodyPart.transform.childCount > 1)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                itemList[i].SetActive(false);
            }
        }

        // Copier / Coller le bloc suivant pour chacun de vos items
        // itemID correspond � l'ID de l'item dans la BDD
        // i = X correspond � l'ID (ou index) de l'item dans la liste

        // //glaive royal
        if (itemID == 1 && transform.childCount > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == 0)
                {
                    itemList[i].SetActive(true);
                }

            }
        }

        // gantelets en fer
        if (itemID == 3 && transform.childCount > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == 0 || i == 1)
                {
                    itemList[i].SetActive(true);
                }
            }
        }


        // casque en fer
        if (itemID == 2 && transform.childCount > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == 0)
                {
                    itemList[i].SetActive(true);
                }
            }
        }
    }
}
