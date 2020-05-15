using System;
using System.Collections.Generic;
using System.Text;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Memory.Interop;
using IReloadedHooks = Reloaded.Hooks.ReloadedII.Interfaces.IReloadedHooks;

namespace SonicHeroes.Utils.UnlimitedObjectDrawdistance
{
    internal class DrawDistanceHook
    {
        private Pinnable<byte> _drawDistance = new Pinnable<byte>(48);
        private IAsmHook _setDrawDistanceHook;

        public unsafe DrawDistanceHook(byte newDrawDistance, IReloadedHooks hooks)
        {
            _drawDistance.Value = newDrawDistance;

            var drawDistanceHook = new[]
            {
                $"use32",
                $"push eax", // Backup eax
                $"mov al, [0x{(long)_drawDistance.Pointer:X}]", // Copy new draw distance.
                $"mov [ecx], al",
                $"pop eax"   // Restore eax
            };

            _setDrawDistanceHook = hooks.CreateAsmHook(drawDistanceHook, 0x0043E00A, AsmHookBehaviour.ExecuteFirst).Activate();
        }

        /// <summary>
        /// Sets the new draw distance to apply upon loading new object sets.
        /// </summary>
        public void SetDrawDistance(byte newDrawDistance) => _drawDistance.Value = newDrawDistance;

        public void Suspend() => _setDrawDistanceHook.Disable();
        public void Resume() => _setDrawDistanceHook.Enable();
        public void Unload() => Suspend();
    }
}
