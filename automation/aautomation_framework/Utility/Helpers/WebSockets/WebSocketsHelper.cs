using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Net;


namespace aautomation_framework.Utility.Helpers.WebSockets
{
    public delegate bool ConditionVerifiedDelegate<in T>(T model, string id = null);

    public class WebSocketsHelper
    {
        private WebSocket webSocket;
        private string sessionId;
        private string webSocketUrl;

        public WebSocketsHelper(string sessionId)
        {
            this.sessionId = sessionId;
        }

        /// <summary>
        /// Method recieves full endPoint as a param and replaces the http protocol to ws protocol depends on whether or not it is secure
        /// </summary>
        /// <param name="endPoint">full endPoint to open WS conn</param>
        private void GetWebSocketUrl(string endPoint)
        {
            Dictionary<string, string> patterns = new Dictionary<string, string>() { { "wss://", @"(?<=https:\/\/).*" }, { "ws://", @"(?<=http:\/\/).*" } };
            foreach (KeyValuePair<string, string> pattern in patterns)
            {
                Regex regex = new Regex(pattern.Value);
                var result = regex.Match(endPoint);
                if (result.Success)
                {
                    webSocketUrl = $"{pattern.Key}{result.Value}";
                }
            }
        }

        private void OpenWebSocketConnection(string endPoint)
        {
            GetWebSocketUrl(endPoint);
            webSocket = new WebSocket(webSocketUrl);
            webSocket.SetCookie(new Cookie("usersession", sessionId));
            webSocket.Connect();
        }

        private void CloseWebSocketConnection()
        {
            webSocket.Close();
        }
        public bool WaitForTargetCondition<T>(string endPoint, ConditionVerifiedDelegate<T> conditionDel, string id = null, int timeOut = 60)
        {
            T modelToParse = default(T);
            OpenWebSocketConnection(endPoint);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            webSocket.OnMessage += (sender, e) => modelToParse = DeserializeJsonHelper.TryParseJson<T>(e.Data);
            try
            {
                while (timer.Elapsed.TotalSeconds <= timeOut && webSocket.IsAlive)
                {
                    if (conditionDel != null && conditionDel(modelToParse, id))
                    {
                        return true;
                    }
                    Thread.Sleep(500);
                }
            }
            finally
            {
                CloseWebSocketConnection();
            }
            return false;
        }
    }
}
