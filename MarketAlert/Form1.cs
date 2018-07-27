using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;

namespace MarketAlert
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private Timer timer1;
        public int interval = 10000;
        public decimal alertThreshold = 0.05m;
        public int minutes = 5;

        string strDebug = "";
        bool statusLogEnabled = true;
        bool fileLogEnabled = true;
        

        public class BinanceCoin
        {
            [JsonProperty("symbol")]
            public string symbol;
            [JsonProperty("price")]
            public decimal price;
        }

        public class BinanceCoinData
        {
            public string symbol;
            public decimal price;
            public DateTime tstamp;
        }

        public List<BinanceCoinData> coinData = new List<BinanceCoinData>();

        public Form1()
        {
            InitializeComponent();
            lblDebug.Text = "";
            lblMessage.Text = "";
            button1.Focus();
            this.Text = "Coin Alert";
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1 == null)
            {
                InitTimer();
                button1.Text = "Stop";

                timer1_Tick(new object(), new EventArgs());
            }
            else
            {
                timer1.Stop();
                timer1 = null;
                button1.Text = "Start";
            }
        }

        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = interval; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            decimal.TryParse(txtAlertThreshold.Text, out alertThreshold);
            int.TryParse(txtMinutes.Text, out minutes);


            getHttpBinance();

            decimal curPrice = 0;

            if (coinData.Count > 0)
                curPrice = coinData[coinData.Count - 1].price;
            
            decimal startPrice = curPrice;
            DateTime startTime = DateTime.Now;
            
            bool found = false;

            for (int i=coinData.Count-1; i>=0; i--)
            {
                if(!found)
                {
                    TimeSpan t = DateTime.Now - coinData[i].tstamp;

                    if (t.Minutes >= minutes || i == 0)
                    {
                        found = true;
                        startPrice = coinData[i].price;
                        startTime = coinData[i].tstamp;
                    }
                }
            }


            decimal percentChange = 0;
                
            if(startPrice > 0)
                percentChange = (curPrice - startPrice) / startPrice;

            lblMessage.Text = "percent change over " + minutes.ToString() + " minutes: " + (percentChange * 100);

            /*
            strDebug = "";

            for (int i = coinData.Count - 1; i >= 0; i--)
                strDebug += coinData[i].price.ToString() + "\n";

            lblDebug.Text = strDebug;
            */

            if (percentChange > alertThreshold / 100m)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"audio/beep-high.wav");
                player.Play();
                coinData.Clear();

                logStatus("\nPrice up at " + DateTime.Now.ToString() + ".  " + alertThreshold + " percent change within " + minutes + " minutes.\n\n");
                logStatus("from " + Math.Round(startPrice) + " at " + startTime.ToString() + " \nto     " + Math.Round(curPrice) + " at " + DateTime.Now.ToString() + ".\n\n");
            }

            if (percentChange < -alertThreshold / 100m)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"audio/beep-low.wav");
                player.Play();
                coinData.Clear();

                logStatus("\nPrice down at " + DateTime.Now.ToString() + ".  " + alertThreshold + " percent change within " + minutes + " minutes.\n\n");
                logStatus("from " + Math.Round(startPrice) + " at " + startTime.ToString() + " \nto     " + Math.Round(curPrice) + " at " + DateTime.Now.ToString() + ".\n\n");
            }
        }


        public async void getHttpBinance()
        {
            try
            {
                string strData = await client.GetStringAsync("https://api.binance.com/api/v3/ticker/price?symbol=BTCUSDT");

                //List<BinanceCoin> coins = JsonConvert.DeserializeObject<List<BinanceCoin>>(strData);
                BinanceCoin coin = JsonConvert.DeserializeObject<BinanceCoin>(strData);

                if (coin.symbol.Contains("BTC"))
                {
                    BinanceCoinData bcd = new BinanceCoinData();
                    bcd.price = coin.price;
                    bcd.symbol = coin.symbol;
                    bcd.tstamp = DateTime.Now;

                    coinData.Add(bcd);
                }

                //keep it from growing too big
                if (coinData.Count > 1000)
                    coinData = coinData.Skip(1).ToList();
            }

            catch (HttpRequestException e)
            {
                logStatus("httpException: " + e.Message + "\n\n");
                return;
            }

            catch (WebException e)
            {
                logStatus("webException: " + e.Message + "\n\n");
                return;
            }

            catch (Exception e)
            {
                logStatus("exception: " + e.Message + "\n\n");
                return;
            }
        }


        private void logStatus(string msg)
        {
            if (statusLogEnabled)
                txtStatus.Text += msg;

            if (fileLogEnabled)
                File.AppendAllText("log_" + DateTime.Today.ToString("d").Replace('/','_') + ".txt", msg);
        }
    }
}
