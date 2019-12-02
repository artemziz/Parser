using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace Parser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = Directory.GetCurrentDirectory() + "thrlist.xlsx.";
        string jsonPath = Directory.GetCurrentDirectory() + "thrlist.txt";
        
        ThreatDB db = new ThreatDB();
        public int pageCount = 0;
        

       
        public MainWindow()
        {
            
            InitializeComponent();
            if (File.Exists(path))
            {
                if (File.Exists(jsonPath))
                {
                   
                    db.threats = JsonConvert.DeserializeObject<List<Threat>>(File.ReadAllText(jsonPath));
                    db.CreatePages();
                }
                else
                {
                    db.Parse(path);
                    db.CreatePages();
                    File.WriteAllText(jsonPath, JsonConvert.SerializeObject(db.threats));
                    
                }

                



            }
            else
            {
                MessageBox.Show("Будет скачан файл!");
                using (var c = new WebClient())
                {
                    c.DownloadFile("https://bdu.fstec.ru/documents/files/thrlist.xlsx", path);
                    db.Parse(path);
                }

                db.CreatePages();
                File.WriteAllText(jsonPath, JsonConvert.SerializeObject(db.threats));
                
                

            }

            foreach(var t in db.pages[pageCount++])
            {
                Threats.Items.Add(t);
            }
           
            







        }
       
        
        
       
        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (pageCount != db.pages.Count)
            {
                Threats.Items.Clear();
                foreach(var t in db.pages[pageCount++])
                {
                    Threats.Items.Add(t);
                }
                Counter.Text = Convert.ToString(pageCount);
            }

        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (pageCount != 1)
            {
                Threats.Items.Clear();
                pageCount--;
                foreach (var t in db.pages[pageCount-1])
                {
                    Threats.Items.Add(t);
                }
                Counter.Text = Convert.ToString(pageCount);
            }

        }

        private void Threats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Threat.Text = "";
            foreach(var t in e.AddedItems)
            {
                Threat.Text += t;
                
            }
            
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
    
}
