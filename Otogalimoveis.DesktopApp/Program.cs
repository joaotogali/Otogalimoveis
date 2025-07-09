using System;
using System.Windows.Forms;

namespace Otogalimoveis.DesktopApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            // Troque "Form1" pelo nome do seu formulário principal, se já tiver criado outro
            System.Windows.Forms.Application.Run(new Form1());
        }
    }
}