using System;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking
{
    [RequireComponent(typeof(NetworkRunner))]
    public class NetworkRunnerHandler : MonoBehaviour
    {
        private const string ROOM_NAME = "ROOM";
        [SerializeField] private GameMode gameMode = GameMode.Shared;
        [SerializeField] protected NetworkRunner _runner;
        private NetAddress netAddress = NetAddress.Any();
        private void Awake()
        {
            _runner ??= GetComponent<NetworkRunner>();
        }
        private void Start()
        {
            var sceneObjectProvider = _runner.GetComponent<NetworkSceneManagerDefault>()
                ?? _runner.gameObject.AddComponent<NetworkSceneManagerDefault>();

            var clientTask = _runner.StartGame(new StartGameArgs
            {
                GameMode = gameMode,
                Address = netAddress,
                Scene = SceneManager.GetActiveScene().buildIndex,
                Initialized = null,
                SessionName = ROOM_NAME,
                SceneManager = sceneObjectProvider
            });
        }
    }
}

