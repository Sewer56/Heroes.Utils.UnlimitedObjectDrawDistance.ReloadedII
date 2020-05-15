using System.ComponentModel;
using SonicHeroes.Utils.UnlimitedObjectDrawdistance.Configuration;

namespace SonicHeroes.Utils.UnlimitedObjectDrawdistance
{
    public class Config : Configurable<Config>
    {
        [DisplayName("Minimum Draw Distance")]
        [Description("Minimum object draw distance, between 0 and 255.")]
        public byte MinimumDrawDistance { get; set; } = 0;

        [DisplayName("Draw Distance Multiplier")]
        [Description("Draw distance of this object is scaled by this value.")]
        public float DrawDistanceMultiplier { get; set; } = 2;
    }
}
