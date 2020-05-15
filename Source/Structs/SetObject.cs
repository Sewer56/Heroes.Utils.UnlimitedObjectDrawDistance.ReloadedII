using System.Runtime.InteropServices;

namespace SonicHeroes.Utils.UnlimitedObjectDrawdistance.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SetObject
    {
        public const int MaxNumberOfObjects = 2048;

        // Note: lazy to write a proper fully featured struct. Maybe when I add SET parsing to Heroes.SDK.
        public float xPos;
        public float yPos;
        public float zPos;
        public int xRot;
        public int yRot;
        public int zRot;
        public short unknown1;
        public byte team;
        public byte unknown2;
        public int unknown3;
        public long unknownDuplicate;
        public byte objectList;
        public byte objectType;
        public byte linkId;
        public byte renderDistance;
        public short padding;
        public short miscEntryId;
    }
}
