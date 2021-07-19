using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class TowerAgent : Agent
{

    // Pair
    public int idx;
    public GameObject Rover;
    public GameObject Landmark;

    public int nagent;
    public int nlandmark;

    public float signal0, signal1, signal2, signal3, signal4;
    public MeshRenderer light0, light1, light2, light3, light4;

    // Color
    public Color lightonColor = new Color(1f, 1f, 1f);
    public Color lightoffColor = new Color(70/255f, 70/255f, 70/255f);

    void Start()
    {   
        
    }

    void Update()
    {
        
    }

    public override void Initialize() 
    {
        signal0 = 0f;
        signal1 = 0f;
        signal2 = 0f;
        signal3 = 0f;
        signal4 = 0f;

        light0 = transform.Find("light1").GetComponent<MeshRenderer>();
        light1 = transform.Find("light2").GetComponent<MeshRenderer>();
        light2 = transform.Find("light3").GetComponent<MeshRenderer>();
        light3 = transform.Find("light4").GetComponent<MeshRenderer>();
        light4 = transform.Find("light5").GetComponent<MeshRenderer>();
    }

    public override void OnEpisodeBegin()
    {

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // pair ower index number ( x nagent )
        for (int i = 0; i < nagent; i++) {
            if (i == idx) sensor.AddObservation(1f);
            else sensor.AddObservation(0f);
        }

        // Landmark Position ( x 2 )
        sensor.AddObservation(Landmark.transform.position.x);
        sensor.AddObservation(Landmark.transform.position.z);

        // Paired Rover Position ( x 2 )
        sensor.AddObservation(Rover.transform.position.x);
        sensor.AddObservation(Rover.transform.position.z);

        // dummy obs ( x 5 )
        // sensor.AddObservation(0f);
        // sensor.AddObservation(0f);
        // sensor.AddObservation(0f);
        // sensor.AddObservation(0f);
        // sensor.AddObservation(0f);
    }   

    public override void OnActionReceived(float[] vectorAction) 
    {
        signal0 = 0f;
        signal1 = 0f;
        signal2 = 0f;
        signal3 = 0f;
        signal4 = 0f;

        light0.material.color = lightoffColor;
        light1.material.color = lightoffColor;
        light2.material.color = lightoffColor;
        light3.material.color = lightoffColor;
        light4.material.color = lightoffColor;

        if (vectorAction[0] == 1f) {
            signal0 = 1f;
            light0.material.color = lightonColor;
        }

        else if (vectorAction[1] == 1f) {
            signal1 = 1f;
            light1.material.color = lightonColor;
        }

        else if (vectorAction[2] == 1f) {
            signal2 = 1f;
            light2.material.color = lightonColor;
        }

        else if (vectorAction[3] == 1f) {
            signal3 = 1f;
            light3.material.color = lightonColor;
        }

        else if (vectorAction[4] == 1f) {
            signal4 = 1f;
            light4.material.color = lightonColor;
        }

        // Reward
        float dist = (Rover.transform.position - Landmark.transform.position).magnitude;
        float reward = -1f * dist;

        if (dist < 0.5f) reward += 10f;
        
        GiveReward(reward);
        Rover.GetComponent<RoverAgent>().GiveReward(reward);        
    }

    public override void Heuristic(float[] actionsOut)
    {
        
        for (int i = 0; i < 5; i++) {
            actionsOut[i] = 0f;
        }

        if (Input.GetKey(KeyCode.Q)) {
            actionsOut[0] = 1f;
        }

        else if (Input.GetKey(KeyCode.W)) {
            actionsOut[1] = 1f;
        }

        else if (Input.GetKey(KeyCode.E)) {
            actionsOut[2] = 1f;
        }

        else if (Input.GetKey(KeyCode.R)) {
            actionsOut[3] = 1f;
        }

        else if (Input.GetKey(KeyCode.T)) {
            actionsOut[4] = 1f;
        }
    }

    public void GiveReward(float reward)
    {
        AddReward(reward);
    }

    public void MakeEpisodeEnd()
    {
        EndEpisode();
    }

    
}
