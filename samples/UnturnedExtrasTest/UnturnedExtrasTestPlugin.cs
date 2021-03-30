﻿using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenMod.API.Plugins;
using OpenMod.Unturned.Players.Life.Events;
using OpenMod.Unturned.Users.Events;
using SilK.Unturned.Extras.Events;
using SilK.Unturned.Extras.Plugins;
using SilK.Unturned.Extras.Server;
using System;

[assembly: PluginMetadata("UnturnedExtrasTest", DisplayName = "Unturned Extras Test")]

namespace UnturnedExtrasTest
{
    public class UnturnedExtrasTestPlugin : OpenModUnturnedExtrasPlugin,
        IExtraEventListener<UnturnedUserConnectedEvent>,
        IAsyncExtraEventListener<UnturnedPlayerDamagedEvent>
    {
        public UnturnedExtrasTestPlugin(
            IServerHelper serverHelper,
            ILogger<UnturnedExtrasTestPlugin> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            serverHelper.RunWhenServerLoaded(() => logger.LogInformation("Server is loaded"));
        }

        public UniTask HandleEventAsync(object? sender, UnturnedUserConnectedEvent @event)
        {
            Logger.LogInformation($"Player connected - {@event.User.DisplayName}");

            return UniTask.CompletedTask;
        }

        public async UniTask HandleEventAsync(object? sender, UnturnedPlayerDamagedEvent @event)
        {
            // Wait fifteen seconds. Players would experience timeout if this runs on the main thread
            await UniTask.Delay(15000);

            Logger.LogInformation($"Player took damage a bit ago");
        }
    }
}