using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace SchoolLang
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        List<Service> ServiswList1 = BD.CE.Service.ToList();
        List<Service> ServiswList = new List<Service>();

        
        public Admin()
        {
            InitializeComponent();
            ServiswList = ServiswList1;
            DGServises.ItemsSource = ServiswList;
            CBPeople.ItemsSource = BD.CE.Client.ToList();
            CBPeople.SelectedValuePath = "ID";
            CBPeople.DisplayMemberPath = "People";
        }

        int i = -1;
        private void MediaElement_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                i++;
                MediaElement ME = (MediaElement)sender;
                Service S = ServiswList[i];
                Uri U = new Uri(S.MainImagePath, UriKind.RelativeOrAbsolute);
                ME.Source = U;  
            }
        }  

        private void TextBlock_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service S = ServiswList[i];
                TB.Text = S.Title;
                //  i++;
            }

        }
        private void StackPanel_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                StackPanel SP = (StackPanel)sender;
                Service S = ServiswList[i];
                if (S.Discount != 0)
                {
                    SP.Background = new SolidColorBrush(Color.FromRgb(231, 250, 191));
                }
            }

        }


        //Инициализация кнопок
        private void Button_Initialized_Red(object sender, EventArgs e)
        {
            Button BtnRed = (Button)sender;
            if (BtnRed != null)
            {
                BtnRed.Uid = Convert.ToString(i);
            }

        }

        private void Button_Initialized_Del(object sender, EventArgs e)
        {
            Button BtnDel = (Button)sender;
            if (BtnDel != null)
            {
                BtnDel.Uid = Convert.ToString(i);
            }
        }

        private void Button_Initialized_Add(object sender, EventArgs e)
        {
            Button BtnAdd = (Button)sender;
            if (BtnAdd != null)
            {
                BtnAdd.Uid = Convert.ToString(i);
            }
        }
        private void TextBlock_Initialized_Cost(object sender, EventArgs e)
        {


            if (i < ServiswList.Count)
            {
                TextBlock Price = (TextBlock)sender;
                Service S = ServiswList[i];
                Price.Text = Convert.ToInt32(S.Cost) + "";
            }
        }
        private void TextBlock_Initialized_Duration(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                TextBlock Duration = (TextBlock)sender;
                Service S = ServiswList[i];
                Duration.Text = Convert.ToString(S.DurationInSeconds / 60 + " Минут");
                //  i++;
            }
        }

        private void TextBlock_Initialized_Discount(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                TextBlock Disc = (TextBlock)sender;
                Service S = ServiswList[i];
                Disc.Text = Convert.ToString(S.Discount * 100 + " %");
            }

        }


        // Реадктирование
        Service S1;
        private void BReg_Click(object sender, RoutedEventArgs e)
        {
            Button BEdit = (Button)sender;
            int ind = Int32.Parse(BEdit.Uid);
            S1 = ServiswList[ind];
            MSP.Visibility = Visibility.Collapsed;
            SPRed.Visibility = Visibility.Visible;
            TBIRId.Text = Convert.ToString(S1.ID);
            TBRTitle.Text = S1.Title;
            TBRCost.Text = Convert.ToInt32(S1.Cost) + "";
            TBRTime.Text = Convert.ToString(S1.DurationInSeconds / 60);
            TBRSale.Text = Convert.ToString(S1.Discount);
            TBRImage.Text = S1.MainImagePath;
        }

        private void BRReg_Click(object sender, RoutedEventArgs e)
        {
            S1.Title = TBRTitle.Text;
            S1.Cost = Convert.ToDecimal(TBRCost.Text);
            S1.DurationInSeconds = Convert.ToInt32(TBRTime.Text);
            S1.Discount  = Convert.ToDouble(TBRSale.Text) ;
            S1.MainImagePath = TBRImage.Text;
            BD.CE.SaveChanges();
            MessageBox.Show("Запись изменена");

        }

        private void RImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.ShowDialog();
            string path = OFD.FileName;
            TBRImage.Text = path;
        }

        private void BRBack_Click(object sender, RoutedEventArgs e)
        {
            ClassF.Mfrm.Navigate(new Admin());          
        }


        //Удаление
        private void BDel_Click(object sender, RoutedEventArgs e)
        {
            Button BtnDel = (Button)sender;
            int ind = Convert.ToInt32(BtnDel.Uid);
            Service S = ServiswList[ind];
            BD.CE.Service.Remove(S);
            MessageBox.Show("Удалена");
            BD.CE.SaveChanges();
            ClassF.Mfrm.Navigate(new Admin());
        }



        //Добавить услугу
        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            
            SPRed.Visibility = Visibility.Collapsed;
            MSP.Visibility = Visibility.Collapsed;
            SPAdd.Visibility = Visibility.Visible;
        }

        private void BAReg_Click(object sender, RoutedEventArgs e)
        {
            Service S = new Service();
            S.Title = TBATitle.Text;
            S.Cost = Convert.ToInt32(TBACost.Text);
            S.DurationInSeconds = Convert.ToInt32(TBATime.Text) * 60;
            S.Discount = Convert.ToDouble(TBASale.Text);
            S.Description = TBADescription.Text;
            S.MainImagePath = TBAImage.Text;
            
            BD.CE.Service.Add(S);
            BD.CE.SaveChanges();

            
        }

        private void AImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.ShowDialog();
            string path = OFD.FileName;
            TBAImage.Text = path;
        }


        private void BABack_Click(object sender, RoutedEventArgs e)
        {
            ClassF.Mfrm.Navigate(new Admin());
        }


        //Новый заказ
        private void BOrder_Click(object sender, RoutedEventArgs e)
        {
            Button BEdit = (Button)sender;
            int ind = Int32.Parse(BEdit.Uid);
            S1 = ServiswList[ind];
            TBOTitle.Text = Convert.ToString(S1.Title);
            TBOTime.Text = Convert.ToString(S1.DurationInSeconds / 60 + " Минут");


            SPRed.Visibility = Visibility.Collapsed;
            MSP.Visibility = Visibility.Collapsed;
            SPAdd.Visibility = Visibility.Collapsed;
            STOrder.Visibility = Visibility.Visible;
        }
        DateTime DT;
        private void TBTime_TextChanged(object sender, TextChangedEventArgs e)
        {

                Regex Time = new Regex("[0-1][0-9]:[0-5][0-9]");
                Regex Time1 = new Regex("2[0-3]:[0-5][0-9]");
                if(Time.IsMatch(TBTime.Text) || Time1.IsMatch(TBTime.Text) && TBTime.Text.Length == 5)
            {
                //MessageBox.Show(TBTime.Text);
                TimeSpan TS = TimeSpan.Parse(TBTime.Text);
                DT = Convert.ToDateTime(DP.SelectedDate);
                DT = DT.Add(TS);
                if(DT > DateTime.Now)
                {
                    MessageBox.Show(DT + "");
                }
                else
                {
                    MessageBox.Show("Неактуалная дата");
                    Zap.IsEnabled = false;
                }
            }
            else
            {
                if (TBTime.Text.Length >= 5)
                {
                    MessageBox.Show("Неверное время");
                    Zap.IsEnabled = false;
                }
            }
        }
        private void TBOBack_Click(object sender, RoutedEventArgs e)
        {
            ClassF.Mfrm.Navigate(new Admin());
        }
       
        private void Zap_Click(object sender, RoutedEventArgs e)
        {
            Service S = ServiswList[i];
            int index = (int)CBPeople.SelectedValue;

            ClientService obj = new ClientService()
            {
                ClientID = index,
                ServiceID = S.ID,
                StartTime = DT
            };
            BD.CE.ClientService.Add(obj);
            BD.CE.SaveChanges();
            MessageBox.Show("Запись добавлена");

        }

        //Сортировка

        private void SortUp_Click(object sender, RoutedEventArgs e)
        {
            i = -1;
            ServiswList.Sort((x, y) => x.Cost.CompareTo(y.Cost));
            ServiswList.Reverse();
            DGServises.Items.Refresh();
        }

        private void SortDown_Click(object sender, RoutedEventArgs e)
        {
            i = -1;
            ServiswList.Sort((x, y) => x.Cost.CompareTo(y.Cost));
            DGServises.Items.Refresh();
        }


        List<Service> ServiseListFilter = new List<Service>();
        private void CBFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            i = -1;
            switch (CBFilter.SelectedIndex)
            {
                case 0:
                    ServiseListFilter = ServiswList.Where(x => x.Discount <= 0.05).ToList();
                    ServiswList = ServiseListFilter;
                    DGServises.ItemsSource = ServiswList;
                    break;
                case 1:
                    ServiseListFilter = ServiswList.Where(x => x.Discount > 0.05 && x.Discount <0.15).ToList();
                    ServiswList = ServiseListFilter;
                    DGServises.ItemsSource = ServiswList;
                    break;
                case 2:
                    ServiseListFilter = ServiswList.Where(x => x.Discount > 0.15 && x.Discount < 0.3).ToList();
                    ServiswList = ServiseListFilter;
                    DGServises.ItemsSource = ServiswList;
                    break;
                case 3:
                    ServiseListFilter = ServiswList.Where(x => x.Discount > 0.3 && x.Discount < 0.7).ToList();
                    ServiswList = ServiseListFilter;
                    DGServises.ItemsSource = ServiswList;
                    break;
                case 4:
                    ServiseListFilter = ServiswList.Where(x => x.Discount > 0.7 && x.Discount < 1).ToList();
                    ServiswList = ServiseListFilter;
                    DGServises.ItemsSource = ServiswList;
                    break;
                case 5:
                    ServiseListFilter = ServiswList.Where(x => x.Discount < 1).ToList();
                    ServiswList = ServiswList1;
                    DGServises.ItemsSource = ServiswList;
                    break;

            }

        }

        private void TBPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            i = -1;
            if (TBPoisk.Text != "")
            {
                List<Service> ServiseListPoisk = new List<Service>();
                ServiseListPoisk = ServiswList.Where(x => x.Title.Contains(TBPoisk.Text)).ToList();
                ServiswList = ServiseListPoisk;
                DGServises.ItemsSource = ServiswList;
            }
            else
            {
                if (ServiseListFilter.Count == 0)
                {
                    ServiswList = ServiswList1;
                    DGServises.ItemsSource = ServiswList;
                }
                else
                {
                    ServiswList = ServiseListFilter;
                    DGServises.ItemsSource = ServiswList;
                }
            }

        }
    }
}
