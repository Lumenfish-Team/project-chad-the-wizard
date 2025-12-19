using Lumenfish.CameraSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Lumenfish.Player;

public class GameInitializer : LifetimeScope
{
    [SerializeField] private PlayerController playerControllerPrefab;
    [SerializeField] private PlayerFollowCamera  playerFollowCamera;
    [SerializeField] private CameraTargetController cameraTargetControllerPrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        var playerController = Instantiate(playerControllerPrefab);
        builder.RegisterComponent(playerController);
        
        var cameraTargetController = Instantiate(cameraTargetControllerPrefab);
        builder.RegisterComponent(cameraTargetController);
        
        var followCamera = Instantiate(playerFollowCamera);
        builder.RegisterComponent(followCamera);
    }
}