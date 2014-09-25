using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class MainWindowVM:ViewModelSupport.ViewModelBase
    {

        public bool Slide
        {
            get { return Get(() => Slide); }
            set { Set(() => Slide, value); }
        }

        public bool SlideGreen
        {
            get { return Get(() => SlideGreen); }
            set { Set(() => SlideGreen, value); }
        }

        public bool SlideOut
        {
            get { return Get(() => SlideOut); }
            set { Set(() => SlideOut, value); }
        }

        public bool SlideGreenOut
        {
            get { return Get(() => SlideGreenOut); }
            set { Set(() => SlideGreenOut, value); }
        }

        public MainWindowVM()
        {
            SlideGreen = true;
        }
        bool toggle = false;
        public void Execute_Click()
        {
            SlideGreen = !SlideGreen;

        }
    }
}
