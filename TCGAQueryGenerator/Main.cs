using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

/**
* @author Nolan Kuza (nkuza8@gmail.com)
* @date - 12/22/18
*/

namespace TCGAQueryGenerator
{
    public partial class Main : Form
    {
        string list = "";

        public Main()
        {
            MaximizeBox = false;
            MinimizeBox = false;
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select file containing list of TCGA ids";
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                var path = openFileDialog.FileName;
                var name = path.Split('\\').Last();
                fileNameBox.Text = $"File {name} selected";

                list = "";
                using (var file = new StreamReader(path))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        var id = line.Trim('"');
                        if(id.Length > 12)
                        {
                            id = id.Substring(0, 12);
                        }
                        list += $"\"{id}\",";
                    }
                    list = list.Remove(list.Length - 1, 1);
                }

                fileNameBox.Text = $"File {name} loaded into query";
                copyButton.Enabled = true;
                manifestButton.Enabled = true;
            }
        }

        private void query_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"cases.project.program.name in [\"TCGA\"] and cases.submitter_id in [{list}]");
            fileNameBox.Text = "Query copied to clipboard. Use Ctrl + V to paste";
        }

        private void manifest_Click(object sender, EventArgs e) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.Title = "Choose file to download manifest to";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var path = saveFileDialog.FileName;
                var name = path.Split('\\').Last();

                string post = "{\"filters\":{\"op\":\"and\",\"content\":[{\"op\":\"in\",\"content\":{\"field\":\"cases.project.program.name\",\"value\":[\"TCGA\"]}},{\"op\":\"in\",\"content\":{\"field\":\"cases.submitter_id\",\"value\":[" + list + "]}}]},\"size\":\"9999999\"}";
                Debug.WriteLine(post);
                var byteArray = Encoding.UTF8.GetBytes(post);

                WebRequest request = WebRequest.Create(new Uri("https://api.gdc.cancer.gov/files?return_type=manifest"));
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                try
                {
                    using (var dataStream = request.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                    }
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        using (var file = File.OpenWrite(path))
                        {
                            response.GetResponseStream().CopyTo(file);
                        }
                    }
                    fileNameBox.Text = $"Mainfest downloaded to {name}";
                }
                catch (WebException ex)
                {
                    fileNameBox.Text = "Error connecting to API. Make sure internet is working";
                }
            }
        }

        private void detailsLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Details().ShowDialog();
        }
    }
}
