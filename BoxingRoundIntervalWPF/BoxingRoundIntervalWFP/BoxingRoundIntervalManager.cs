using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingRoundIntervalWFP
{
    internal class BoxingRoundIntervalManager
    {
        private MainWindow mainWindow;

        public BoxingRoundIntervalManager(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        public void Initialize()
        {

            Console.WriteLine("BoxingRoundIntervalManager initialized.");
            mainWindow.BeginCountdown();
        }
    }
}
