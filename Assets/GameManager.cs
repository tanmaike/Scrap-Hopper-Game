using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {

    public GameObject platformPrefab;
    public GameObject fuelPrefab1;
    public GameObject fuelPrefab2;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject gemStonePrefab;
    public TMP_Text fuelText;

    public int platformCount = 300;
    public float fuelPercent {
        get { return _feuPer; }
        set { _feuPer = Mathf.Clamp(value, 0, 100); }
    }
    [SerializeField, Range(0, 100)] private float _feuPer;

    public int gemstoneCount = 0;

    // Start is called before the first frame update
    void Start() {
        fuelPercent = 100;
        Vector3 spawnPosition = new Vector3();
        for (int i = 0; i < platformCount; i++) {
            spawnPosition.y += Random.Range(1.5f, 3.25f);
            spawnPosition.x = Random.Range(-8f, 8f);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

            // fuel spawning
            if(Random.Range(1, 8) > 4) {
                Vector3 fuelPosition = spawnPosition + new Vector3(0, 1f, 0);
                if (Random.Range(1, 5) == 1) {
                    Instantiate(fuelPrefab2, fuelPosition, Quaternion.identity);
                }
                else {
                    Instantiate(fuelPrefab1, fuelPosition, Quaternion.identity);
                }
            }

            // enemy1 spawning
            if(Random.Range(1, 10) > 8) {
                Vector3 enemy1Position = spawnPosition;
                enemy1Position.y += Random.Range(2f, 4f);
                enemy1Position.x = Random.Range(-5f, 5f);
                Instantiate(enemy1Prefab, enemy1Position, Quaternion.identity);
            }

            // enemy2 (platform spikes) spawning
            if(Random.Range(1, 10) > 7) {
                Vector3 spikePosition = spawnPosition;
                spikePosition.y += Random.Range(0f, 5f);
                spikePosition.x = Random.Range(-9f, 9f);
                Instantiate(enemy2Prefab, spikePosition, Quaternion.identity);
            }

            // Gemstone spawning
            if(Random.Range(1, 20) == 10) {
                Vector3 gemPosition = spawnPosition;
                gemPosition.y += Random.Range(0f, 5f);
                gemPosition.x = Random.Range(-8f, 8f);
                Instantiate(gemStonePrefab, gemPosition, Quaternion.identity);
            }
        }
    }

    void Update() {
        fuelPercent -= Time.deltaTime * 3.25f;
        fuelText.text = fuelPercent.ToString("0") + "%\n" + gemstoneCount.ToString("0") + " / 10";
    }
}
