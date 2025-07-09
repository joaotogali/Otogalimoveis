using System;
using System.Windows.Forms;

namespace Otogalimoveis.DesktopApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Troque "Form1" pelo nome do seu formulário principal, se já tiver criado outro
            Application.Run(new Form1());
        }
    }
}