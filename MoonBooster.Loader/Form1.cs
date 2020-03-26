using SharpMonoInjector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoonBooster.Loader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                if(Process.GetProcessesByName("rustclient").Length < 1)
                {
                    MessageBox.Show("Игра не запущена!");
                    return;
                }
                try
                {
                    Injector i = new Injector("rustclient");
                    using (i)
                    {
                        i.Inject(Resource1.MoonBooster_196, "MoonBooster", "Core", "Start");
                    }
                    MessageBox.Show("Готово!");
                    Environment.Exit(0);
                } catch
                {
                    MessageBox.Show("Ошибки при инжектирование!\nКод ошибки: 0x013000F");
                }
            }
            if(comboBox1.SelectedIndex == 1)
            {
                if (Process.GetProcessesByName("rustclient").Length < 1)
                {
                    MessageBox.Show("Игра не запущена!");
                    return;
                }
                try
                {
                    Injector i = new Injector("rustclient");
                    using (i)
                    {
                        i.Inject(Resource1.MoonBooster, "MoonBooster", "Core", "Start");
                    }
                    MessageBox.Show("Готово!");
                    Environment.Exit(0);
                }
                catch
                {
                    MessageBox.Show("Ошибки при инжектирование!\nКод ошибки: 0x013500F");
                }
            }
        }
    }
}
