using System.Runtime.InteropServices;
using System.Security;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.X86;
using SonicHeroes.Utils.UnlimitedObjectDrawdistance.Structs;
using IReloadedHooks = Reloaded.Hooks.ReloadedII.Interfaces.IReloadedHooks;

namespace SonicHeroes.Utils.UnlimitedObjectDrawdistance;

internal class DrawDistanceHook
{
    private Config _config;
    private IHook<SetParamOptimize> _optimizeHook;

    public unsafe DrawDistanceHook(Config config, IReloadedHooks hooks, IReloadedHooksUtilities utilities)
    {
        _config = config;
        _optimizeHook = hooks.CreateHook<SetParamOptimize>(ModifyObjectDistance, 0x0043DF40).Activate();
    }

    private unsafe void ModifyObjectDistance()
    {
        // We're modifying the SET file entries here
        var renderDistancePointer = (SetObject*)(*(int*)0xA23F6C);
        for (int x = 0; x < SetObject.MaxNumberOfObjects; x++)
        {
            ModifyObjectRenderDistanceImpl(&renderDistancePointer->renderDistance);
            renderDistancePointer += 1;
        }
        return;
    }

    private unsafe void ModifyObjectRenderDistanceImpl(byte* renderDistance)
    {
        // Discard any other info in register.
        int scaledDistance = (int)((int)*renderDistance * _config.DrawDistanceMultiplier);
            
        if (scaledDistance < _config.MinimumDrawDistance)
            scaledDistance = _config.MinimumDrawDistance;

        if (scaledDistance > byte.MaxValue)
            scaledDistance = byte.MaxValue;

        *renderDistance = (byte) scaledDistance;
    }

    public void UpdateConfig(Config config) => _config = config;
    public void Suspend() => _optimizeHook.Disable();
    public void Resume() => _optimizeHook.Enable();
    public void Unload() => Suspend();

    [SuppressUnmanagedCodeSecurity]
    [Function(CallingConventions.Cdecl)]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private unsafe delegate void SetParamOptimize();
}