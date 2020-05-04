using UnityEngine;
using MLAgents;
using MLAgents.Sensors;

public class robot : Agent
{
    [Header("速度"), Range(1, 50)]
    public float speed = 10;
    private Rigidbody rigrobot;
    private Rigidbody rigblue;
    private Rigidbody rigred;

    private void Start()
    {
        rigrobot = GetComponent<Rigidbody>();
        rigblue = GameObject.Find("藍色").GetComponent<Rigidbody>();
        rigred = GameObject.Find("紅色").GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        //重設機器人的加速度與角速度
        rigrobot.velocity = Vector3.zero;
        rigrobot.angularVelocity = Vector3.zero;

        //隨機機器人初始位置
        Vector3 posrobot = new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(-2f, 2f));
        transform.position = posrobot;
        //隨機顏色初始位置
        Vector3 posblue = new Vector3(Random.Range(-2f, 2f), 1.29f, Random.Range(-2f, 2f));
        rigblue.position = posblue;
        Vector3 posred = new Vector3(Random.Range(-2f, 2f), 1.29f, Random.Range(-2f, 2f));
        rigred.position = posred;

        blue.complete = false;
        red.complete = false;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(rigred.position);
        sensor.AddObservation(rigblue.position);
        sensor.AddObservation(rigrobot.velocity.x);
        sensor.AddObservation(rigrobot.velocity.z);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        //使用參數控制機器人
        Vector3 control = Vector3.zero;
        control.x = vectorAction[0];
        control.z = vectorAction[1];
        rigrobot.AddForce(control * speed);


        if (blue.complete)
        {
            SetReward(1);
            EndEpisode();
        }

        if (transform.position.y < 0 || red.complete)
        {
            SetReward(-1);
            EndEpisode();
        }

    }


    //public override void Heuristic(float[] actionsOut)
    //{
        //提供開發者控制的方式
        //var action = new float[2];
        //actionsOut[0] = Input.GetAxis("Horizontal");
        //actionsOut[1] = Input.GetAxis("Vertical");
    //}

}
