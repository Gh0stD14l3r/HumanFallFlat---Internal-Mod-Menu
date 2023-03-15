using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HumanFallFlat.UI
{
    class UI
    {
        private static string baseTitle = "Human Fall Flat - GH0STD14L3R";

        /* Menu window variables */
        private static Rect MainMenu = new Rect(5f, 5f, 230f, 175f);
        private static Rect TrollMenu = new Rect(5f, 5f, 270f, 300f);

        /* Menu window toggle variables */
        public static bool _MainMenu = true;
        private static bool _TrollMenu = false;

        /* Menu item toggle variables */
        public static bool _ESP = false;
        private static bool _ESPToggle = false;
        private static bool _TrollMenuToggle = false;

        /* Troll toggle variables */
        public static List<Human> butterFingers = new List<Human>();
        public static List<Human> deadPlayers = new List<Human>();
        public static List<Human> protectedPlayers = new List<Human>();

        /* Other variables */
        private static Vector2 scrollPosition;

        public static void displayUI()
        {
            GUI.backgroundColor = Modules.Utils.RGBtoColor(81, 81, 81);

            if (_MainMenu)
            {
                MainMenu = GUI.Window(0, MainMenu, MainWindow, baseTitle);
            }
            if (_TrollMenu)
            {
                TrollMenu = GUI.Window(1, TrollMenu, TrollWindow, baseTitle);
            }
        }

        private static void MainWindow(int windowID)
        {
            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal("");

            _ESPToggle = GUILayout.Toggle(_ESP, "Player ESP");
            if (_ESPToggle != _ESP)
            {
                _ESP = !_ESP;
            }

            _TrollMenuToggle = GUILayout.Toggle(_TrollMenu, "Troll Menu");
            if (_TrollMenuToggle != _TrollMenu)
            {
                _TrollMenu = !_TrollMenu;
            }
            

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUI.DragWindow(new Rect(0, 0, (float)Screen.width, (float)Screen.height));
        }

        private static void TrollWindow(int windowID)
        {
            GUILayout.Label("Troll Menu");

            scrollPosition = GUILayout.BeginScrollView(
                scrollPosition, GUILayout.Width(245), GUILayout.Height(200));

            foreach (Multiplayer.NetPlayer Player in UnityEngine.GameObject.FindObjectsOfType(typeof(Multiplayer.NetPlayer)) as Multiplayer.NetPlayer[])
            {
                Human mPlayer = Player.human;

                GUILayout.BeginVertical("box");
                GUILayout.BeginHorizontal("");
                GUILayout.Label($"Player: {mPlayer.player.overHeadNameTag.textMesh.text}");
                GUILayout.Label($"Is Host: {mPlayer.player.isLocalPlayer}");
                GUILayout.EndHorizontal();

                GUILayout.BeginVertical("");
                GUILayout.Label($"Is Grabbing: {mPlayer.hasGrabbed}");
                bool isGrabbed = mPlayer.grabbedByHuman == null ? false : true;
                GUILayout.Label($"Is Grabbed: {isGrabbed}");
                GUILayout.Label($"Butterfingers: {butterFingers.Contains(mPlayer)}");
                GUILayout.Label($"Dead: {deadPlayers.Contains(mPlayer)}");
                GUILayout.Label($"Protected: {protectedPlayers.Contains(mPlayer)}");

                GUILayout.EndVertical();

                if (butterFingers.Contains(mPlayer))
                {
                    if (GUILayout.Button("Stop Butterfingers"))
                    {
                        butterFingers.Remove(mPlayer);
                    }
                }
                else
                {
                    if (GUILayout.Button("Start Butterfingers"))
                    {
                        butterFingers.Add(mPlayer);
                    }
                }

                if (protectedPlayers.Contains(mPlayer))
                {
                    if (GUILayout.Button("Stop Protection"))
                    {
                        protectedPlayers.Remove(mPlayer);
                    }
                }
                else
                {
                    if (GUILayout.Button("Start Protection"))
                    {
                        protectedPlayers.Add(mPlayer);
                    }
                }

                if (deadPlayers.Contains(mPlayer))
                {
                    if (GUILayout.Button("Stop Death"))
                    {
                        deadPlayers.Remove(mPlayer);
                    }
                }
                else
                {
                    if (GUILayout.Button("Start Death"))
                    {
                        deadPlayers.Add(mPlayer);
                    }
                }

                if (GUILayout.Button("Teleport To Player"))
                {
                    Hacks.localPlayer.human.SetPosition(mPlayer.transform.position);
                }

                if (GUILayout.Button("Teleport To Me"))
                {
                    mPlayer.SetPosition(Hacks.localPlayer.human.transform.position);
                }

                if (GUILayout.Button("Increase Jump"))
                {
                    mPlayer.weight += 500f;
                }
                if (GUILayout.Button("Decrease Jump"))
                {
                    mPlayer.weight -= 500f;
                }
                if (GUILayout.Button("Reset Player"))
                {
                    Vector3 MainSpawn = Game.currentLevel.spawnPoint.position;
                    mPlayer.SpawnAt(MainSpawn);
                }
                GUILayout.EndVertical();
            }

            

            GUILayout.EndScrollView();
            GUI.DragWindow(new Rect(0, 0, (float)Screen.width, (float)Screen.height));
        }
    }
}
