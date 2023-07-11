
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
// 1. Added the "GooseModdingAPI" project as a reference.
// 2. Compile this.
// 3. Create a folder with this DLL in the root, and *no GooseModdingAPI DLL*
using GooseShared;
using SamEngine;
using Windows;
using Microsoft.Win32;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.IO;
using System.Windows.Input;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DefaultMod
{
    public class ModEntryPoint : IMod
    {
        // Gets called automatically, passes in a class that contains pointers to
        // useful functions we need to interface with the goose.
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);
        #region variable definitions
        private GooseEntity goose;
        private bool isSnapshotRequested;
        public bool IsKeyDown;
        public string ScreenshotDefaultFiletype;
        public string ScreenshotDebugFiletype;
        public string DebugLogFilepath;
        public bool isDebugEnabled;
        public bool isMidSessionDebugEnabled;
        public string fileName;
        public int IDML;
        //Imod.Init Debug Message limit
        #endregion

        #region initialization
        void IMod.Init()
        {
            // Subscribe to whatever events we want
            MessageBox.Show("Debug","IMod.Init was called",MessageBoxButtons.OK);
            isSnapshotRequested = false;
            InjectionPoints.PostTickEvent += PostTick;
            InjectionPoints.PostRenderEvent += PostRender;
            InjectionPoints.PostModsLoaded += PostModsLoaded;
            //if (isDebugEnabled)
        }
    #endregion
        private void PostModsLoaded(GooseEntity goos, Graphics gfx, ModLoader.ModEntry[] mods) {
    
        }
        private void PostRender(GooseEntity goose, Graphics gfx) {
            MessageBox.Show("Debug","PostRender was called",MessageBoxButtons.OK);
        }

        public void PostTick(GooseEntity g)
        {
            // Do whatever you want here.
            MessageBox.Show("Debug", "PostTick was called",MessageBoxButtons.OK);
            goose = g;

            // If we're running our mod's task
            if (Keyboard.IsKeyDown(Key.S) && Keyboard.IsKeyDown(Key.Down))
            {
                MessageBox.Show("Debug","Keys S and Down were Pressed",MessageBoxButtons.OK);
                isSnapshotRequested = true;
                //if (isDebugEnabled){}
                // Lock our goose facing one direction for some reason?
                //g.direction = 0;
                //Console.WriteLine(API.TaskDatabase);

            }

        }

        public void debuglog()
        {

        }
        public void RenderScreenShot()
        {
            if (isSnapshotRequested && goose != null)
            {
                Bitmap screenCapture = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
                using (Graphics graphics = Graphics.FromImage(screenCapture))
                {
                    graphics.CopyFromScreen(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Location, Point.Empty, screenCapture.Size);
                }
                fileName = "Goosesnapshot.png";
                screenCapture.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                //string fileName = + ;
            }
        }
        
        public sealed class debug
        {

        }
    }

    public class Debug : ModEntryPoint, IMod
    {
        public void debugcheck()
        {

        }
    }


}
