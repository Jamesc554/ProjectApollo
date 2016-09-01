using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApollo
{
    [MoonSharpUserData]
    public class LuaUtils
    {
        public WorldController GetWorldController()
        {
            return WorldController.instance;
        }
    }
}
