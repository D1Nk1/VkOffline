using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using VkNet.Model.RequestParams;
using System.Threading;
using VkNet.Enums.SafetyEnums;
using System.Collections;
using SKYPE4COMLib;
using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace VkOffline
{
    public partial class Form1 : Form
    {
        VkApi api = new VkApi();
        private string token = "";
        MessagesGetParams getMessages = new MessagesGetParams();
        public string chat;


        public Form1()
        {
            InitializeComponent();
            api.Authorize(token);

            timer1.Enabled = true;
            timer1.Interval = 1000;



        }

        //private string get(string url)
        //{
        //    string html;
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    request.AutomaticDecompression = DecompressionMethods.GZip;

        //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //    using (Stream stream = response.GetResponseStream())
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        html = reader.ReadToEnd();
        //    }

        //    return html;
        //}

        private void button1_Click(object sender, EventArgs e)
        {

            getMessages.Count = 5;
            getMessages.Out = VkNet.Enums.MessageType.Received;
            VkNet.Model.MessagesGetObject messages = api.Messages.Get(getMessages);
            int i;

            for (i = messages.Messages.Count - 1; i >= 0; i--)
            {
                var p = api.Users.Get(messages.Messages[i].UserId.Value);
                if (messages.Messages[i].ReadState == VkNet.Enums.MessageReadState.Unreaded)
                {
                    if (messages.Messages[i].Title.Except(" ... ").Any())
                    {
                        chat = "\n" + "В беседе: " + messages.Messages[i].Title;
                    }
                    else if (messages.Messages[i].Title == " ... ")
                    {
                        chat = "";
                    }
                    richTextBox1.AppendText(Environment.NewLine + messages.Messages[i].Date + chat + "\n" + p.FirstName + " " + p.LastName + " написал:" + " " + messages.Messages[i].Body + "\n");
                }
            }

            //string url = "https://api.vk.com/method/messages.get?count=5&access_token=" + token;
            //string html = get(url);

            //dynamic qwe = JObject.Parse(html);
            //dynamic qw = qwe.response;
            //for(int i=1; i<qw.Count; i++)
            //{
            //    dynamic msg = qw[i];
            //    MessageBox.Show(msg.body.Value);
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var get = api.Users.Get(20091500);
            //richTextBox2.Text = get.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            getMessages.Count = 1;
            getMessages.Out = VkNet.Enums.MessageType.Received;
            VkNet.Model.MessagesGetObject messages = api.Messages.Get(getMessages);
        }
    }
}
