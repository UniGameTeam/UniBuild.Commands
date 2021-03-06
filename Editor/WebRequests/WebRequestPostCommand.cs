﻿namespace UniModules.UniGame.BuildCommands.Editor.WebRequests
{
    using System;
    using Core.Runtime.Utils;
    using UniBuild.Editor.ClientBuild.Commands.PreBuildCommands;
    using UniBuild.Editor.ClientBuild.Interfaces;
    using UniCore.Runtime.Rx.Extensions;
    using UnityEngine;
    using UnityEngine.Networking;

    [Serializable]
    public class WebRequestPostCommand : UnitySerializablePostBuildCommand
    {
        public string apiUrl = "";

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.BoxGroup("Parameters")]
#endif
        public WebRequestParameters header = new WebRequestParameters() {
            {"Content-Type","application/json"},
            {"Accept","application/json"},
        };
        
        [Space(4)]
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.BoxGroup("Parameters")]
#endif
        public WebRequestParameters parameters = new WebRequestParameters();
        
        public override void Execute(IUniBuilderConfiguration configuration) => Execute();

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void Execute()
        {
            var targetUrl = apiUrl.CombineUrlParameters(parameters);

            var webRequest = UnityWebRequest.Post(targetUrl,string.Empty);
            foreach (var headerParameter in header) {
                webRequest.SetRequestHeader(headerParameter.Key,headerParameter.Value);
            }
            
            var reqeustValue = webRequest.uri;
            Debug.Log($"Send Post to : {reqeustValue}");
            
            var requestAsyncOperation = webRequest.SendWebRequest();
            requestAsyncOperation.completed += x => {
                
                if (webRequest.isNetworkError || webRequest.isHttpError) {
                    Debug.Log(webRequest.error);
                }
                else {
                    Debug.Log($"Request to {apiUrl} complete. Code: {webRequest.responseCode}");
                }

                webRequest.Cancel();
            };
        }
    }
}
