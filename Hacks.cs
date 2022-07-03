using System;
using System.Threading;
using UnityEngine;


namespace HumanFallFlat
{
    class Hacks : MonoBehaviour
    {
        public static bool t_MENU = true;
        public static bool t_ESP = false;
        public static bool t_CTRL = false;
        public static bool t_FLIGHT = false;


        public static bool toggleESP;
        public static bool toggleControl;

        public static Multiplayer.NetPlayer localPlayer = null;

        public static Vector3 TP_Pos;
        public static int TP_Saved = 0;

        public void Start()
        {
            
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.End)) //Kill hacks on "End" key pressed
            {
                Loader.unload();
            }
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                t_MENU = !t_MENU;
            }
            
        }

        public void OnGUI()
        {
            if (t_MENU)
            {
                GUI.Box(new Rect(5f, 5f, 300f, 90f), "");
                GUI.Label(new Rect(10f, 5f, 300f, 30f), "Human Fall Flat - Gh0st v1");
                toggleESP = GUI.Toggle(new Rect(10f, 30f, 290f, 25f), t_ESP, "Enable/Disable ESP");
                if (toggleESP != t_ESP)
                {
                    t_ESP = !t_ESP;
                }
                toggleControl = GUI.Toggle(new Rect(10f, 55f, 290f, 25f), t_CTRL, "Control Panel");
                if (toggleControl != t_CTRL)
                {
                    t_CTRL = !t_CTRL;
                }
            }
            

            if (localPlayer == null)
            {
                foreach (Multiplayer.NetPlayer Player in UnityEngine.GameObject.FindObjectsOfType(typeof(Multiplayer.NetPlayer)) as Multiplayer.NetPlayer[])
                {
                    if (Player.isLocalPlayer)
                    {
                        localPlayer = Player;
                    }
                }
            }
            

            if (t_ESP)
            {
                foreach (Multiplayer.NetPlayer Player in UnityEngine.GameObject.FindObjectsOfType(typeof(Multiplayer.NetPlayer)) as Multiplayer.NetPlayer[])
                {
                    Human mPlayer = Player.human;
                    Vector3 pivotPos1 = Player.human.ragdoll.transform.position;
                    Vector3 playerFootPos1; playerFootPos1.x = pivotPos1.x; playerFootPos1.z = pivotPos1.z; playerFootPos1.y = pivotPos1.y - 2f;
                    Vector3 playerHeadPos1; playerHeadPos1.x = playerFootPos1.x; playerHeadPos1.z = playerFootPos1.z; playerHeadPos1.y = playerFootPos1.y + 2f;


                    Vector3 w2s_playerFoot1 = Camera.main.WorldToScreenPoint(playerFootPos1);
                    Vector3 w2s_playerHead1 = Camera.main.WorldToScreenPoint(playerHeadPos1);

                    if (w2s_playerFoot1.z > 0f)
                    {
                        DrawESP(w2s_playerFoot1, w2s_playerHead1, Color.magenta, mPlayer.player.overHeadNameTag.textMesh.text);
                    }
                }
            }

            if (t_CTRL && t_MENU)
            {
                GUI.Box(new Rect(5f, 100f, 300f, 300f), "");

                if (GUI.Button(new Rect(10f, 105f, 140f, 30f), "Players Release")) 
                {
                    foreach (Multiplayer.NetPlayer Player in UnityEngine.GameObject.FindObjectsOfType(typeof(Multiplayer.NetPlayer)) as Multiplayer.NetPlayer[])
                    {
                        if (!Player.isLocalPlayer)
                        {
                            Player.human.ReleaseGrab(0f);
                        }
                    }
                }
                if (GUI.Button(new Rect(155f, 105f, 140f, 30f), "Reset Player")) 
                {
                    Vector3 MainSpawn = Game.currentLevel.spawnPoint.position;
                    localPlayer.human.SpawnAt(MainSpawn);
                }
                if (GUI.Button(new Rect(10f, 140f, 140f, 30f), "Knockout self"))  
                {
                    localPlayer.human.MakeUnconscious(4f);
                }
                if (GUI.Button(new Rect(155f, 140f, 140f, 30f), "Knockout Players")) 
                {
                    foreach (Multiplayer.NetPlayer Player in UnityEngine.GameObject.FindObjectsOfType(typeof(Multiplayer.NetPlayer)) as Multiplayer.NetPlayer[])
                    {
                        if (!Player.isLocalPlayer)
                        {
                            Player.human.MakeUnconscious(4f);
                        }
                    }
                }
                if (GUI.Button(new Rect(10f, 175f, 140f, 30f), "Decrease Jump")) 
                {
                    localPlayer.human.weight -= 500f;
                }
                if (GUI.Button(new Rect(155f, 175f, 140f, 30f), "Increase Jump")) 
                {
                    localPlayer.human.weight += 500f;
                }
                if (GUI.Button(new Rect(10f, 210f, 140f, 30f), "Save TP Pos")) 
                {
                    TP_Pos = localPlayer.human.ragdoll.transform.position;
                    TP_Saved = 1;
                }
                if (GUI.Button(new Rect(155f, 210f, 140f, 30f), "TP To Saved Pos")) 
                {
                    if (TP_Saved == 1)
                    {
                        localPlayer.human.SetPosition(TP_Pos);
                    }
                }
                if (GUI.Button(new Rect(10f, 245f, 140f, 30f), "Respawn All Players"))
                {
                    Game.instance.RespawnAllPlayers();
                }
                
            }

            
            


        }

        public void DrawESP(Vector3 objfootPos, Vector3 objheadPos, Color objColor, String name)
        {
            float height = objheadPos.y - objfootPos.y;
            float widthOffset = 2f;
            float width = height / widthOffset;

            //Uncomment to add ESP box.. not recommended with the ragdoll bodies. Bones would be better and bone locations are
            //already added under localPlayer.human.ragdoll or NetPlayer...etc
            //Render.DrawBox(objheadPos.x - (width / 2), (float)Screen.height - objheadPos.y - height, 50f, 50f, objColor, 2f);
            if (name != "")
            {
                Render.DrawString(new Vector2(objfootPos.x - (width / 2), (float)Screen.height - objfootPos.y - height), $"{name}", objColor);
            }
        }

        

    }
}
