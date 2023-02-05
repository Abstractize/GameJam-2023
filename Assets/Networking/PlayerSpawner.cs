using System;
using System.Collections.Generic;
using Cinemachine;
using Components;
using Fusion;
using Fusion.Sockets;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Networking
{
    public class PlayerSpawner : MonoBehaviour, INetworkRunnerCallbacks
    {
        [Header("Player Settings")]
        [SerializeField] private NetworkPrefabRef _playerPrefab;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private PlayerController _controller;
        [SerializeField] private Color[] _bodyColors;

        [Header("Audio Settings")]
        [SerializeField] private AudioSource _musicPlayer;

        [Header("GUI Settings")]
        [SerializeField] private StatsBar _statsBar;
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private GameObject _hud;

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
                if (NavMesh.SamplePosition(transform.position, out NavMeshHit closestHit, 500f, NavMesh.AllAreas))
                {
                    var player = runner.Spawn(_playerPrefab, closestHit.position, Quaternion.identity, runner.LocalPlayer);
                    _camera.Follow = player.transform;
                    _controller.gameObject.SetActive(true);
                    _controller.Player = player.GetComponent<Player.NetworkPlayer>();
                    _controller.StatsBar = _statsBar;
                    _musicPlayer.Play();

                    _loadingScreen.SetActive(false);
                    _hud.SetActive(true);

                    if (_controller.Player.HasInputAuthority)
                        player.GetComponentInChildren<SpriteSetter>().BackgroundColor =
                            _bodyColors[UnityEngine.Random.Range(0, _bodyColors.Length - 1)];
                }
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
