using UnityEngine;
using MLAgents;
using MLAgents.Sensors;

public class rabit : Agent
{   [Header("速度"), Range(1, 50)]
    public float speed = 10;
    private Rigidbody rigrabbit;
    private Rigidbody rigblue;
    private Rigidbody rigred;

    private void Start()
    {
        rigrabbit = GetComponent<Rigidbody>();
        rigblue = GameObject.Find("藍色").GetComponent<Rigidbody>();
        rigred = GameObject.Find("紅色").GetComponent<Rigidbody>();
    }


    /// <summary>
    /// 事件開始時：重新設定機器人與食物位置
    /// </summary>
    public override void OnEpisodeBegin()
    {
        //重設機器人的加速度與角速度
        rigrabbit.velocity = Vector3.zero;
        rigrabbit.angularVelocity = Vector3.zero;
        rigred.velocity = Vector3.zero;
        rigred.angularVelocity = Vector3.zero;
        rigblue.velocity = Vector3.zero;
        rigblue.angularVelocity = Vector3.zero;

        //隨機機器人初始位置
        Vector3 posrabbit = new Vector3(Random.Range(-2f, 2f), 0.05f, Random.Range(-2f, 2f));
        transform.position = posrabbit;
        Vector3 posblue = new Vector3(Random.Range(-3.0f, 2f), 0.05f, Random.Range(-3.0f, 2f));
        transform.position = posblue;
        Vector3 posred = new Vector3(Random.Range(-3.0f, 2f), 0.05f, Random.Range(-3.0f, 2f));
        transform.position = posred;

        blue.complete = false;
        red.complete = false;
    }

    /// <summary>
    /// 收集觀測資料
    /// </summary>
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(rigred.position);
        sensor.AddObservation(rigblue.position);
        sensor.AddObservation(rigrabbit.velocity.x);
        sensor.AddObservation(rigrabbit.velocity.z);
    }

    /// <summary>
    /// 動作：控制機器人與回饋
    /// </summary>
    /// <param name="vectorAction"></param>
    public override void OnActionReceived(float[] vectorAction)
    {
        //使用參數控制機器人
        Vector3 control = Vector3.zero;
        control.x = vectorAction[0];
        control.z = vectorAction[1];
        rigrabbit.AddForce(control * speed);

        //球進球門:成功:加一分並結束
        if (blue.complete)
        {
            SetReward(1);
            EndEpisode();
        }

        //機器人或球掉到地板下:失敗:扣一分並結束
        if (transform.position.y < 0 || red.complete)
        {
            SetReward(-1);
            EndEpisode();
        }

    }


    public override float[] Heuristic()
    {
        //提供開發者控制的方式
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }
}
