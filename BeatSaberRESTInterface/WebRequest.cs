using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace BeatSaberRESTInterface
{
    class WebRequest : MonoBehaviour
    {
        public String Address;
        public double UpdateInterval = 1.0f;

        public GameStatus gameStatus;

        private double time = 0.0f;



        void Update()
        {
            time += Time.deltaTime;
            if (time > UpdateInterval)
            {
                time = 0.0f;
                Logger.log.Debug("Update");

                // Send and retrieve data from a server
                StartCoroutine(PutRequest(Address));
            }
        }

        //private int count = 0;

        private IEnumerator PutRequest(string Address)
        {
            var msgObj = gameStatus.GetStatusMsg();


            //var msgObj = new ActionMsg();
            //if (count == 5)
            //{
            //    msgObj.LevelID = "FeelingStronger";
            //    msgObj.Start = true;
            //}
            //count += 1;    
            

            string msgJson = JsonUtility.ToJson(msgObj);
            byte[] msgData = System.Text.Encoding.UTF8.GetBytes(msgJson);

            using (UnityWebRequest req = UnityWebRequest.Put(Address, msgData))
            {
                req.SetRequestHeader("Content-type", "application/json; charset=UTF-8");

                // Send data and wait for the response
                yield return req.SendWebRequest();

                if (req.isNetworkError)
                {
                    Logger.log.Debug("Network Error: " + req.error);
                }
                else
                {
                    //Logger.log.Debug("Received: " + req.downloadHandler.text);

                    ActionMsg myObject = JsonUtility.FromJson<ActionMsg>(req.downloadHandler.text);
                    gameStatus.SetActionMsg(myObject);
                }
            }
        }

    }
}
