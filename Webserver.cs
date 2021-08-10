using System;
using System.Net;
using System.Windows.Forms;

namespace Webserver
{
    public partial class Webserver : Form
    {
        public Webserver()
        {
            InitializeComponent();
            
            // used for debugging
            bool isTest = true;

            if (isTest)
            {
                // hardcoded variables used for debugging
                textBoxIpaddress.Text = "192.168.183.4";
                textBoxPort.Text = "8282";
                textBoxContent.Text = @"C:\tmp\";
                textBoxMaxConnections.Text = "2";
            }

            buttonStop.Enabled = false;
            buttonStart.Enabled = true;

        }

        // new instance of server class
        readonly Server server = new Server();

       
       
        private void buttonStart_Click(object sender, EventArgs e)
        {
            // parse the ipaddress from the text box into an ipaddress object
            IPAddress ipAddress = IPAddress.Parse(textBoxIpaddress.Text);
            int port = int.Parse(textBoxPort.Text);
            string content = textBoxContent.Text;
            int maxConnections = int.Parse(textBoxMaxConnections.Text);
            
            // start the webserver with the given parameters
            if (server.Start(ipAddress, port, maxConnections, content))
            {
                buttonStart.Enabled = false;
                buttonStop.Enabled = true;
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            // stop the webserver
            server.Stop();
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
        }
    }
}