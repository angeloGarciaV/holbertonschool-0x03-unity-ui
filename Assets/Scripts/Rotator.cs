using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(45 * Time.deltaTime, 0, 0);
    }
}
