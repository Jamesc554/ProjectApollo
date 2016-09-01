using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ProjectApollo
{
    public class GUIButtons
    {
        private static List<Button> buttons = new List<Button>();
        private static Dictionary<string, int> tileIdDic = new Dictionary<string, int>();

        public static void ReadFromXML(string filePath, string spriteFilePath)
        {
            XmlTextReader reader = new XmlTextReader(filePath);

            while (reader.Read())
            {
                if (reader.GetAttribute("spriteLocation") != null)
                {
                    string tileFilePath;
                    tileFilePath = spriteFilePath + reader.GetAttribute("spriteLocation") + reader.GetAttribute("spriteFile");

                    Debug.WriteLine("Button loaded: " + reader.GetAttribute("spriteFile"));
                    Button newButton = new Button(tileFilePath);
                    newButton.luaClickedFunction = reader.GetAttribute("luaClickedFunction");

                    newButton.onClicked = (b) =>
                    {
                        Debug.WriteLine("Button has been clicked");

                        Script script = new Script();
                        script.Options.ScriptLoader = new FileSystemScriptLoader();
                        script.LoadFile(spriteFilePath + "Buttons.lua");

                        script.Globals["worldController"] = WorldController.instance;
                        script.Globals["camera"] = ProjectApollo.camera;

                        script.DoFile(spriteFilePath + "Buttons.lua");

                        DynValue buttonOnClickFunction = script.Globals.Get(b.luaClickedFunction);
                        DynValue res = script.Call(buttonOnClickFunction, b);

                        Debug.WriteLine(res.ToString());
                    };

                    if (reader.GetAttribute("luaUpdateFunction") != null)
                    {
                        newButton.luaUpdateFunction = reader.GetAttribute("luaUpdateFunction");
                        newButton.onUpdate = (b) =>
                        {

                            Script script = new Script();
                            script.Options.ScriptLoader = new FileSystemScriptLoader();
                            script.LoadFile(spriteFilePath + "Buttons.lua");

                            script.Globals["worldController"] = WorldController.instance;
                            script.Globals["camera"] = ProjectApollo.camera;

                            script.DoFile(spriteFilePath + "Buttons.lua");

                            DynValue buttonOnUpdateFunction = script.Globals.Get(b.luaUpdateFunction);
                            DynValue res = script.Call(buttonOnUpdateFunction, b);

                            Debug.WriteLine(res.ToString());
                        };

                        //newButton.onClicked();

                    }
                    AddButton(newButton);
                }
            }
        }

        public static void AddButton(Button button)
        {
            buttons.Add(button);
        }

        public static Button GetButton(int buttonId)
        {
            Button oldButton = buttons[buttonId];
            Button newButton = new Button(oldButton.spriteLocation);
            newButton.luaClickedFunction = oldButton.luaClickedFunction;
            newButton.luaUpdateFunction = oldButton.luaUpdateFunction;
            newButton.onClicked = oldButton.onClicked;
            newButton.onUpdate = oldButton.onUpdate;
            return newButton;
        }
    }
}
