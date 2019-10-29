using IPA;
using IPA.Config;
using IPA.Utilities;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace BeatSaberRESTInterface
{
    public class Plugin : IBeatSaberPlugin
    {
        internal static Ref<PluginConfig> config;
        internal static IConfigProvider configProvider;

        private GameObject gameObject1;
        private GameObject gameObject2;
        private WebRequest webReqest1;
        private WebRequest webReqest2;
        private GameStatus gameStatus = new GameStatus();


        public void Init(IPALogger logger, [Config.Prefer("json")] IConfigProvider cfgProvider)
        {
            Logger.log = logger;
            configProvider = cfgProvider;

            config = cfgProvider.MakeLink<PluginConfig>((p, v) =>
            {
                if (v.Value == null || v.Value.RegenerateConfig)
                    p.Store(v.Value = new PluginConfig() { RegenerateConfig = false });
                config = v;
            });
        }

        public void OnApplicationStart()
        {

        }

        public void OnApplicationQuit()
        {

        }

        public void OnFixedUpdate()
        {

        }

        public void OnUpdate()
        {

        }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
            //Register WebRequest class
            if(nextScene.name == "MenuCore")
            {
                Logger.log.Debug("nextScene.name == MenuCore");
                if(gameObject1 == null)
                {
                    gameObject1 = new GameObject("BeatSaberRESTInterface1");
                    webReqest1 = gameObject1.AddComponent<WebRequest>();
                    webReqest1.Address = config.Value.Address;
                    webReqest1.UpdateInterval = config.Value.UpdateInterval;
                    webReqest1.gameStatus = gameStatus;
                }
            }
            if (nextScene.name == "GameCore")
            {
                Logger.log.Debug("nextScene.name == GameCore");
                if (gameObject2 == null)
                {
                    gameObject2 = new GameObject("BeatSaberRESTInterface2");
                    webReqest2 = gameObject2.AddComponent<WebRequest>();
                    webReqest2.Address = config.Value.Address;
                    webReqest2.Address = config.Value.Address;
                    webReqest2.UpdateInterval = config.Value.UpdateInterval;
                    webReqest2.gameStatus = gameStatus;
                    webReqest2.allowChangeMap = false;
                }
            }
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
     
        }

        public void OnSceneUnloaded(Scene scene)
        {

        }
    }
}
