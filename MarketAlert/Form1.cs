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

                //timerTrades.Stop();
                //timerTrades = null;
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

            bool found = false;

            decimal curPrice = 0;

            if (coinData.Count > 0)
                curPrice = coinData[coinData.Count - 1].price;
            
            decimal startPrice = curPrice;
            DateTime startTime = DateTime.Now;

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


            decimal pct = 0;
                
            if(startPrice > 0)
                pct = (curPrice - startPrice) / startPrice;

            lblMessage.Text = "pct change over " + minutes.ToString() + " minutes: " + (pct * 100);

            /*
            strDebug = "";

            for (int i = coinData.Count - 1; i >= 0; i--)
            {
                strDebug += coinData[i].price.ToString() + "\n";
            }

            lblDebug.Text = strDebug;
            */

            if (pct > alertThreshold / 100m)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"beep-high.wav");
                player.Play();
                coinData.Clear();

                strDebug += "Alert at " + DateTime.Now.ToString() + " for " + alertThreshold + " pct change within " + minutes + " minutes.\n";
                strDebug += "  Price went from " + startPrice + " at " + startTime.ToString() + " to " + curPrice + " at " + DateTime.Now.ToString() + ".\n\n";
            }

            if (pct < -alertThreshold / 100m)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"beep-low.wav");
                player.Play();
                coinData.Clear();

                strDebug += "Alert at " + DateTime.Now.ToString() + " for " + alertThreshold + " pct change within " + minutes + " minutes.\n";
                strDebug += "  Price went from " + startPrice + " at " + startTime.ToString() + " to " + curPrice + " at " + DateTime.Now.ToString() + ".\n\n";
            }

            lblDebug.Text = strDebug;
        }


        public async void getHttpBinance()
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
            if(coinData.Count > 1000)
                coinData = coinData.Skip(1).ToList();

        }
    }
}
