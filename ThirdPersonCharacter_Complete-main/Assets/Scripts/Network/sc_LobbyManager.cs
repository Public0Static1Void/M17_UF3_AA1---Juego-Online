using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using UnityEngine;
using TMPro;

public class sc_LobbyManager : MonoBehaviour
{
    public TMP_InputField room_field;
    public TMP_InputField player_num_field;

    public void StartGamePlayerMax()
    {
        int max_players = 2;
        if (player_num_field.text != "")
        {
            if (!int.TryParse(player_num_field.text, out max_players))
            {
                max_players = 2;
            }
        }
        StartHost(max_players);
    }
    public async void StartHost(int max_players)
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(max_players - 1);
        string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        Debug.Log("Código para compartir: " + joinCode);
        GameManager.instance.room_code = joinCode;

        RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
        NetworkManager.Singleton.StartHost();
    }

    public void JoinRoom()
    {
        JoinGame(room_field.text);
    }

    public async void JoinGame(string joinCode)
    {
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

        RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
        NetworkManager.Singleton.StartClient();
    }
}