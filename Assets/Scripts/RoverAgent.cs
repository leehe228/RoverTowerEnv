using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class RoverAgent : Agent
{

    // Pair
    public int idx;
    public GameObject Tower;
    public GameObject Landmark;

    public int nagent;
    public int nlandmark;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Initialize() 
    {

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

        // signal ( x 5 )
        sensor.AddObservation(Tower.GetComponent<TowerAgent>().signal0);
        sensor.AddObservation(Tower.GetComponent<TowerAgent>().signal1);
        sensor.AddObservation(Tower.GetComponent<TowerAgent>().signal2);
        sensor.AddObservation(Tower.GetComponent<TowerAgent>().signal3);
        sensor.AddObservation(Tower.GetComponent<TowerAgent>().signal4);

        // position ( x 2 )
        sensor.AddObservation(gameObject.transform.position.x);
        sensor.AddObservation(gameObject.transform.position.z);

        // velocity ( x 2 )
        sensor.AddObservation(gameObject.GetComponent<Rigidbody>().velocity.x);
        sensor.AddObservation(gameObject.GetComponent<Rigidbody>().velocity.z);
    }

    public override void OnActionReceived(float[] vectorAction) 
    {
        if (vectorAction[0] == 1f) {
            gameObject.transform.Translate(Vector3.zero);
        }

        else if (vectorAction[1] == 1f) {
            gameObject.transform.Translate(Vector3.forward * 0.3f);
        }

        else if (vectorAction[2] == 1f) {
            gameObject.transform.Translate(Vector3.back * 0.3f);
        }

        else if (vectorAction[3] == 1f) {
            gameObject.transform.Translate(Vector3.left * 0.3f);
        }

        else if (vectorAction[4] == 1f) {
            gameObject.transform.Translate(Vector3.right * 0.3f);
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        for (int i = 0; i < 5; i++) {
            actionsOut[i] = 0f;
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            actionsOut[1] = 1f;
        }

        else if (Input.GetKey(KeyCode.DownArrow)) {
            actionsOut[2] = 1f;
        }

        else if (Input.GetKey(KeyCode.LeftArrow)) {
            actionsOut[3] = 1f;
        }

        else if (Input.GetKey(KeyCode.RightArrow)) {
            actionsOut[4] = 1f;
        }

        else {
            actionsOut[0] = 1f;
        }
    }

    public void GiveReward(float reward)
    {
        SetReward(reward);
    }

    public void MakeEpisodeEnd()
    {
        EndEpisode();
    }
}
