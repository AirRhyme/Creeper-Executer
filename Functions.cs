using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Creeper_Executer
{
    class Functions
    {
        public static string dll = "Creeper x";

        public static OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "Lua Script Txt (*.txt)|*.txt|AllFiles (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = true,
            Title = "Creeper X"
        };

        public static void PopulateListBox(ListBox lsb, string Folder, string FileType)
        {
            DirectoryInfo dinfo = new DirectoryInfo(Folder);
            FileInfo[] files = dinfo.GetFiles(FileType);
            foreach (FileInfo file in files)
            {
                lsb.Items.Add(file.Name);
            }
        }

    }
}
