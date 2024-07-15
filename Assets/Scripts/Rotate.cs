using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector3 angularSpeed;
    

    void Update()
    {
        transform.Rotate(angularSpeed * Time.deltaTime);
    }
}
