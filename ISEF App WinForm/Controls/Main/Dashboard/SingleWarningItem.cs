using cvti.data.Classifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvti.isef.winformapp.Controls.Main.Dashboard
{
    public class SingleWarningItem
    {
        public SingleWarningItem(ChybajuciRiadok item)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }

        public ChybajuciRiadok Item { get; }
    }
}
