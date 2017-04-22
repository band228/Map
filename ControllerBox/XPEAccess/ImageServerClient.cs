using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;

namespace ETC.Windows.Business.Layer3
{
    /// <summary>
    /// MileStoneImageInfo contains data from goto command
    /// </summary>
    [Serializable]
    public class MileStoneImageInfo
    {
        public static DateTime EPOC = new DateTime(1970, 1, 1, 0, 0, 0);
        private long m_PrevMiliseconds, m_CurrMiliseconds, m_NextMiliseconds;
        private Stream m_Image;
        private bool m_bIsEnd = false;

        public MileStoneImageInfo()
        {
        }

        /// <summary>
        /// Previous Miliseconds from EPOC in UTC
        /// </summary>
        public long PrevMiliseconds
        {
            get { return m_PrevMiliseconds; }
            set { m_PrevMiliseconds = value; }
        }

        /// <summary>
        /// Next Miliseconds from EPOC in UTC
        /// </summary>
        public long NextMiliseconds
        {
            get { return m_NextMiliseconds; }
            set { m_NextMiliseconds = value; }
        }

        /// <summary>
        /// Current Miliseconds from EPOC in UTC
        /// </summary>
        public long CurrMiliseconds
        {
            get { return m_CurrMiliseconds; }
            set { m_CurrMiliseconds = value; }
        }

        public Stream Image
        {
            get { return m_Image; }
            set { m_Image = value; }
        }

        public static long MilisecondsToTicks(long TotalMiliseconds)
        {
            DateTime temp = EPOC;
            temp.AddMilliseconds(TotalMiliseconds);
            TimeSpan t = temp.Subtract(EPOC);
            return t.Ticks;
        }

        public bool IsEnd
        {
            get { return m_bIsEnd; }
            set { m_bIsEnd = value; }
        }

        /// <summary>
        /// GetTimeFromEPOCMiliseconds translate specific time from EPOC into DateTime (UTC format)
        /// </summary>
        /// <param name="TotalMiliseconds"></param>
        /// <returns></returns>
        public DateTime GetTimeFromEPOCMiliseconds(long TotalMiliseconds)
        {
            DateTime temp = EPOC;
            temp = temp.AddMilliseconds(TotalMiliseconds);
            return temp;
        }
    }

    /// <summary>
    /// Summary description for ImageServer.
    /// </summary>
    public class ImageServerClient
    {
        public static DateTime EPOC = new DateTime(1970, 1, 1, 0, 0, 0);
        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        //Uri TestUri;		
        public ArrayList cameraNames = new ArrayList();
        string addr;
        int portNumber;
        string user;
        string password;
        long epocOffset = new DateTime(1970, 1, 1).Ticks / 10000;
        XmlDocument configuration = null;

        private string m_sErrorDescription;

        private static int requestNr = 10;
        private static int imageNr = 1;
        private string m_LastCameraId = "";

        private Socket m_ISSocket;

        #region Private functions
        private void GetConfigFromSystemInfo(XmlDocument doc, string username)
        {

            if (doc != null)
                foreach (XmlNode enginenode in doc.SelectNodes("/methodresponse/engines/engine"))
                {
                    Uri uri = (new UriBuilder("http", enginenode["hostname"].InnerText, Convert.ToInt32(enginenode["port"].InnerText))).Uri;
                    string recordername = enginenode.Attributes["name"].Value.ToString();

                    foreach (XmlNode cameranode in enginenode.SelectNodes("cameras/camera"))
                    {
                        XmlNode securitynode = cameranode.SelectSingleNode("userrights");

                        Uri camerauri = (new UriBuilder("http", cameranode["hostname"].InnerText, Convert.ToInt32(cameranode["port"].InnerText))).Uri;
                        string cameraName = cameranode.Attributes["cameraid"].Value.ToString();
                        cameraNames.Add(cameraName);

                        //TODO fill in your own code to extract needed parameters
                    }
                }
        }



        private XmlDocument GetImageServerDocument(Uri uri, string username, string password)
        {
            //convert username and password into base64 encoded string
            string base64userpass = System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password));

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Authorization", "Basic " + base64userpass);
            try
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader responseStreamReader = new StreamReader(httpWebResponse.GetResponseStream());
                    try
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(responseStreamReader);
                        return doc;
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Trace.WriteLine(e.GetType() + ": " + e.Message + "\n" + e.StackTrace);
                        throw e;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.GetType() + ": " + e.Message + "\n" + e.StackTrace);
                //throw e;
            }

            return null;
        }

        private void send(Socket sock, string request)
        {
            byte[] bbuf = new byte[1000];
            int cnt = encoder.GetBytes(request, 0, request.Length, bbuf, 0);
            sock.Send(bbuf, cnt, SocketFlags.None);
        }

        private string receive(Socket sock)
        {
            bool done = false;
            byte[] bytebuf = new byte[1000];
            string receiveBuffer = "";
            int cnt = 0;
            while (!done)
            {
                cnt = sock.Receive(bytebuf, 0, 1000, SocketFlags.Partial);

                char[] charBuf = encoder.GetChars(bytebuf, 0, cnt);
                receiveBuffer += new String(charBuf);
                if (receiveBuffer.IndexOf("\r\n\r\n") != -1)
                    done = true;
            }
            return receiveBuffer;
        }

        private bool receiveImage(Socket sock, out ArrayList headers, out byte[] JPEGimage)
        {
            bool done = false;
            byte[] bytebuf = new byte[1000];
            ArrayList headerLines = new ArrayList();
            int cnt = 0;
            int ofz = 0;
            bool inHeader = true;
            int binaryOfz = 0;
            byte[] image = null;
            headers = null;
            JPEGimage = null;
            int imageSize = 0;
            while (!done)
            {
                cnt = sock.Receive(bytebuf, 0, 1000, SocketFlags.Partial);
                int ix = 0;
                while (ix < cnt - 3)
                {
                    if (inHeader)
                    {
                        if (bytebuf[ix] == 0x0d && bytebuf[ix + 1] == 0x0a)
                        {
                            char[] charBuf = encoder.GetChars(bytebuf, ofz, ix - ofz);
                            headerLines.Add(new String(charBuf));

                            ofz = ix + 2;
                            if (bytebuf[ix + 2] == 0x0d && bytebuf[ix + 3] == 0x0a)
                            {
                                inHeader = false;
                                ix += 4;
                                foreach (string header in headerLines)
                                {
                                    if (header.StartsWith("Content-length:"))
                                    {
                                        imageSize = Convert.ToInt32(header.Substring(16));
                                        image = new byte[imageSize + 4];
                                    }
                                }

                            }
                            else
                                ix += 2;
                        }
                        else
                            ix++;
                    }
                    else
                    {
                        if (image == null) return false;
                        while (ix < cnt)
                            image[binaryOfz++] = bytebuf[ix++];
                    }
                    done = binaryOfz >= imageSize;
                }

            }
            headers = headerLines;
            JPEGimage = image;
            return true;
        }
        #endregion

        public ImageServerClient(String addr, int portNumber, String user, String password)
        {
            this.addr = addr;
            this.portNumber = portNumber;
            this.user = user;
            this.password = password;

            try
            {
                Uri uri = new UriBuilder("http", addr, portNumber, "/systeminfo.xml").Uri;
                configuration = GetImageServerDocument(uri, user, password);
                if (configuration == null) throw new Exception("Your computer could not connect to Image Server or Username or Password is wrong!");
                GetConfigFromSystemInfo(configuration, user);
                //uri = new Uri("http://" + addr + ":" + portNumber.ToString() + "/sessionhandler.xml?command=sessionstart HTTP/1.1");
                //configuration = GetImageServerDocument(uri, user, password);

                m_ISSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPHostEntry hostEntry = System.Net.Dns.Resolve(addr);
                IPEndPoint endPoint = new IPEndPoint(hostEntry.AddressList[0], portNumber);
                m_ISSocket.Connect(endPoint);
                if (!m_ISSocket.Connected) throw new Exception("Cannot connect to " + hostEntry.AddressList[0].ToString() + " " + portNumber.ToString());
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("unreachable host"))
                {
                    throw new Exception("Your computer could not connect to Image Server or Username or Password is wrong!");
                }
                else throw ex;
            }
        }

        ~ImageServerClient()
        {
            try
            {
                m_ISSocket.Close();
            }
            catch
            {
            }
        }

        public string ErrorDescription
        {
            get
            {
                string sTemp = m_sErrorDescription;
                m_sErrorDescription = "";
                return sTemp;
            }
        }

        /// <summary>
        /// GetImage retrives an image at the given miliseconds from EPOC, from the defined CameraId
        /// </summary>
        /// <param name="cameraId"></param>
        /// <param name="milisecondssinceepoc"></param>
        /// <returns></returns>
        public MileStoneImageInfo GetImage(string cameraId, double milisecondssinceepoc)
        {
            DateTime curr = EPOC;
            curr = curr.AddMilliseconds(milisecondssinceepoc);
            return GetImage(cameraId, curr.Ticks);
        }

        public MileStoneImageInfo PrevImage(string cameraId)
        {
            MileStoneImageInfo retInfo = new MileStoneImageInfo();
            Socket mySocket = m_ISSocket;
            requestNr++;
            string request = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><methodcall><requestid>" + requestNr + "</requestid>" +
                    "<methodname>previous</methodname></methodcall>\r\n\r\n";
            send(mySocket, request);
            ArrayList headers;
            byte[] JPEGimage;
            if (receiveImage(mySocket, out headers, out JPEGimage))
            {
                foreach (string line in headers)
                {
                    if (line.StartsWith("Prev:"))
                    {
                        string sTemp = line.Substring(5).Trim();
                        long prev = long.Parse(sTemp);
                        retInfo.PrevMiliseconds = prev;
                    }
                    if (line.StartsWith("Current:"))
                    {
                        string sTemp = line.Substring(8).Trim();
                        long curr = long.Parse(sTemp);
                        retInfo.CurrMiliseconds = curr;
                    }
                    if (line.StartsWith("Next:"))
                    {
                        string sTemp = line.Substring(5).Trim();
                        long next = long.Parse(sTemp);
                        retInfo.NextMiliseconds = next;
                    }
                    else
                        if (line.StartsWith("Content-type:"))
                            if (line.IndexOf("image/jpeg") != -1)
                            {
                                imageNr++;

                                retInfo.Image = new MemoryStream();
                                retInfo.Image.Write(JPEGimage, 0, JPEGimage.Length);
                            }
                }
            }
            return retInfo;
        }

        public MileStoneImageInfo NextImage(string cameraId)
        {
            MileStoneImageInfo retInfo = new MileStoneImageInfo();
            Socket mySocket = m_ISSocket;
            requestNr++;
            string request = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><methodcall><requestid>" + requestNr + "</requestid>" +
                    "<methodname>next</methodname></methodcall>\r\n\r\n";
            send(mySocket, request);
            ArrayList headers;
            byte[] JPEGimage;
            if (receiveImage(mySocket, out headers, out JPEGimage))
            {
                foreach (string line in headers)
                {
                    if (line.StartsWith("Prev:"))
                    {
                        string sTemp = line.Substring(5).Trim();
                        long prev = long.Parse(sTemp);
                        retInfo.PrevMiliseconds = prev;
                    }
                    if (line.StartsWith("Current:"))
                    {
                        string sTemp = line.Substring(8).Trim();
                        long curr = long.Parse(sTemp);
                        retInfo.CurrMiliseconds = curr;
                    }
                    if (line.StartsWith("Next:"))
                    {
                        string sTemp = line.Substring(5).Trim();
                        long next = long.Parse(sTemp);
                        retInfo.NextMiliseconds = next;
                    }
                    else
                        if (line.StartsWith("Content-type:"))
                            if (line.IndexOf("image/jpeg") != -1)
                            {
                                imageNr++;

                                retInfo.Image = new MemoryStream();
                                retInfo.Image.Write(JPEGimage, 0, JPEGimage.Length);
                            }
                }
            }
            return retInfo;
        }

        /// <summary>
        /// GetImage retrives an image at the given UTC time, from the defined CameraId
        /// </summary>
        /// <param name="cameraId"></param>
        /// <param name="ticks"></param>
        /// <returns></returns>
        public MileStoneImageInfo GetImage(string cameraId, long ticks)
        {
            try
            {
                MileStoneImageInfo retInfo = new MileStoneImageInfo();

                Socket mySocket = m_ISSocket;
                //Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //IPHostEntry hostEntry = System.Net.Dns.Resolve(addr);
                //IPEndPoint endPoint = new IPEndPoint(hostEntry.AddressList[0], portNumber);
                //mySocket.Connect(endPoint);
                //if (!mySocket.Connected) throw new Exception("Không thể kết nối tới " + hostEntry.AddressList[0].ToString() + " " + portNumber.ToString());

                string request = "", response = "";

                m_LastCameraId = "";
                requestNr++;
                request = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><methodcall><requestid>" + requestNr + "</requestid>" +
                    "<methodname>connect</methodname><username>" + user + "</username>" +
                    "<password>" + password + "</password>" +
                    "<cameraid>" + cameraId + "</cameraid>" +
                    "</methodcall>\r\n\r\n";

                send(mySocket, request);
                response = receive(mySocket);
                if (response.Contains("<connected>no</connected>")) throw new Exception("Cannot connect to camera " + cameraId + "!");

                //TODO check response for OK

                requestNr++;
                DateTime t = new DateTime(ticks);
                //long tickLocal = t.ToLocalTime().Ticks;

                //long epoc = (tickLocal / 10000) - epocOffset;

                TimeSpan span = t.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
                long epoc = Convert.ToInt64(span.TotalMilliseconds);
                request = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><methodcall><requestid>" + requestNr + "</requestid>" +
                        "<methodname>goto</methodname><time>" + epoc + "</time></methodcall>\r\n\r\n";

                send(mySocket, request);
                ArrayList headers;
                byte[] JPEGimage;

                if (receiveImage(mySocket, out headers, out JPEGimage))
                {
                    //mySocket.Close();
                    foreach (string line in headers)
                    {
                        if (line.StartsWith("Prev:"))
                        {
                            string sTemp = line.Substring(5).Trim();
                            long prev = long.Parse(sTemp);
                            retInfo.PrevMiliseconds = prev;
                        }
                        if (line.StartsWith("Current:"))
                        {
                            string sTemp = line.Substring(8).Trim();
                            long curr = long.Parse(sTemp);
                            retInfo.CurrMiliseconds = curr;
                        }
                        if (line.StartsWith("Next:"))
                        {
                            string sTemp = line.Substring(5).Trim();
                            long next = long.Parse(sTemp);
                            retInfo.NextMiliseconds = next;
                        }
                        else
                        if (line.StartsWith("Content-type:"))
                            if (line.IndexOf("image/jpeg") != -1)
                            {
                                imageNr++;

                                retInfo.Image = new MemoryStream();
                                retInfo.Image.Write(JPEGimage, 0, JPEGimage.Length);
                            }
                    }
                }
                //mySocket.Close();

                return retInfo;
            }
            catch (Exception e)
            {
                m_sErrorDescription = e.Message;
                System.Diagnostics.Trace.WriteLine(e.GetType() + ": " + e.Message + "\n" + e.StackTrace);
                //throw e;
                //TODO:recheck
                return null;
            }

        }

        public override string ToString()
        {
            if (configuration == null)
                return "No XML configuration available.";
            else
            {
                //string fileName = "Configuration.xml";
                //configuration.Save(fileName);
                //StreamReader sr = new StreamReader(fileName);
                //string text = sr.ReadToEnd();
                //sr.Close();
                return "XML";
            }
        }

    }
}
