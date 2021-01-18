using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, IObserver<string>
{

    [SerializeField] private GameObject _lampLight = default;
    [SerializeField] private GameObject _domeOff = default;
    [SerializeField] private GameObject _domeOn = default;

    private const string Name = "Lamp";

    private bool _finished;
    private bool _turnedOn;
    private bool _updated;


    void Update () {

        if (_updated)
        {
            _lampLight.SetActive(_turnedOn);
            _domeOff.SetActive(!_turnedOn);
            _domeOn.SetActive(_turnedOn);
            _updated = false;
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
        if(Name == value)
        {
            _turnedOn = !_turnedOn;
            _updated = true;
        }
    }
}
