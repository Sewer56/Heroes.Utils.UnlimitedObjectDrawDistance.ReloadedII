using System;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
using SonicHeroes.Utils.UnlimitedObjectDrawdistance;
using sonicheroes.utils.unlimitedobjectdrawdistance.Configuration;

namespace sonicheroes.utils.unlimitedobjectdrawdistance
{
    public class Program : IMod
    {
        private const string ModId = "sonicheroes.utils.unlimitedobjectdrawdistance"; // Insert Your Mod ID from ModConfig.json Here

        private ILogger _logger;
        private IModLoader _modLoader;
        private Configurator _configurator;
        private Config _configuration;
        private IReloadedHooks _hooks;
        private DrawDistanceHook _drawDistanceHook;

        public void Start(IModLoaderV1 loader)
        {
            _modLoader = (IModLoader)loader;
            _logger = (ILogger)_modLoader.GetLogger();

            /* Your mod code starts here. */

            // Your config file is in Config.json.
            // Need more configurations? See `_configurator.MakeConfigurations`
            _configurator  = new Configurator(_modLoader.GetDirectoryForModId(ModId));
            _configuration = _configurator.GetConfiguration<Config>(0);
            _configuration.ConfigurationUpdated += OnConfigurationUpdated;

            // Execute mod code.
            _modLoader.GetController<IReloadedHooks>().TryGetTarget(out _hooks);
            _drawDistanceHook = new DrawDistanceHook(_configuration.DrawDistance, _hooks);
        }

        private void OnConfigurationUpdated(IConfigurable obj)
        {
            /*
                This is executed when the configuration file gets updated by the user
                at runtime. This allows for mods to be configured in real time.

                Note: Events are also copied, you do not need to re-subscribe.
            */

            // Replace configuration with new.
            _configuration = (Config)obj;
            _logger.WriteLine($"[{ModId}] Config Updated: Applying");

            // Apply settings from configuration.
            // ... your code here.
            _drawDistanceHook.SetDrawDistance(_configuration.DrawDistance);
        }

        /* Mod loader actions. */
        public void Suspend() => _drawDistanceHook.Suspend();
        public void Resume()  => _drawDistanceHook.Resume();
        public void Unload()  => Suspend();

        public bool CanUnload()  => true;
        public bool CanSuspend() => true;

        /* Automatically called by the mod loader when the mod is about to be unloaded. */
        public Action Disposing { get; }
    }
}
