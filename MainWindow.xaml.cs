using System;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace LedWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort serialPort1;
        string[] serialPorts2;
        public MainWindow()
        {
            InitializeComponent();
            InitializedSerialPorts();//custom
        }

        /**************************** Operation ******************/
        private void InitializedSerialPorts()
        {
            serialPorts2 = SerialPort.GetPortNames();//Using the System.IO.Ports.SerialPort.GetPortNames() it will retrieve the all available serial port names.
            if ( serialPorts2.Count() != 0 )
            {
                foreach ( string serial in serialPorts2 )
                {
                    var serialItem = SerialPortComboBox.Items;
                    if ( !serialItem.Contains(serial) )//contain used for comparing string with substring
                    {
                        SerialPortComboBox.Items.Add(serial);
                    }
                }
                SerialPortComboBox.SelectedItem = serialPorts2[0];
            }
        }

        // WPF to arduino Connection
        bool isConnectedToArduino = false;
        private void ConnectToArduino()
        {
            try
            {
                string selectedSerialPort = SerialPortComboBox.SelectedItem.ToString(); //get selected port
                serialPort1 = new SerialPort(selectedSerialPort , 9600);
                serialPort1.Open();
                SerialPortConnection.Content = "Disconnect";
                LedControlPanel.IsEnabled = true;
                isConnectedToArduino = true;
            }
            catch ( UnauthorizedAccessException )
            {
                MessageBox.Show("The selected serial port is busy" , "Busy" , MessageBoxButton.OK , MessageBoxImage.Stop);
            }
            catch ( NullReferenceException )
            {
                MessageBox.Show("There is no serial port!" , "Empty Serial port" , MessageBoxButton.OK , MessageBoxImage.Exclamation);
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.ToString());
            }

        }

        /* **************************** All Function ******************************/

        private void DisconnectFromArduino()
        {
            SerialPortConnection.Content = "Connected";
            LedControlPanel.IsEnabled = false;
            isConnectedToArduino = false;
            ResetControl();
            serialPort1.Close();
        }

        private void ResetControl()
        {
            LedOff.Background = Brushes.SaddleBrown;
            LedON.Background = Brushes.Green;
            SliderBrightness.Value = 0;
            lblSliderBrightness.Content = "0";
        }

        private void RefreshBtn_Click(object sender , RoutedEventArgs e)
        {
            InitializedSerialPorts();
        }

        private void SerialPortConnection_Click(object sender , RoutedEventArgs e)
        {
            if ( !isConnectedToArduino )
            {
                ConnectToArduino();
            }
            else
            {
                DisconnectFromArduino();
            }
        }

        private void LedOff_Click(object sender , RoutedEventArgs e)
        {
            serialPort1.Write("256");
            LedOff.Background = Brushes.DarkSeaGreen;
            LedON.Background = Brushes.Green;
        }

        private void LedON_Click(object sender , RoutedEventArgs e)
        {
            serialPort1.Write("257");
            LedOff.Background = Brushes.SaddleBrown;
            LedON.Background = Brushes.YellowGreen;
        }

        private void SliderBrightness_ValueChanged(object sender , RoutedPropertyChangedEventArgs<double> e)
        {
            int sliderValue = (int) SliderBrightness.Value;
            serialPort1.Write(sliderValue.ToString());//send the value of slider to arduino
            serialPort1.Write("-");//as no data to send in loop
            lblSliderBrightness.Content = sliderValue.ToString();
        }
    }
}
