using System;
using System.Collections.Concurrent;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Lamp Lamp;
    public Explosion Explosion;

    private TcpReactiveServer _tcpReactiveServer;
    private TcpReactiveClient _tcpReactiveClient;


    private void Start()
    {
        _tcpReactiveServer = new TcpReactiveServer(8888, 8096);
        _tcpReactiveClient = new TcpReactiveClient();

        _tcpReactiveServer.Subscribe(Lamp);
        _tcpReactiveServer.Subscribe(Explosion);
    }

    public void OnExplosion()
    {
        _tcpReactiveClient.Connection(Explosion.name);
    }

    public void OnLight()
    {
        _tcpReactiveClient.Connection(Lamp.name);
    }
}
