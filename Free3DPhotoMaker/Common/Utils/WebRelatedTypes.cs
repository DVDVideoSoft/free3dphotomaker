using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.Utils
{
    public class ProxyOptions
    {
        public ProxyOptions()
        {
            m_strAddress = "";
            m_nPort = 80;
            m_strUser = "";
            m_strPw = "";
        }

        public ProxyOptions(string strServer, int nPort = 80)
        {
            m_strAddress = strServer;
            m_nPort = nPort;
            m_strUser = "";
            m_strPw = "";
        }

        public ProxyOptions(string strProxyString)
        {
            if (string.IsNullOrEmpty(strProxyString))
                return;

            int nPos = strProxyString.IndexOf(":");
            if (nPos == -1)
            {
                m_strAddress = strProxyString;
            }
            else
            {
                m_strAddress = strProxyString.Substring(0, nPos);
                string strPort = strProxyString.Substring(nPos + 1);
                int nPort = 80;
                if (int.TryParse(strPort, out nPort))
                    m_nPort = nPort;
            }
        }

        /*todo: public->private*/
        public string m_strAddress = "";
        public int m_nPort = 80;
        public string m_strUser = "";
        public string m_strPw = "";

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(m_strAddress);
        }
    }

    public class UploadTypes
    {
        public delegate string StringUploadResultDelegate(UploadTypes.UploadResult code);

        public static class YouTubeKeys
        {
            public enum VideoPrivacy
            {
                Public = 0,
                Unlisted = 1,
                Private = 2
            }

            public enum TernaryActionPermissions
            {
                NotSet = -1,
                Allowed = 0,
                Moderated = 1,
                Denied = 2
            }
        }

        public enum UploadResult
        {
            Success,
            NoInternetConnect,
            InvalidLogin,
            InvalidParam,
            Cancelled,
            IOError,
            UnknownMediaType,
            OutOfMemory,
            RetryNumberExceeded,
            NetworkError,
            WebError,
            ExceedsLimits,
            ServiceSpecificError,
            UnknownError,
        }

        public class UploadResultInfo
        {
            public UInt64 Id { get; set; }
            public string FileName { get; set; }
            public string URL { get; set; }
            public UploadResult Result { get; set; }
            
            public UploadResultInfo()
            {
                this.Id = 0;
                this.Result = UploadResult.Success;
            }
        }

        public class UploadInfo
        {
            public UploadInfo(string fileName, string title)
            {
                this.Id = 0;
                this.FileName = fileName;
                this.Title = title;
                this.Tags = "";
                this.Desc = title;
                this.Privacy = YouTubeKeys.VideoPrivacy.Public;
                this.CommentsAllowed = YouTubeKeys.TernaryActionPermissions.Allowed;
                this.CommentVoteAllowed = true;
                this.FileSize = 0;
            }

            public override string ToString()
            {
                return FileName;
            }

            public UInt64 Id { get; set; }
            public string FileName { get; set; }
            public string Title { get; set; }
            public string Tags { get; set; }
            public string Desc { get; set; }
            public int Category { get; set; }
            public YouTubeKeys.VideoPrivacy Privacy { get; set; }
            public YouTubeKeys.TernaryActionPermissions CommentsAllowed { get; set; }
            public bool CommentVoteAllowed { get; set; }
            public int Duration { get; set; }
            public Int64 FileSize { get; set; }
        }

        public class UploadTask
        {
            public UploadTask(List<UploadInfo> uploadingItems,
                              List<UploadResultInfo> uploadingResults,
                              string userName,
                              string usersChannel,
                              string password,
                              bool useProxy,
                              string proxyAddress,
                              int proxyPort,
                              string proxyUser,
                              string proxyPassword)
            {
                Items = uploadingItems;
                Results = uploadingResults;

                UserName = userName;
                UserChannelName = usersChannel;
                Password = password;
                UseProxy = useProxy;
                ProxyAddress = proxyAddress;
                ProxyPort = proxyPort;
                ProxyUser = proxyUser;
                ProxyPassword = proxyPassword;
            }

            public List<UploadInfo>   Items;
            public List<UploadResultInfo> Results;

            public string UserName;
            public string UserChannelName;
            public string Password;
            public bool   UseProxy;
            public string ProxyAddress;
            public int    ProxyPort;
            public string ProxyUser;
            public string ProxyPassword;
        }

        public class OnlineMediaInfo
        {
            private IList<string> urls;
            private string author;
            //private string copyright;
            private string duration;

            public IList<string> Urls { get { return this.urls; } set { this.urls = value; } }
            public string Author { get { return this.author; } set { this.author = value; } }
            public string Duration { get { return this.duration; } set { this.duration = value; } }

            public OnlineMediaInfo(IList<string> urls)
            {
                this.urls = urls;
            }
        }
    }
}
