using System;
using UnityEngine;

public class Explosion : MonoBehaviour, IObserver<string>
{
    [SerializeField] private ParticleSystem _explosion = default;

    private const string Name = "Explosion";

    private bool _finished;
    private bool _boom;
    private float _force = 10.0f;


    void Update()
    {
        if (_boom)
        {
            _explosion.Play();
            _boom = false;
        }
    }

    public void OnCompleted()
    {
        if (_finished)

            OnError(new Exception($"This {Name} already finished it's lifecycle"));
        else
            _finished = true;
    }

    public void OnError(Exception error)
    {
        Debug.Log(error.Message);
    }

    public void OnNext(string value)
    {
        if (Name == value)
            _boom = true;
    }
}
