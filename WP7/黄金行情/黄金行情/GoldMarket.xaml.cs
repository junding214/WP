using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Xml.Linq;
using ImageTools;
using ImageTools.IO;
using ImageTools.IO.Gif;
using Microsoft.Phone.Controls;

namespace GoldMarket
{
    public partial class GoldMarket : PhoneApplicationPage
    {


        public GoldMarket()
        {
            InitializeComponent();
            Decoders.AddDecoder<GifDecoder>();
            LoadXlm();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadXlm()
        {
            XElement carMakers;

            carMakers = XElement.Load("config.xml");

            var items = from student in carMakers.Descendants("Item")
                        select new Item
                            {
                                Name = student.Element("Name").Value,
                                Url = student.Element("Url").Value,
                                FileName = student.Element("FileName").Value,
                                RefreshRate = int.Parse(student.Element("RefreshRate").Value),
                                RefreshUnits = student.Element("RefreshUnits").Value,
                                DownLoadTime = new DateTime(1900, 01, 01),
                            };

            foreach (Item a in items)
            {
                _bindData.Add(a);
            }
        }

        private ObservableCollection<Item> _bindData = new ObservableCollection<Item>();


        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            switch (pivo.SelectedIndex)
            {
                case 0:
                    LoadPic(pivo.SelectedIndex, ref today);
                    break;
                case 1:
                    LoadPic(pivo.SelectedIndex, ref oneYear);
                    break;
                case 2:
                    LoadPic(pivo.SelectedIndex, ref twoYears);
                    break;
                case 3:
                    LoadPic(pivo.SelectedIndex, ref fiveYears);
                    break;
                case 4:
                    LoadPic(pivo.SelectedIndex, ref tenYears);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="imgContain"></param>
        private void LoadPic(int index, ref ImageTools.Controls.AnimatedImage imgContain)
        {
            if (imgContain.Source != null)
            {
                if (!OutOfTime(index))
                {
                    return;
                }
            }

            //ExtendedImage imgTmp = new ExtendedImage();

            //    imgTmp.UriSource = new Uri("/WaitFor.gif", UriKind.Relative);

            //imgContain.Source = imgTmp;

            // return;

            ExtendedImage image = new ImageTools.ExtendedImage();
            image.UriSource = new Uri(_bindData[index].Url);
            imgContain.Source = image;

            _bindData[index].DownLoadTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                                         DateTime.Now.Hour, DateTime.Now.Minute, 0);

        }

        private bool OutOfTime(int index)
        {
            int[] intNow =
                {
                    DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute
                };

            int[] intPre =
                {
                    _bindData[index].DownLoadTime.Year, _bindData[index].DownLoadTime.Month,
                    _bindData[index].DownLoadTime.Day, _bindData[index].DownLoadTime.Hour,
                    _bindData[index].DownLoadTime.Minute
                };

            if (_bindData[index].RefreshRate == 0)
                return true;
            else
            {
                if (_bindData[index].RefreshUnits == "年")
                {
                    if (intNow[0] - intPre[0] >= _bindData[index].RefreshRate)
                        return true;
                    else

                        return false;

                }

                //比月份、日期......
                if (intNow[0] > intPre[0])
                    return true;

                if (_bindData[index].RefreshUnits == "月")
                {
                    if (intNow[1] - intPre[1] >= _bindData[index].RefreshRate)
                        return true;
                    else
                        return false;
                }

                //比日期 时间...
                if (intNow[1] > intPre[1])
                    return true;

                if (_bindData[index].RefreshUnits == "日")
                {
                    if (intNow[2] - intPre[2] >= _bindData[index].RefreshRate)
                        return true;
                    else
                        return false;
                }

                //小时 分钟...
                if (intNow[2] > intPre[2])
                    return true;

                if (_bindData[index].RefreshUnits == "时")
                {
                    if (intNow[3] - intPre[3] >= _bindData[index].RefreshRate)
                        return true;
                    else
                        return false;
                }

                //分钟...
                if (intNow[3] > intPre[3])
                    return true;

                if (_bindData[index].RefreshUnits == "分")
                {
                    if (intNow[4] - intPre[4] >= _bindData[index].RefreshRate)
                        return true;
                    else
                        return false;

                }
            }
            return true;
        }

        private class Item
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public string FileName { get; set; }
            public int RefreshRate { get; set; }
            public string RefreshUnits { get; set; }
            public DateTime DownLoadTime { get; set; }
        }

        private void menuOnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }
    }
}



