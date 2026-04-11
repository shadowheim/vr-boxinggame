using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class LanConnectionConfig : MonoBehaviour
{
    [SerializeField] private ushort port = 7777;

    // For quick testing. Later you can replace this with an input field.
    [SerializeField] private string hostAddress = "127.0.0.1";

    private UnityTransport GetTransport()
    {
        if (NetworkManager.Singleton == null)
        {
            Debug.LogError("NetworkManager.Singleton is null.");
            return null;
        }

        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        if (transport == null)
        {
            Debug.LogError("UnityTransport component was not found on NetworkManager.");
            return null;
        }

        return transport;
    }

    public void ConfigureForHost()
    {
        UnityTransport transport = GetTransport();
        if (transport == null)
            return;

        // Listen on all interfaces so other LAN devices can reach this host.
        transport.SetConnectionData("0.0.0.0", port, "0.0.0.0");

        Debug.Log($"Configured host transport on port {port} with listen address 0.0.0.0");
    }

    public void ConfigureForClient()
    {
        UnityTransport transport = GetTransport();
        if (transport == null)
            return;

        transport.SetConnectionData(hostAddress, port);

        Debug.Log($"Configured client transport to connect to {hostAddress}:{port}");
    }

    public void SetHostAddress(string address)
    {
        hostAddress = address;
    }

    public string GetHostAddress()
    {
        return hostAddress;
    }

    public ushort GetPort()
    {
        return port;
    }
}