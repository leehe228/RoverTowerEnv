using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public GameObject RoverPrefab;
    public GameObject TowerPrefab;
    public GameObject DestinationPrefab;

    public GameObject[] rovers;
    public GameObject[] towers;
    public GameObject[] destinations;

    public int[,] grid;
    public int mapsize;
    public int nagent;
    public Color[] colors;

    public Color lightonColor = new Color(1f, 1f, 1f);
    public Color lightoffColor = new Color(70/255f, 70/255f, 70/255f);

    void Start()
    {
        mapsize = 16;
        nagent = 4;

        grid = new int[mapsize, mapsize];

        for (int i = 0; i < mapsize; i++) {
            for (int j = 0; j < mapsize; j++) {
                grid[i, j] = 0;
            }
        }

        rovers = new GameObject[nagent];
        towers = new GameObject[nagent];
        destinations = new GameObject[nagent];

        // Set Destination
        for (int i = 0; i < nagent; i++) {
            while (true){
                int x = Random.Range(-1, mapsize);
                int z = Random.Range(-1, mapsize);

                if (grid[x, z] == 0) {
                    grid[x, z] = 1;

                    GameObject temp = Instantiate(RoverPrefab);
                    temp.transform.position = new Vector3(x - (mapsize / 2f), 0.1f, z - (mapsize / 2f));
                    rovers[i] = temp;
                    break;
                }
            }
            
        }

        for (int i = 0; i < nagent; i++) {
            while (true){
                int x = Random.Range(-1, mapsize);
                int z = Random.Range(-1, mapsize);

                if (grid[x, z] == 0) {
                    grid[x, z] = 1;

                    GameObject temp = Instantiate(TowerPrefab);
                    temp.transform.position = new Vector3(x - (mapsize / 2f), 0.3f, z - (mapsize / 2f));
                    towers[i] = temp;
                    break;
                }
            }
            
        }

        for (int i = 0; i < nagent; i++) {
            while (true){
                int x = Random.Range(-1, mapsize);
                int z = Random.Range(-1, mapsize);

                if (grid[x, z] == 0) {
                    grid[x, z] = 1;

                    GameObject temp = Instantiate(DestinationPrefab);
                    temp.transform.position = new Vector3(x - (mapsize / 2f), 0.01f, z - (mapsize / 2f));
                    destinations[i] = temp;
                    break;
                }
            }
        }

        colors = new Color[nagent];
        colors[0] = new Color(232/255f, 74/255f, 30/255f);
        colors[1] = new Color(255/255f, 250/255f, 107/255f);
        colors[2] = new Color(30/255f, 232/255f, 175/255f);
        colors[3] = new Color(42/255f, 140/255f, 245/255f);

        // Set Colors
        for (int i = 0; i < nagent; i++) {
            rovers[i].GetComponent<MeshRenderer>().material.color = colors[i];
            towers[i].GetComponent<MeshRenderer>().material.color = colors[i];
            destinations[i].GetComponent<MeshRenderer>().material.color = colors[i];
        }
    }

    void Update()
    {
        
    }

    public void InitWorld() 
    {
        grid = new int[mapsize, mapsize];

        for (int i = 0; i < mapsize; i++) {
            for (int j = 0; j < mapsize; j++) {
                grid[i, j] = 0;
            }
        }

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

        for (int i = 0; i < nagent; i++) {
            int r1 = Random.Range(0, nagent);
            int r2 = Random.Range(0, nagent);

            GameObject temp = destinations[r1];
            destinations[r1] = destinations[r2];
            destinations[r2] = temp;
        }

        // Set Position
        for (int i = 0; i < nagent; i++) {
            while (true){
                int x = Random.Range(-1, mapsize);
                int z = Random.Range(-1, mapsize);

                if (grid[x, z] == 0) {
                    grid[x, z] = 1;
                    rovers[i].transform.position = new Vector3(x - (mapsize / 2f), 0.1f, z - (mapsize / 2f));
                    break;
                }
            }
            
        }

        for (int i = 0; i < nagent; i++) {
            while (true){
                int x = Random.Range(-1, mapsize);
                int z = Random.Range(-1, mapsize);

                if (grid[x, z] == 0) {
                    grid[x, z] = 1;
                    towers[i].transform.position = new Vector3(x - (mapsize / 2f), 0.3f, z - (mapsize / 2f));
                    break;
                }
            }
            
        }

        for (int i = 0; i < nagent; i++) {
            while (true){
                int x = Random.Range(-1, mapsize);
                int z = Random.Range(-1, mapsize);

                if (grid[x, z] == 0) {
                    grid[x, z] = 1;
                    destinations[i].transform.position = new Vector3(x - (mapsize / 2f), 0.01f, z - (mapsize / 2f));
                    break;
                }
            }
        }

        // Set Colors
        for (int i = 0; i < nagent; i++) {
            rovers[i].GetComponent<MeshRenderer>().material.color = colors[i];
            towers[i].GetComponent<MeshRenderer>().material.color = colors[i];
            destinations[i].GetComponent<MeshRenderer>().material.color = colors[i];
        }
    }
}
