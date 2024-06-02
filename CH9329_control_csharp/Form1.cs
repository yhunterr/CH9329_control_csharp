// https://wowon.tistory.com/321

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CH9329_control_csharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Serial port settings
        public void connect_uart()
        {
            if (!serialPort1.IsOpen)
            {
                if (comboBox1.Text == "")
                {
                    label1.Text = "comBox is null";
                    comboBox1.DataSource = SerialPort.GetPortNames();
                    return;
                }

                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = 9600;
                serialPort1.DataBits = 8;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Parity = Parity.None;

                serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);

                try
                {
                    serialPort1.Open();
                    serialPort1.DiscardOutBuffer();
                    serialPort1.DiscardInBuffer();
                }
                catch
                {
                    label1.Text = "Serial Port Error";
                }
                if(serialPort1.IsOpen)
                    label1.Text = "Serial Port OPEN :"+comboBox1.Text;
                else
                    label1.Text = "Serial Port Not OPEN :" + comboBox1.Text;
            }
            else
            {
                label1.Text = "Serial Port already open";
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                this.Invoke(new EventHandler(serial_DataReceived));
            }
            catch
            {
                label1.Text = "recv error";
            }
        }

        public void disconnect_uart()
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.DataReceived -= new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                serialPort1.Close();
                label1.Text = "Serial Port close";
            }
            else
            {
                label1.Text = "Serial Port already close";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            connect_uart();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            disconnect_uart();
        }
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.DataSource = SerialPort.GetPortNames();

        }
        private void sendByteMsg(byte[] data)
        {
            try
            {
                serialPort1.Write(data, 0, data.Length);
            }
            catch
            {
                label1.Text = "send error";
            }
        }
        #endregion

        #region main function
        int line = 1;
        byte[] array_recv = new byte[7];
        byte array_count = 0;
        public void serial_DataReceived(object s, EventArgs e)
        {
            int recv;
            int start = serialPort1.BytesToRead;
            for (int i = 0; i <= start; i++)
            {
                if (serialPort1.BytesToRead >= 1)
                {
                    recv = serialPort1.ReadByte();
                }
                else
                {
                    return;
                }
                if (recv == 0x57)
                {
                    Array.Clear(array_recv, 0, array_recv.Length);
                    array_count = 0;
                    array_recv[array_count] = (byte)recv;
                    textBox1.AppendText("\r\n");
                    textBox1.Text += line + " : ";
                    line++;
                }
                else
                {
                    array_count++;
                    array_recv[array_count] = (byte)recv;
                }

                if (array_count == 6)
                {
                    array_count = 0;
                    if (keyboard.receive_check(array_recv))
                    {
                        wait_recv = false;
                    }
                    else
                    {
                        sendByteMsg(keyboard.release_keyboard());
                    }
                }
                textBox1.Text += recv + " ";
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
            }
        }

        bool wait_recv = true;
        public void main_func()
        {
            Thread.Sleep(2000);
            string input = textBox2.Text;
            wait_recv = true;
            foreach (byte c in input)
            {
                sendByteMsg(keyboard.press_keyboard((byte)c));
                while (wait_recv)
                {
                }
                wait_recv = true;
                sendByteMsg(keyboard.release_keyboard());
                while (wait_recv)
                {
                }
                wait_recv = true;
            }
        }
#endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = SerialPort.GetPortNames();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(main_func);
            th.Start();
        }
    }
}