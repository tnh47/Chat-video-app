using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_video_app.Classes
{
    internal static class FirestoreHelper
    {
        static string fireconfig = @"
        {
          ""type"": ""service_account"",
          ""project_id"": ""chat-video-app-9d1ca"",
          ""private_key_id"": ""d3fba37be6d3e01ce9ab7b24c4761d772f1a885c"",
          ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQC6fFoF4pUaWJRN\nG0AHMeMgUOO89yYh5SuUGSmylPHseM7rDxqYpPSMSgX4sJGEdWveJl0k4mIddaim\nlmhOACgUuBWAqs1pdqhYVLDGgTWlRXKG2+ZcbX6EQXz2nLLk5d924lwvYk6W5fP2\nnloLfi3eLPhsngagQmYJQKIbUxU1CTYEpE3BOL6XPfnKHEyq5LR+WbItigBlvll3\nMjzMBAi5ZBbDoRWCFixDOMcFcfgmIcwUryCmMINscrfNh8m+6rpjTfrl44qtb/hE\nKjscP0q5OKhbEKFke1FYcvPO+YKuEVbuc/2mwGuws+usskWF2yphmEUT0QPBWBkV\noDs/RkyFAgMBAAECggEABH2YqVx5k4uNFYbRvuPAN66R1DTtqJJvdhEUe0FoXNFB\nEAhFyq7qxlfE1wrzuGSxwCBu8XqTzWhgV7hSDmSAkpTd8NTQrAsKuL3N6jwTuMVB\nfS/djhSDMiD33c/VCLJSGNLsB1038mX/dcz+F4dULNuYp0YS5VgvZ/jGUx+Mn1Pa\nshsB54zy2TBsQdLC+NbRW69FHA4WllLa+sBqQQp+nW3QXgg+GRS7K9eujeiMnxoc\nkvkk5suV1FMO12HE1+c8D9UXtNE+DMC3LRJQ1LipixM4PUV55O79QfWuAwDjWlis\nr+V10wgiAm0ybNzYXRHkaZGVhuNYGv8JP0FiJdR4QQKBgQD5lTg2hUNlMwCnNMT6\nBbnS4GhJdpk+HQX6G3SnqUw2UIyard2gEpnexqB8ohBtVr0o/cDCTZXqdza8n07f\ncNXLZhHoW1VWz/d5qkBCCgNBFrt3SK3zbeZms5UluB+Tj1rShQLVmthfYlMxz/kV\nFNYH9Dt3Jsjcjncu9bsvy/5JUQKBgQC/R9H19XzftQxyRKudm33nZf4SdKecd58c\nPdQtDtpEe6K0cf0YBCBU5uSzD5FtBNoQgIh4is/XkJYB4DyKR6xVRGBGaynV62+n\nWhtBjXY68dPzDH/IMuv47WMa2HviOHRZ0ZV95RMBZK8N1YTTh1dYmiJ73FgyOHax\ncNS2HP+C9QKBgByM9l4+RmRoPjXicnoLd6No6mUk/Qi+9zUSOPkJA9/evQbgxs6N\n8i4q2KcJPwnS91aeGJPhnjmxI4DWVIZ4+OwIpBnJgOyqY6v2Tt1/GbozaISPut5y\n/v1Wo8Qp2OrfZktYzeczjHb034F+QgcoWAeW58qMlEOuxHGJyL0B//fRAoGAGCDs\nKFWWMfX5ovRsnY3+vNDN0vAqMH+ab4qRGDn2aIscBB0Y6w2iMnOJx9if5JunEkvU\nazg/bZbMhWIO4PaXk43zgw6DkXuIcxjXtVYt4tOcg91AxTU/NQBk7v796ZRrgOUC\nMYZS9msH9fvILJK2/vM6hFtQOM2Mk2eDPZG83X0CgYEA1HvJ+XStEA4JZTwnRAyD\n1osfIagnlNiP6fLpF62GJhJ2bzSspFqBk7HxRZKzFUAKAPKl4uYYPHdyG2UIegkm\nnvGGuQyWmQuiugeE1YMx4kaSchSDXwa2F/zAEnM26kwptRKXZBhUpKXmPOU7dhbF\n85sKu3MeBWkIrxToaiwCgAQ=\n-----END PRIVATE KEY-----\n"",
          ""client_email"": ""firebase-adminsdk-b14ma@chat-video-app-9d1ca.iam.gserviceaccount.com"",
          ""client_id"": ""109432235401480748939"",
          ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
          ""token_uri"": ""https://oauth2.googleapis.com/token"",
          ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
          ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-b14ma%40chat-video-app-9d1ca.iam.gserviceaccount.com"",
          ""universe_domain"": ""googleapis.com""
        } ";
        static string filepath = "";
        public static FirestoreDb Database { get; private set; }
        public static void SetEnvironmentVariable()
        {
            filepath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetRandomFileName())) + ".json";
            File.WriteAllText(filepath, fireconfig);
            File.SetAttributes(filepath, FileAttributes.Hidden);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            Database = FirestoreDb.Create("chat-video-app-9d1ca");
            File.Delete(filepath);
        }
    }
}
