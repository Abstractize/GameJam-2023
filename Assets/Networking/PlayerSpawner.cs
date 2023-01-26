using System;
using System.Collections.Generic;
using Cinemachine;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace Networking
{
    public class PlayerSpawner : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private NetworkPrefabRef _playerPrefab;
        [SerializeField] private new CinemachineVirtualCamera camera;
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (runner.IsServer)
                runner.Spawn(_playerPrefab, transform.position, Quaternion.identity, player);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        { }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {

        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        { }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        { }

        public void OnConnectedToServer(NetworkRunner runner)
        {
            if (runner.Topology == SimulationConfig.Topologies.Shared)
            {
                var player = runner.Spawn(_playerPrefab, transform.position, Quaternion.identity, runner.LocalPlayer);
                camera.LookAt = player.transform;
                camera.Follow = player.transform;
            }

        }

        public void OnDisconnectedFromServer(NetworkRunner runner)
        { }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        { }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        { }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        { }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        { }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        { }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        { }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
        { }

        public void OnSceneLoadDone(NetworkRunner runner)
        { }

        public void OnSceneLoadStart(NetworkRunner runner)
        { }
    }
}
