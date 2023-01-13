using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinGame : MonoBehaviour
{
    public GameObject thisWindow;
    public GameObject persoSelectionWindow;

    private string serveurURL = "monURL";

    public void AddServeurUrl(string URL)
    {
        serveurURL = URL;
    }

    public void JoinServeur()
    {
        print(serveurURL);
        thisWindow.SetActive(false);
        //persoSelectionWindow.SetActive(true);
    }

    public void ReturnMenu()
    {
        thisWindow.SetActive(false);
    }
}
