using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GarisKBT.Helper
{
    public class StackLayoutBase : StackLayout
    {
        private string id;
        
        public string Sid  { get => id; set => id = value; }
    }
}
