using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject _tamagotchiPrefab;
    [SerializeField]
    private GameObject _playerPrefab;

    [Header("Spawn Points")]
    [SerializeField]
    private Transform[] _playerSpawnPoints;
    [SerializeField]
    private Transform _tamagotchiSpawnPoint;

    private int _currentPlayerIndex = 0;

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        GameObject tamagtochiObject = Instantiate(_tamagotchiPrefab, _tamagotchiSpawnPoint.position, _tamagotchiSpawnPoint.rotation);
        NetworkServer.Spawn(tamagtochiObject);
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);
        Debug.Log("On server ready");
        Transform spawnPoint = _playerSpawnPoints[_currentPlayerIndex];

        GameObject playerObject = Instantiate(_playerPrefab, spawnPoint.position, spawnPoint.rotation);
        NetworkServer.Spawn(playerObject, conn);

        _currentPlayerIndex++;
    }
}
