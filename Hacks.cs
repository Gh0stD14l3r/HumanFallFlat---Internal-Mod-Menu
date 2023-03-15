using System;
using System.Threading;
using UnityEngine;


namespace HumanFallFlat
{
    class Hacks : MonoBehaviour
    {
       
        public static Multiplayer.NetPlayer localPlayer = null;

        private static bool cursor = false;
        public static float Timer = 3f;

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
                UI.UI._MainMenu = !UI.UI._MainMenu;
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!cursor)
                {
                    Cursor.visible = true;
                    cursor = true;
                }
            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                if (cursor)
                {
                    Cursor.visible = false;
                    cursor = false;
                }
            }

            Timer += Time.deltaTime;
            if (Timer >= 5f)
            {
                Timer = 0f;

                foreach (Multiplayer.NetPlayer Player in UnityEngine.GameObject.FindObjectsOfType(typeof(Multiplayer.NetPlayer)) as Multiplayer.NetPlayer[])
                {
                    Human mPlayer = Player.human;
                    
                    if (Player.isLocalPlayer)
                    {
                        localPlayer = Player;
                    }
                }
            }

            foreach(Human _human in UI.UI.butterFingers)
            {
                if (_human.hasGrabbed)
                {
                    _human.ReleaseGrab(0f);
                }
            }

            foreach(Human _human in UI.UI.deadPlayers)
            {
                _human.MakeUnconscious(4f);
            }

            foreach(Human _human in UI.UI.protectedPlayers)
            {
                Human _target = _human.grabbedByHuman;
                if (_target != null)
                {
                    _target.ReleaseGrab(0f);
                }
                _target = null;
            }
            
        }

        public void OnGUI()
        {
            UI.UI.displayUI();
            
            if (UI.UI._ESP)
            {
                foreach (Multiplayer.NetPlayer Player in UnityEngine.GameObject.FindObjectsOfType(typeof(Multiplayer.NetPlayer)) as Multiplayer.NetPlayer[])
                {
                    Human mPlayer = Player.human;
                    Vector3 pivotPos1 = Player.human.ragdoll.transform.position;
                    Vector3 playerFootPos1; playerFootPos1.x = pivotPos1.x; playerFootPos1.z = pivotPos1.z; playerFootPos1.y = pivotPos1.y - 2f;
                    Vector3 playerHeadPos1; playerHeadPos1.x = playerFootPos1.x; playerHeadPos1.z = playerFootPos1.z; playerHeadPos1.y = playerFootPos1.y + 2f;


                    Vector3 w2s_playerFoot1 = Camera.main.WorldToScreenPoint(playerFootPos1);
                    Vector3 w2s_playerHead1 = Camera.main.WorldToScreenPoint(playerHeadPos1);
                    DrawBones(mPlayer);
                    if (w2s_playerFoot1.z > 0f && !Player.isLocalPlayer)
                    {
                        DrawESP(w2s_playerFoot1, w2s_playerHead1, Color.magenta, mPlayer.player.overHeadNameTag.textMesh.text);
                        //DrawBones(mPlayer);
                    }
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

        private void DrawBones(Human _human)
        {
            string c = "";
            foreach(Transform _t in _human.ragdoll.bones)
            {
                c = c + _t.name;
            }
            string text = $"Bones: {c}";
            GUI.TextArea(new Rect(0f, 200f, (float)Screen.width, (float)Screen.height), text);

            Vector3 _head = _human.ragdoll.partHead.skeleton.position;
            Vector3 _chest = _human.ragdoll.partChest.skeleton.position;
            Vector3 _hips = _human.ragdoll.partHips.skeleton.position;

            Vector3 _leftthigh = _human.ragdoll.partLeftThigh.skeleton.position;
            Vector3 _leftleg = _human.ragdoll.partLeftLeg.skeleton.position;
            Vector3 _leftfoot = _human.ragdoll.partLeftFoot.skeleton.position;

            Vector3 _rightthigh = _human.ragdoll.partRightThigh.skeleton.position;
            Vector3 _rightleg = _human.ragdoll.partRightLeg.skeleton.position;
            Vector3 _rightfoot = _human.ragdoll.partRightFoot.skeleton.position;

            Vector3 _leftarm = _human.ragdoll.partLeftArm.skeleton.position;
            Vector3 _leftforearm = _human.ragdoll.partLeftForearm.skeleton.position;
            Vector3 _lefthand = _human.ragdoll.partLeftHand.skeleton.position;

            Vector3 _rightarm = _human.ragdoll.partRightArm.skeleton.position;
            Vector3 _rightforearm = _human.ragdoll.partRightForearm.skeleton.position;
            Vector3 _righthand = _human.ragdoll.partRightHand.skeleton.position;

            /*DrawBoneLine(_head, _chest, Color.red);
            DrawBoneLine(_chest, _hips, Color.red);

            DrawBoneLine(_hips, _leftthigh, Color.red);
            DrawBoneLine(_leftthigh, _leftleg, Color.red);
            DrawBoneLine(_leftleg, _leftfoot, Color.red);

            DrawBoneLine(_hips, _rightthigh, Color.red);
            DrawBoneLine(_rightthigh, _rightleg, Color.red);
            DrawBoneLine(_rightleg, _rightfoot, Color.red);

            DrawBoneLine(_chest, _leftarm, Color.red);
            DrawBoneLine(_leftarm, _leftforearm, Color.red);
            DrawBoneLine(_leftforearm, _lefthand, Color.red);

            DrawBoneLine(_chest, _rightarm, Color.red);
            DrawBoneLine(_rightarm, _rightforearm, Color.red);
            DrawBoneLine(_rightforearm, _righthand, Color.red);*/
        }

        public static void DrawBoneLine(Vector3 w2s_objectStart, Vector3 w2s_objectFinish, Color color)
        {
            if (w2s_objectStart != null && w2s_objectFinish != null)
            {
                Render.DrawLine(new Vector2(w2s_objectStart.x, (float)Screen.height - w2s_objectStart.y), new Vector2(w2s_objectFinish.x, (float)Screen.height - w2s_objectFinish.y), color, 1f);
            }
        }


    }
}
