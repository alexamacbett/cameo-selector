using BepInEx;
using HarmonyLib;
using Sunless.Game.ApplicationProviders;
using System;
using System.Collections.Generic;
using Sunless.Game.Utilities;
using Sunless.Game.UI.Officers;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using Sunless.Game.Entities;


namespace CCCameoSelector
{
    [BepInPlugin("mod.clevercrumbish.cameoselector","Cameo Selector", "1.0.1")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Loaded {PluginInfo.PLUGIN_GUID}!");
            DoPatching();
        }

        public static void DoPatching()
        {
            Harmony.CreateAndPatchAll(typeof(AvatarAndNamePanelConstructorPatch));
        }
    }

    [HarmonyPatch(typeof(AvatarAndNamePanel), MethodType.Constructor, new System.Type[1] { typeof(GameObject) })]
    class AvatarAndNamePanelConstructorPatch
    {


        static void Postfix(AvatarAndNamePanel __instance)
        {
            AvatarAndNamePanelHelper.ClearPage(__instance);
            AvatarAndNamePanelHelper.Initialise(__instance);
            AvatarAndNamePanelHelper.LoadPage(__instance);
        }

    }

    public static class AvatarAndNamePanelHelper
    {
        public static int pageNumber = 0;
        public static int maxPageNumber = 0;
        public static List<List<string>> pages = new List<List<string>>();
        public static void Initialise(AvatarAndNamePanel __instance)
        {
            List<String> looseAvatars = new List<string>();
            looseAvatars.AddRange(ImageHelper.GetAvatarNames());
            looseAvatars.AddRange(GetAdditionalAvatarNames());
            int offset = 0;
            int currentPageSize;
            if (looseAvatars.Count <= 21)
            {
                pages.Add(new List<string>());
                pages[0].AddRange(looseAvatars);
                offset = looseAvatars.Count;
            }
            while (offset < looseAvatars.Count)
            {
                while (pages.Count < pageNumber + 1)
                {
                    pages.Add(new List<string>());
                }
                if (pageNumber == 0)
                {
                    currentPageSize = 20;
                }
                else if ((looseAvatars.Count - offset) <= 20)
                {
                    currentPageSize = looseAvatars.Count - offset;
                }
                else
                {
                    currentPageSize = 19;
                }
                pages[pageNumber].AddRange(looseAvatars.Skip(offset).Take(currentPageSize));
                offset += currentPageSize;
                if (offset < looseAvatars.Count)
                {
                    pageNumber++;
                }
            }
            maxPageNumber = pageNumber;
            pageNumber = 0;
        }

        public static void CreateBackButton(AvatarAndNamePanel __instance)
        {
            GameObject gameObject = __instance._avatarGrid.AddChildPreserveTransform(PrefabHelper.Instance.Get("UI/CharacterCreation/AvatarButton"));
            gameObject.GetComponentInChildren<RawImage>().texture = (Texture)"avatars/cc_menu/cc_menu_left".GetIconTexture("");
            gameObject.GetComponent<Button>().OnClick((Action)(() => AvatarAndNamePanelHelper.PreviousPage(__instance)));
        }

        public static void CreateForwardButton(AvatarAndNamePanel __instance)
        {
            GameObject gameObject = __instance._avatarGrid.AddChildPreserveTransform(PrefabHelper.Instance.Get("UI/CharacterCreation/AvatarButton"));
            gameObject.GetComponentInChildren<RawImage>().texture = (Texture)"avatars/cc_menu/cc_menu_right".GetIconTexture("");
            gameObject.GetComponent<Button>().OnClick((Action)(() => AvatarAndNamePanelHelper.NextPage(__instance)));
        }

        public static void CreateBlankButton(AvatarAndNamePanel __instance)
        {
            GameObject gameObject = __instance._avatarGrid.AddChildPreserveTransform(PrefabHelper.Instance.Get("UI/CharacterCreation/AvatarButton"));
            gameObject.GetComponentInChildren<RawImage>().texture = (Texture)"avatars/cc_menu/cc_menu_blank".GetIconTexture("");
        }

        public static void ClearPage(AvatarAndNamePanel __instance)
        {
            __instance._avatarGrid.Clear();
        }

        public static void LoadPage(AvatarAndNamePanel __instance)
        {
            ClearPage(__instance);
            if(pageNumber != 0)
            {
                CreateBackButton(__instance);
            }
            foreach (string avatarName in pages[pageNumber])
                __instance.CreateAvatarButton(avatarName);
            if (pageNumber != maxPageNumber)
            {
                CreateForwardButton(__instance);
            } else if (pages[pageNumber].Count < 20)
            {
                int remainder = 20 - pages[pageNumber].Count;
                if (maxPageNumber == 0)
                {
                    remainder++;
                }
                for (int i = 0; i < remainder; i++)
                {
                    CreateBlankButton(__instance);
                }
            }
        }

        public static void NextPage(AvatarAndNamePanel __instance)
        {
            if (pageNumber < maxPageNumber)
            {
                pageNumber++;
                LoadPage(__instance);
            }
        }

        public static void PreviousPage(AvatarAndNamePanel __instance)
        {
            if (pageNumber > 0)
            {
                pageNumber--;
                LoadPage(__instance);
            }
        }

        public static List<string> GetAdditionalAvatarNames()
        {
            string absoluteFilePath = GameProvider.Instance.GetApplicationPath("images/sn/icons/avatars/");
            return ((IEnumerable<string>)Directory.GetFiles(absoluteFilePath, "*", SearchOption.TopDirectoryOnly)).Select<string, string>((Func<string, string>)(file => file.Replace(absoluteFilePath, "avatars/").Replace(".png", ""))).OrderBy<string, string>((Func<string, string>)(x => x)).ToList<string>();
        }
    }
}
