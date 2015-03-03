using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using LitJson;
using System;
using Vbaccelerator.Components.Algorithms;
using System.IO;
using System.Windows.Media;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Threading;

namespace PuzzleCreator
{
    public class NetworkController
    {

        public string ip;
        public int port;
        public int timeOutSec;

        public string broadcastIP;
        public int broadcastPort;

        public Dictionary<System.Timers.Timer, AbstractTransferObject> timeOutTimers;

        private ConnectionManager connManager;
        public bool connected;

        private string log;
        public string clientID;

        public Parameters parameters;

        public NetworkController(Parameters para)
        {
            parameters = para;
        }

        public void Start()
        {
            timeOutSec = 60000;
            connected = false;
            connManager = new ConnectionManager();
            timeOutTimers = new Dictionary<System.Timers.Timer, AbstractTransferObject>();
           
        }

        private void broadCastTest()
        {
            broadcastOpen();
            broadcastReceive();
            broadcastSend();
        }

        void OnApplicationQuit()
        {
            closeConn();
        }

        public void broadcastOpen()
        {
            connManager.open(ClientType.Configurator, ConnectionType.Broadcast, new WLANConnectionDef(broadcastIP, broadcastPort), new ConnectionDelegates.ConnectedHandler(connectedCallback));
        }

        public void broadcastSend()
        {
            SimpleParameterTransferObject spto = new SimpleParameterTransferObject(Command.AreYouThere, null, null);
           // Console.WriteLine(spto.toJson());
            connManager.send(spto, new ConnectionDelegates.SentHandler(sentCallback));
        }

        public void broadcastReceive()
        {
            connManager.receive(new ConnectionDelegates.ReceivedHandler(receiveCallback));
        }

        public void openConn()
        {
            Thread openConnThread = new Thread(openConnHelper);
            openConnThread.Start();
        }

        private void openConnHelper()
        {
            connManager.open(ClientType.Configurator, ConnectionType.WLAN, new WLANConnectionDef(ip, port), new ConnectionDelegates.ConnectedHandler(connectedCallback));
            connManager.receive(new ConnectionDelegates.ReceivedHandler(receiveCallback));
        }

        public void sendConn(AbstractTransferObject obj)
        {
           // Console.WriteLine("Sent command: " + obj.msgType);
            connManager.send(obj, new ConnectionDelegates.SentHandler(sentCallback));

            if (!(obj is FlagTransferObject))
            {
                System.Timers.Timer timer = new System.Timers.Timer(timeOutSec * 1000);
                timer.Elapsed += timer_Elapsed;
                //timer.Start();
                timeOutTimers.Add(timer, obj);
            }
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
           // Console.WriteLine("Packet timed out, resending");
            System.Timers.Timer timer = (System.Timers.Timer)sender;
            AbstractTransferObject obj = timeOutTimers[timer];
            timeOutTimers.Remove(timer);
            sendConn(obj);
        }

        public void closeConn()
        {
            connManager.close();
            connected = false;
        }

        private void connectedCallback()
        {
            connected = true;
          //  Console.WriteLine("connected");

            // send images to server
            //sendImagesToServer();

            SimpleParameterTransferObject registerPackage = new SimpleParameterTransferObject(Command.Register, null, null);
            sendConn(registerPackage);
        }

        private void sentCallback()
        {
            Console.WriteLine("Sent message");
        }

        private void receiveCallback(string response)
        {
            //response = response.Remove(response.Length - 1);
            //Console.WriteLine("received: " + response);


            JsonData responseData = null;
            long crc = 0;
            try
            {
                responseData = JsonMapper.ToObject(response);
                crc = long.Parse(responseData["checkSum"].ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            int recSeqID = (int)responseData["seqID"];

            bool checkSumCorrect = checkCheckSum(response, crc);


            if (checkSumCorrect)
            {
                if (responseData.Keys.Contains("flags"))
                {
                    //Check if checksum is true, then stop the timer
                    if (bool.Parse(responseData["flags"]["ack"].ToString()))
                    {
                        foreach (System.Timers.Timer t in timeOutTimers.Keys)
                        {
                            AbstractTransferObject obj = timeOutTimers[t];
                            if (obj.seqID == recSeqID)
                            {
                                t.Stop();
                                timeOutTimers.Remove(t);
                                break;
                            }
                        }
                    }
                }
                else if (responseData.Keys.Contains("appMsg"))
                {
                    FlagTransferObject fto = new FlagTransferObject(true, false, recSeqID);
                    parameters._networkController.sendConn(fto);

                    Trace.WriteLine("Acknowledged " + recSeqID);

                    parseCommand(responseData["appMsg"].ToString());
                }
               // Console.WriteLine("Checksum correct");
            }
            else
            {
                //Console.WriteLine("CheckSum false");
                FlagTransferObject fto = new FlagTransferObject(false, false, recSeqID);
                parameters._networkController.sendConn(fto);
            }

            //connManager.receive(new ConnectionDelegates.ReceivedHandler(receiveCallback));

        }

        public void setPuzzle(int id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("id", id);
            SimpleParameterTransferObject setPuzzleTO = new SimpleParameterTransferObject(Command.SetPuzzle, clientID, parameters);
            sendConn(setPuzzleTO);
        }

        public void parseCommand(String base64AppMsg)
        {
                JsonData appMsg = JsonMapper.ToObject(Base64.Base64Decode(base64AppMsg));
                Command receivedCommand = CommandMethods.getCommand(appMsg["msgType"].ToString());
               // Console.WriteLine("received Command: " + receivedCommand);

                switch (receivedCommand)
                {
                    case Command.Registered:
                        clientID = appMsg["clientID"].ToString();
                        MessageBox.Show("Connection established");
                        break;
                    case Command.PuzzleCreated:
                        
                        bool success = (bool) appMsg["msgData"]["success"];
                        if (!success)
                        {
                            MessageBox.Show("Puzzle was not created!!");
                        }
                        else
                        {
                            int puzzleId = int.Parse( appMsg["msgData"]["id"].ToString());
                            this.sendPuzzleParts(puzzleId);
                        }
                        break;
                    case Command.PuzzleList:
                        
                        JsonData jsonPuzzleList = appMsg["msgData"]["puzzleList"];
                        if (jsonPuzzleList.IsArray){
                            var puzzleList = new List<Puzzle>();

                            foreach (JsonData puzzle in jsonPuzzleList)
                            {
                                var id = int.Parse(puzzle["id"].ToString());
                                var name = puzzle["name"].ToString();
                                puzzleList.Add(new Puzzle(name,id));
                            }

                            parameters.mainWindow.ShowPuzzleListWindow(puzzleList);
                        }
                        break;
                        
                    case Command.PuzzlePartList:

                        JsonData puzzlePieces = appMsg["msgData"]["partList"];


                        if (puzzlePieces.IsArray)
                        {
                            Dictionary<int, Guid> puzzlePartIds = new Dictionary<int, Guid>();
                            int count = puzzlePieces.Count;

                            for (int i = 0; i < count; i++)
                            {
                                puzzlePartIds.Add((int)puzzlePieces[i]["id"], new Guid((string)puzzlePieces[i]["barCode"]));
                            }

                            if (!parameters.puzzlePieces.ContainsKey(parameters.actualPuzzleName))
                                parameters.puzzlePieces.Add(parameters.actualPuzzleName, puzzlePartIds);

                            foreach (int i in puzzlePartIds.Keys)
                            {
                                Dictionary<string, object> paramsDict = new Dictionary<string, object>();
                                paramsDict.Add("id", i);
                                SimpleParameterTransferObject getImagePackage = new SimpleParameterTransferObject(Command.GetImage, clientID, paramsDict);
                                parameters._networkController.sendConn(getImagePackage);
                                Thread.Sleep(300);
                            }
                        }
                        break;
                     case Command.GetImageResponse:

                        string base64 = appMsg["msgData"]["base64Image"].ToString();
                        int imageID = int.Parse(appMsg["msgData"]["id"].ToString());

                        byte[] imgData = System.Convert.FromBase64String(base64);
                        BitmapImage img = ToImage(imgData);
                        PuzzlePiece piece = new PuzzlePiece((parameters.puzzlePieces[parameters.actualPuzzleName])[imageID], img);
                        parameters.ActualPuzzlepices.Add(imageID, piece);
                        if (parameters.ActualPuzzlepices.Count == 9)
                        {


                            parameters.allPartsReceived = true;
                            MessageBox.Show("All puzzlepieces received");
                        }
                        /*else
                        {
                            MessageBox.Show(parameters.ActualPuzzlepices.Count.ToString());
                        }*/
                        break;
                    default:
                        break;


                }
        }

        public BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private bool checkCheckSum(string response, long crc)
        {
            string responseWithoutChecksum = response.Remove(response.IndexOf(",\"checkSum")) + "}";
            byte[] bytes = Encoding.UTF8.GetBytes(responseWithoutChecksum);
            long crcVal = CRC32.calcCrc32(bytes);
            //Console.WriteLine("Got Checksum: " + crc + ", Calc crc: " + crcVal);

            if (crc == crcVal)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void getPuzzleItemList(String puzzleName)
        {
            parameters.actualPuzzleName = puzzleName;
            
            Dictionary<string, object> para = new Dictionary<string, object>();
            para.Add("clientID", clientID.ToString());
            para.Add("id", parameters.actualPuzzleID); // puzzleID
            SimpleParameterTransferObject sto = new SimpleParameterTransferObject(Command.GetPuzzlePartList, clientID, para);
            sendConn(sto);
        }

        public void getPuzzleList()
        {
            SimpleParameterTransferObject sto = new SimpleParameterTransferObject(Command.GetPuzzleList, clientID, null);
            sendConn(sto);
        }

        public void CreatePuzzle(String puzzleName)
        {
            this.parameters.puzzleCreated = false;

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("clientID", clientID.ToString());
            parameters.Add("name", puzzleName);

            SimpleParameterTransferObject createPuzzlePackage = new SimpleParameterTransferObject(Command.CreatePuzzle, null, parameters);
            sendConn(createPuzzlePackage);

        }

        private LinkedList<BitmapImage> _images;

        public void sendPuzzleParts(int puzzleId)
        {
            Dictionary<string, object> para = new Dictionary<string, object>();

            for (int i = 0; i < _images.Count; i++)
            {
                ImageSourceConverter imgConv = new ImageSourceConverter();
                byte[] data;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(
                    BitmapFrame.Create(
                        (BitmapImage)

                                _images.ElementAt(i)
                    )
                );

                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }

                String bytes = Convert.ToBase64String(data);
                para = new Dictionary<string, object>();
                
                para.Add("order", i);
                para.Add("puzzleID", puzzleId); // id vom puzzle
                para.Add("base64Image", bytes);
                para.Add("barCode", System.Guid.NewGuid().ToString());
                SimpleParameterTransferObject createPiecePackage = new SimpleParameterTransferObject(Command.CreatePuzzlePart, null, para);
                sendConn(createPiecePackage);
            }

            MessageBox.Show("Puzzle send to server");
        }

        public void sendImagesToServer(LinkedList<BitmapImage> images, String puzzleName)
        {
            _images = images;
            CreatePuzzle(puzzleName);
        }
    }
}
