using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;


using Photon.Pun;
using Photon.Realtime;


namespace Com.MyCompany.MyGame
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields  
        public GameObject pcPrefab;
        public GameObject vrPrefab; 
        public GameObject pcSpawn; 
        public GameObject vrSpawn; 
        #endregion

        #region Photon Callbacks

        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }
        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

                LoadArena();
            }
        }

        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

                LoadArena();
            }
        }

        #endregion

        #region Public Methods

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion
        #region Private Methods

        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
                return;
            }
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2) {
                Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
                PhotonNetwork.LoadLevel(Launcher.GetSceneNameFromBuildIndex(1));
            }
        }
        void Start()
        {
                InitializeGameBasedOnDevice();
            
        }

        void InitializeGameBasedOnDevice()
        {
            if (XRSettings.enabled)
            {
                Debug.Log("VR device detected. Initializing VR setup...");
                // Load VR-specific scene or instantiate VR objects
                PhotonNetwork.Instantiate(this.vrPrefab.name, vrSpawn.transform.position, Quaternion.identity, 0);
            }
            else
            {
                Debug.Log("Non-VR device detected. Initializing PC setup...");
                // Load PC-specific scene or instantiate PC objects
                PhotonNetwork.Instantiate(this.pcPrefab.name, pcSpawn.transform.position, Quaternion.identity, 0);

            }
        }

        #endregion
    }
}