using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;


public class TcpReactiveServer : IObservable<string>, IDisposable
{
    private readonly TcpListener _listener;
    private readonly List<IObserver<string>> _observerList;

    public TcpReactiveServer(int port, int backlogSize)
    {
        _observerList = new List<IObserver<string>> { };
        _listener = TcpListener.Create(port);
        _listener.Start(backlogSize);
        _listener.AcceptTcpClientAsync().ContinueWith(this.OnTcpClientConnected);
    }

    public IDisposable Subscribe(IObserver<string> observer)
    {
        _observerList.Add(observer);
        return null;
    }

    public void Dispose()
    {
        _listener.Stop();
    }

    private void OnTcpClientConnected(Task<TcpClient> clientTask)
    {
        if (clientTask.IsCompleted)
            Task.Factory.StartNew(() =>
          {
              using (var tcpClient = clientTask.Result)
              using (var stream = tcpClient.GetStream())
              using (var reader = new StreamReader(stream))

                  while (tcpClient.Connected)
                  {
                      var command = reader.ReadLine();
                      if (String.IsNullOrEmpty(command))
                          break;

                      foreach (var observer in _observerList)
                      {
                          try
                          {
                              observer.OnNext(command);
                          }
                          catch (Exception exc)
                          {

                              Debug.Log(exc.Message);
                              Debug.Log(exc.StackTrace);

                              observer.OnError(exc);
                          }
                      }
                  }

          }, TaskCreationOptions.PreferFairness);

        _listener.AcceptTcpClientAsync().ContinueWith(OnTcpClientConnected);
    }
}
