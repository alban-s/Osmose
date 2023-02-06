using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject[] chests;
    public int amount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() {
        List<GameObject> inactiveChests = new List<GameObject>();
        for (int i = 0; i < chests.Length; ++i) {
            inactiveChests.Add(chests[i]);
        }
        for (int i = 0; i < amount && inactiveChests.Count > 0; ++i) {
            GameObject chest = inactiveChests[Random.Range(0, inactiveChests.Count-1)];
            chest.SetActive(true);
            inactiveChests.Remove(chest);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
