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
using Microsoft.Win32;
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
        string oldJsonPath = Directory.GetCurrentDirectory() + "oldthrlist.txt";
        ThreatDB db = new ThreatDB();
        public int pageCount = 0;
        public void Initialize()
        {
            db = new ThreatDB();
            pageCount = 0;
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

            foreach (var t in db.pages[pageCount++])
            {
                Threats.Items.Add(t);
            }
        }


       
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }
        
        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (pageCount != db.pages.Count)
            {
                string threatText = Threat.Text;
                Threats.Items.Clear();

                foreach(var t in db.pages[pageCount++])
                {
                    Threats.Items.Add(t);
                }

                Threat.Text = threatText;
                Counter.Text = Convert.ToString(pageCount);
            }
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (pageCount != 1)
            {
                string threatText = Threat.Text;
                Threats.Items.Clear();
                pageCount--;

                foreach (var t in db.pages[pageCount-1])
                {
                    Threats.Items.Add(t);
                }

                Threat.Text = threatText;
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
            
            if (File.Exists(oldJsonPath))
            {
                File.Delete(oldJsonPath);
            }
            
            File.Copy(jsonPath, oldJsonPath);
            File.Delete(jsonPath);
            File.Delete(path);
            MessageBox.Show("Будет скачан файл!");
            using (var c = new WebClient())
            {
                c.DownloadFile("https://bdu.fstec.ru/documents/files/thrlist.xlsx", path);
                db.threats = new List<Threat>();
                db.Parse(path);
            }
            
            File.WriteAllText(jsonPath, JsonConvert.SerializeObject(db.threats));

            List<Threat> oldDB = JsonConvert.DeserializeObject<List<Threat>>(File.ReadAllText(oldJsonPath));
            File.Delete(oldJsonPath);
            int ChangesCount = 0;
            List<string> changes = new List<string>();
            try
            {
                for (int i = 0; i < db.threats.Count; i++)
                {
                    if (!db.threats[i].Equals(oldDB[i]))
                    {
                        string change = "";
                        ChangesCount++;
                        if (db.threats[i].Name != oldDB[i].Name)
                        {
                            change += String.Format($"УБИ {db.threats[i].Id}\nИзменение в поле Наименование УБИ: было {oldDB[i].Name}, стало {db.threats[i].Name}  ");
                            change += "\n";
                        }
                        if (db.threats[i].Description != oldDB[i].Description)
                        {
                            change += String.Format($"УБИ {db.threats[i].Id}\nИзменение в поле Описание: было {oldDB[i].Description}, стало {db.threats[i].Description}  ");
                            change += "\n";
                        }
                        if (db.threats[i].Source != oldDB[i].Source)
                        {
                            change += String.Format($"УБИ {db.threats[i].Id}\nИзменение в поле Источник угрозы (характеристика и потенциал нарушителя): было {oldDB[i].Source}, стало {db.threats[i].Source}  ");
                            change += "\n";
                        }
                        if (db.threats[i].Subject != oldDB[i].Subject)
                        {
                            change += String.Format($"УБИ {db.threats[i].Id}\nИзменение в поле Объект воздействия: было {oldDB[i].Subject}, стало {db.threats[i].Subject}  ");
                            change += "\n";
                        }
                        if (db.threats[i].Confidentiality != oldDB[i].Confidentiality)
                        {
                            change += String.Format($"УБИ {db.threats[i].Id}\nИзменение в поле Нарушение конфиденциальности: было {oldDB[i].Confidentiality}, стало {db.threats[i].Confidentiality}  ");
                            change += "\n";
                        }
                        if (db.threats[i].Integrity != oldDB[i].Integrity)
                        {
                            change += String.Format($"УБИ {db.threats[i].Id}\nИзменение в поле Нарушение целостности: было {oldDB[i].Integrity}, стало {db.threats[i].Integrity}  ");
                            change += "\n";
                        }
                        if (db.threats[i].Availability != oldDB[i].Availability)
                        {
                            change += String.Format($"УБИ {db.threats[i].Id}\nИзменение в поле Нарушение доступности: было {oldDB[i].Availability}, стало {db.threats[i].Availability}  ");
                            change += "\n";
                        }
                        changes.Add(change);
                    }
                }

                if (ChangesCount != 0)
                {
                    MessageBox.Show($"Обновлено {ChangesCount} записей");
                    string changesList = "";
                    foreach(var change in changes)
                    {
                        changesList += change + "\n";
                    }
                    MessageBox.Show(changesList);
                }
                else
                {
                    MessageBox.Show("Изменений не обнаружено");
                }
                Threats.Items.Clear();
                Threat.Text = "";
                Counter.Text = "1";
               
                Initialize();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        
        }
        
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog.FileName = "thrlist";
            saveFileDialog.DefaultExt = ".xlsx";
            if (saveFileDialog.ShowDialog() == true)
            {
                    File.Copy(path, saveFileDialog.FileName);
            }
        }
    }
    
}
