using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace E_Commerce.Infrastrucrure.Service;

public class JWTOtions
{
    public static string SectionName { get; set; } = "JWTOptions";
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int DurationInHours { get; set; }
}

