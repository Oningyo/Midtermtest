using UnityEngine;

public class red : MonoBehaviour
{
    public static bool complete;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "兔子")
        {
            complete = true;
        }
    }
}
