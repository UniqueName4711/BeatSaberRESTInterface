using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BS_Utils.Gameplay;

using SongDataCore;
using UnityEngine;

namespace BeatSaberRESTInterface
{
    public class GameStatus
    {
        private int score = 0;
        private bool startMap = false;

        private SoloFreePlayFlowCoordinator soloFreePlayFlowCoordinator;

        private LevelPackLevelsViewController levelPackLevelsViewController;

        private StandardLevelDetailViewController standardLevelDetailViewController;

        private BeatmapLevelsModelSO beatmapLevelsModelSO;
        private IBeatmapLevelPackCollection levelPackCollection;


        public GameStatus()
        {
            BSEvents.OnLoad();
            BSEvents.scoreDidChange += OnScoreDidChange;
        }

        public StatusMsg GetStatusMsg()
        {
            StatusMsg msg = new StatusMsg();
            msg.UserName = GetUserName();
            msg.UserId = GetUserId();
            msg.Score = GetScore();
            return msg;
        }

        public void SetActionMsg(ActionMsg msg)
        {
            SetLevel(msg.LevelID, msg.Start);
        }

        public void SetLevel(string levelID, bool start = false)
        {
            initMapVariables();

            if (levelID == "")
            {
                return;
            }

            var level = FindMap(levelID);
            if(level == null)
            {
                return;
            }

            startMap = start;
            levelPackLevelsViewController.HandleLevelPackLevelsTableViewDidSelectLevel(null, level);
        }


        public string GetUserName()
        {
            return GetUserInfo.GetUserName();
        }

        public ulong GetUserId()
        {
            return GetUserInfo.GetUserID();
        }

        public int GetScore()
        {
            return score;
        }

        private void initMapVariables()
        {
            if (soloFreePlayFlowCoordinator == null)
            {
                // has a start function
                soloFreePlayFlowCoordinator = Resources.FindObjectsOfTypeAll<SoloFreePlayFlowCoordinator>().FirstOrDefault();

                // has a set level function
                levelPackLevelsViewController = Resources.FindObjectsOfTypeAll<LevelPackLevelsViewController>().FirstOrDefault();

                // has an event, that is called, when a level finished loading
                standardLevelDetailViewController = Resources.FindObjectsOfTypeAll<StandardLevelDetailViewController>().FirstOrDefault();
                standardLevelDetailViewController.didPresentContentEvent +=
                    new Action<StandardLevelDetailViewController, StandardLevelDetailViewController.ContentType>
                    (this.HandleLevelDetailViewControllerDidPresentContent);

                // all the beat saber levels
                beatmapLevelsModelSO = Resources.FindObjectsOfTypeAll<BeatmapLevelsModelSO>().FirstOrDefault();
                levelPackCollection = beatmapLevelsModelSO.allLoadedBeatmapLevelPackCollection;
            }
        }

        private IPreviewBeatmapLevel FindMap(string levelID)
        {
            uint i = 0;
            string packName = "";
            while(i < levelPackCollection.beatmapLevelPacks.Length && packName != "Custom Maps")
            {
                packName = levelPackCollection.beatmapLevelPacks[i].packName;

                var beatmapLevelPack = levelPackCollection.beatmapLevelPacks[i];
                var beatmapLevelCollection2 = beatmapLevelPack.beatmapLevelCollection;
                var beatmapLevels = beatmapLevelCollection2.beatmapLevels;

                for (uint j = 0; j < beatmapLevels.Length; ++j)
                {
                    if(beatmapLevels[j].levelID == levelID)
                    {
                        return beatmapLevels[j];
                    }
                }

                ++i;
            }

            return null;
        }


        // #######+++++++ Event Handlers +++++++#######

        private void OnScoreDidChange(int rawScore, int multipliedScore)
        {
            score = multipliedScore;
        }


        public void HandleLevelDetailViewControllerDidPresentContent(StandardLevelDetailViewController viewController, StandardLevelDetailViewController.ContentType contentType)
        {
            if (viewController.isActiveAndEnabled && contentType == StandardLevelDetailViewController.ContentType.OwnedAndReady)
            {
                Logger.log.Debug("#####ready#####");
                if(startMap == true)
                {
                    startMap = false;
                    soloFreePlayFlowCoordinator.StartLevel(null, false);
                }
            }
        }
    }
}
