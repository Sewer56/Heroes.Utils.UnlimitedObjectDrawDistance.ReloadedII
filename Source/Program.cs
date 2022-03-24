using System;
using Reloaded.Hooks.Definitions;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
using SonicHeroes.Utils.UnlimitedObjectDrawdistance.Configuration;
using IReloadedHooks = Reloaded.Hooks.ReloadedII.Interfaces.IReloadedHooks;

namespace SonicHeroes.Utils.UnlimitedObjectDrawdistance;

public class Program : IMod
{
    private static string ModId = "sonicheroes.utils.unlimitedobjectdrawdistance"; // Insert Your Mod ID from ModConfig.json Here

    private ILogger _logger;
    private IModLoader _modLoader;
    private Configurator _configurator;
    private Config _configuration;
    private IReloadedHooks _hooks;
    private IReloadedHooksUtilities _utilities;
    private DrawDistanceHook _drawDistanceHook;

    public void StartEx(IModLoaderV1 loader, IModConfigV1 config)
    {
        _modLoader = (IModLoader)loader;
        _logger = (ILogger)_modLoader.GetLogger();

        /* Your mod code starts here. */
        ModId = config.ModId;

        // Your config file is in Config.json.
        // Need more configurations? See `_configurator.MakeConfigurations`
        var modFolder = _modLoader.GetDirectoryForModId(config.ModId);
        var configFolder = _modLoader.GetModConfigDirectory(config.ModId);

        _configurator = new Configurator(configFolder);
        _configurator.Migrate(modFolder, configFolder);
        _configuration = _configurator.GetConfiguration<Config>(0);
        _configuration.ConfigurationUpdated += OnConfigurationUpdated;

        // Execute mod code.
        _modLoader.GetController<IReloadedHooks>().TryGetTarget(out _hooks);
        _modLoader.GetController<IReloadedHooksUtilities>().TryGetTarget(out _utilities);
        _drawDistanceHook = new DrawDistanceHook(_configuration, _hooks, _utilities);
    }

    private void OnConfigurationUpdated(IConfigurable obj)
    {
        // Replace configuration with new.
        _configuration = (Config)obj;
        _logger.WriteLine($"[{ModId}] Config Updated: Applying");

        // Apply settings from configuration.
        // ... your code here.
        _drawDistanceHook.UpdateConfig(_configuration);
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