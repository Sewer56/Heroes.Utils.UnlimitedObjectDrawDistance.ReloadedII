using System.ComponentModel;
using sonicheroes.utils.unlimitedobjectdrawdistance.Configuration;

namespace sonicheroes.utils.unlimitedobjectdrawdistance
{
    public class Config : Configurable<Config>
    {
        [DisplayName("Draw Distance")]
        [Description("Constant object draw distance, between 0 and 255.")]
        public byte DrawDistance { get; set; } = 48;
    }
}
