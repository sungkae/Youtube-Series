using System;
using GeonBit.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Youtube2DMMORPGClient
{
    class InterfaceGUI
    {
        public static List<Panel> Windows = new List<Panel>();

        public void InitializeGUI()
        {

        }

        public void CreateWindow(Panel panel)
        {
            Windows.Add(panel);
        }

        public void CreateWindow_Login()
        {
            //Create Entities.
            Panel panel = new Panel(new Vector2(500, 430));
            Button btnLogin = new Button("Login");
            TextInput txtUser = new TextInput(false);
            TextInput txtPass = new TextInput(false);
            CheckBox chkUser = new CheckBox("Save Username?");
            UserInterface.Active.AddEntity(panel);
        }
    }
}
