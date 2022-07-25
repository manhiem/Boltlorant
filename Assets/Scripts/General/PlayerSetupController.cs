using Photon.Bolt;
using UnityEngine;


public class PlayerSetupController : Photon.Bolt.GlobalEventListener
{
    [SerializeField]
    private GameObject _setupPanel;
    public Camera _sceneCamera;

    public Camera SceneCamera { get => _sceneCamera; }

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        if (!BoltNetwork.IsServer)
        {
            _setupPanel.SetActive(true);
        }
    }

    public override void OnEvent(SpawnPlayerEvent evnt)
    {
        BoltEntity entity = BoltNetwork.Instantiate(BoltPrefabs.Player, new Vector3(0, 1f, 0), Quaternion.identity);
        entity.AssignControl(evnt.RaisedBy);
    }

    public void SpawnPlayer()
    {
        SpawnPlayerEvent evnt = SpawnPlayerEvent.Create(GlobalTargets.OnlyServer);
        evnt.Send();
    }
}
