using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;

public class Manager : MonoBehaviour
{

    // Prefabs
    public GameObject RoverPrefab;
    public GameObject TowerPrefab;
    public GameObject LandmarkPrefab;

    // GameObject Array
    public GameObject[] rovers;
    public GameObject[] towers;
    public GameObject[] landmarks;

    // World Grid
    public int mapsize;
    public int nagent;
    public int nlandmark;
    public Color[] colors;

    void Start()
    {
        mapsize = 17;
        nagent = 4;
        nlandmark = 6;

        // Landmark Colors
        colors = new Color[nlandmark];
        colors[0] = new Color(192/255f, 0/255f, 0/255f);
        colors[1] = new Color(255/255f, 192/255f, 0/255f);
        colors[2] = new Color(0/255f, 176/255f, 80/255f);
        colors[3] = new Color(0/255f, 176/255f, 240/255f);
        colors[4] = new Color(0/255f, 112/255f, 192/255f);
        colors[5] = new Color(112/255f, 48/255f, 160/255f);

        rovers = new GameObject[nagent];
        towers = new GameObject[nagent];
        landmarks = new GameObject[nlandmark];

        // Set Landmarks
        for (int i = 0; i < nlandmark; i++) {
            int x = Random.Range(0, mapsize);
            int z = Random.Range(0, mapsize);

            GameObject temp = Instantiate(LandmarkPrefab);
            temp.transform.position = new Vector3(x - (mapsize / 2f), 0.01f, z - (mapsize / 2f));
            temp.name = "Landmark" + i.ToString();
            temp.GetComponent<MeshRenderer>().material.color = colors[i];
            landmarks[i] = temp;
        }

        // Rovers
        for (int i = 0; i < nagent; i++) {
            int x = Random.Range(0, mapsize);
            int z = Random.Range(0, mapsize);

            GameObject temp = Instantiate(RoverPrefab);
            temp.transform.position = new Vector3(x - (mapsize / 2f), 0.1f, z - (mapsize / 2f));
            temp.name = "Rover" + i.ToString();
            temp.GetComponent<RoverAgent>().nagent = nagent;
            temp.GetComponent<RoverAgent>().nlandmark = nlandmark;
            rovers[i] = temp;
        }

        // Towers
        for (int i = 0; i < nagent; i++) {
            int x = Random.Range(0, mapsize);
            int z = Random.Range(0, mapsize);

            GameObject temp = Instantiate(TowerPrefab);
            temp.transform.position = new Vector3(x - (mapsize / 2f), 0.3f, z - (mapsize / 2f));
            temp.name = "Tower" + i.ToString();
            temp.GetComponent<TowerAgent>().nagent = nagent;
            temp.GetComponent<TowerAgent>().nlandmark = nlandmark;

            // Add Academy Script to First Tower Agent
            if (i == 0) {
                temp.AddComponent<RoverTowerAcademy>();
            }

            towers[i] = temp;
        }

        // Shuffle
        for (int i = 0; i < nagent; i++) {
            int r = Random.Range(0, nlandmark);

            rovers[i].GetComponent<RoverAgent>().Landmark = landmarks[r];
            rovers[i].GetComponent<RoverAgent>().Tower = towers[i];
            rovers[i].GetComponent<MeshRenderer>().material.color = landmarks[r].GetComponent<MeshRenderer>().material.color;
            rovers[i].GetComponent<RoverAgent>().idx = i;

            towers[i].GetComponent<TowerAgent>().Landmark = landmarks[r];
            towers[i].GetComponent<TowerAgent>().Rover = rovers[i];
            towers[i].GetComponent<MeshRenderer>().material.color = landmarks[r].GetComponent<MeshRenderer>().material.color;
            towers[i].GetComponent<TowerAgent>().idx = i;
        }

        InvokeRepeating("InitWorld", 3f, 3f);
    }

    void Update()
    {
        
    }

    public void InitWorld() 
    {
        // Set Pair Randomly
        for (int i = 0; i < nagent; i++) {
            int r1 = Random.Range(0, nagent);
            int r2 = Random.Range(0, nagent);

            GameObject temp = rovers[r1];
            rovers[r1] = rovers[r2];
            rovers[r2] = temp;
        }

        for (int i = 0; i < nagent; i++) {
            int r1 = Random.Range(0, nagent);
            int r2 = Random.Range(0, nagent);

            GameObject temp = towers[r1];
            towers[r1] = towers[r2];
            towers[r2] = temp;
        }

        // Set Position
        for (int i = 0; i < nagent; i++) {
                int x = Random.Range(0, mapsize);
                int z = Random.Range(0, mapsize);

                rovers[i].transform.position = new Vector3(x - (mapsize / 2f), 0.1f, z - (mapsize / 2f));
                rovers[i].name = "Rover" + i.ToString();
        }

        for (int i = 0; i < nagent; i++) {
                int x = Random.Range(0, mapsize);
                int z = Random.Range(0, mapsize);
            
                towers[i].transform.position = new Vector3(x - (mapsize / 2f), 0.3f, z - (mapsize / 2f));
                towers[i].name = "Tower" + i.ToString();
        }

        for (int i = 0; i < nlandmark; i++) {
            int x = Random.Range(0, mapsize);
            int z = Random.Range(0, mapsize);

            landmarks[i].transform.position = new Vector3(x - (mapsize / 2f), 0.01f, z - (mapsize / 2f));
        }

        // Shuffle
        for (int i = 0; i < nagent; i++) {
            int r = Random.Range(0, nlandmark);

            rovers[i].GetComponent<RoverAgent>().Landmark = landmarks[r];
            rovers[i].GetComponent<RoverAgent>().Tower = towers[i];
            rovers[i].GetComponent<MeshRenderer>().material.color = landmarks[r].GetComponent<MeshRenderer>().material.color;
            rovers[i].GetComponent<RoverAgent>().idx = i;

            towers[i].GetComponent<TowerAgent>().Landmark = landmarks[r];
            towers[i].GetComponent<TowerAgent>().Rover = rovers[i];
            towers[i].GetComponent<MeshRenderer>().material.color = landmarks[r].GetComponent<MeshRenderer>().material.color;
            towers[i].GetComponent<TowerAgent>().idx = i;
        }
    }
}
