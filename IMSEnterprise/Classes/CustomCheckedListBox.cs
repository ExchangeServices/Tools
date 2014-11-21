using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace IMSEnterprise
{
    class CustomCheckedListBox : CheckedListBox
    {
    
        public CustomCheckedListBox()
        {
            this.ItemHeight = Font.Height + 5;
        }
       
        public override int ItemHeight { get; set; }

    }
}

