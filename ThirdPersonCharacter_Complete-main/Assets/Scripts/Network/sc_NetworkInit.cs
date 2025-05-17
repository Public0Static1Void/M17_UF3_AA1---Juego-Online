using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using System.Threading.Tasks;

public class sc_NetworkInit : MonoBehaviour
{
    private async void Awake()
    {
        await InitializeServicesAsync();
    }

    private async Task InitializeServicesAsync()
    {
        if (!UnityServices.State.Equals(ServicesInitializationState.Initialized))
        {
            await UnityServices.InitializeAsync();
        }

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Signed in anonymously as: " + AuthenticationService.Instance.PlayerId);
        }
    }
}
