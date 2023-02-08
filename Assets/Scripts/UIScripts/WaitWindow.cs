using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using Osmose.Gameplay;

public class WaitWindow : MonoBehaviour
{
    public GameObject blueCountText;
    public GameObject redCountText;
    public GameObject remainingText;

    public GameObject startButton;

    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();   
    }

    // Update is called once per frame
    void Update()
    {
         blueCountText.GetComponent<TextMeshProUGUI>().text = manager.GetBlueCount().ToString() + " joueurs";
         redCountText.GetComponent<TextMeshProUGUI>().text = manager.GetRedCount().ToString() + " joueurs";
         int remaining = manager.GetMaxNbOfPlayer() - manager.GetTotalPlayerCount();
         if(remaining == 0){
            remainingText.GetComponent<TextMeshProUGUI>().text = "Ready to Start";
            startButton.GetComponent<Button>().interactable = true;
        }else{
            remainingText.GetComponent<TextMeshProUGUI>().text = "en attente de " + remaining + " joueurs";
            startButton.GetComponent<Button>().interactable = false;
        }
         
    }

    public void onStartButtonClicked(){

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        //WAIT

        GameObject localPlayer = NetworkClient.localPlayer.gameObject;
        localPlayer.GetComponent<PlayerController>().enabled = true;
        localPlayer.GetComponent<PlayerMotor>().enabled = true;

        gameObject.SetActive(false);

    }
}
