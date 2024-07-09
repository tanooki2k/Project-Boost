// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 _startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] private float period = 2f;
    [SerializeField] [Range(0, 2 * Mathf.PI)] private float phase = 0f;

    float _movementFactor;
    
    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        
        float cycles = Time.time / period; // Continually growing over time
        
        const float tau = Mathf.PI * 2; // Constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau + phase); // Going from -1 to 1

        _movementFactor = (rawSinWave + 1f) / 2f; // Recalculated to go from 0 to 1, so it's cleaner
        
        Vector3 offset = movementVector * _movementFactor;
        transform.position = _startingPosition + offset;
    }
}
