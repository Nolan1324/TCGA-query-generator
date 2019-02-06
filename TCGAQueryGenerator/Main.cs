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
        static string fields = "md5sum,data_type,file_name,downstream_analyses.workflow_type,downstream_analyses.output_files.data_type,downstream_analyses.output_files.file_name,downstream_analyses.output_files.data_format,downstream_analyses.output_files.access,downstream_analyses.output_files.file_id,downstream_analyses.output_files.data_category,downstream_analyses.output_files.file_size,file_size,data_format,analysis.analysis_id,analysis.updated_datetime,analysis.created_datetime,analysis.submitter_id,analysis.state,analysis.workflow_link,analysis.workflow_type,analysis.workflow_version,submitter_id,access,platform,state,file_id,data_category,associated_entities.entity_id,associated_entities.case_id,associated_entities.entity_submitter_id,associated_entities.entity_type,experimental_strategy";
        string list = "";
        string types = "";

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
                typeBox.Enabled = true;
            }
        }

        private void typeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (typeBox.SelectedIndex)
            {
                case 0:
                    types = "\"*.htseq.counts*\", \"*.FPKM.txt*\", \"*.FPKM-UQ.txt*\"";
                    break;
                case 1:
                    types = "\"*.isoforms.quantification.txt*\"";
                    break;
                case 2:
                    types = "\"*.htseq.counts*\", \"*.FPKM.txt*\", \"*.FPKM-UQ.txt*\", \"*.isoforms.quantification.txt*\"";
                    break;
            }

            copyButton.Enabled = true;
            manifestButton.Enabled = true;
        }

        private void query_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"files.file_name in [{types}] and cases.project.program.name in [\"TCGA\"] and cases.submitter_id in [{list}]");
            fileNameBox.Text = "Query copied to clipboard. Use Ctrl + V to paste";
        }

        private void manifest_Click(object sender, EventArgs e) {
            string post = "{\"filters\":{\"op\":\"and\",\"content\":[{\"op\":\"in\",\"content\":{\"field\":\"files.file_name\",\"value\":[" + types + "]}},{\"op\":\"in\",\"content\":{\"field\":\"cases.project.program.name\",\"value\":[\"TCGA\"]}},{\"op\":\"in\",\"content\":{\"field\":\"cases.submitter_id\",\"value\":[" + list + "]}}]},\"size\":\"9999999\",\"fields\":\"" + fields + "\"}";
            var byteArray = Encoding.UTF8.GetBytes(post);
            var nameManifest = "";
            var nameMetadata = "";

            SaveFileDialog saveFileDialogManifest = new SaveFileDialog();
            saveFileDialogManifest.DefaultExt = ".txt";
            saveFileDialogManifest.Title = "Choose file to download manifest to";
            saveFileDialogManifest.OverwritePrompt = false;
            if (saveFileDialogManifest.ShowDialog() == DialogResult.OK)
            {
                SaveFileDialog saveFileDialogMetadata = new SaveFileDialog();
                saveFileDialogMetadata.DefaultExt = ".json";
                saveFileDialogMetadata.Title = "Choose file to download metadata to";
                saveFileDialogMetadata.OverwritePrompt = false;

                if (saveFileDialogMetadata.ShowDialog() == DialogResult.OK)
                {
                    #region Manifest
                    var pathManifest = saveFileDialogManifest.FileName;
                    //If the file exists, append a version number
                    pathManifest = AlterExistingFileName(pathManifest, ".txt");
                    nameManifest = pathManifest.Split('\\').Last();

                    WebRequest requestManifest = WebRequest.Create(new Uri("https://api.gdc.cancer.gov/files?return_type=manifest"));
                    requestManifest.Method = "POST";
                    requestManifest.ContentType = "application/json";
                    requestManifest.ContentLength = byteArray.Length;
                    try
                    {
                        using (var dataStream = requestManifest.GetRequestStream())
                        {
                            dataStream.Write(byteArray, 0, byteArray.Length);
                        }
                        using (var response = (HttpWebResponse)requestManifest.GetResponse())
                        {
                            using (var file = File.OpenWrite(pathManifest))
                            {
                                response.GetResponseStream().CopyTo(file);
                            }
                        }
                    }
                    catch (WebException)
                    {
                        fileNameBox.Text = "Error connecting to API. Make sure internet is working";
                        return;
                    }
                    #endregion

                    #region Metadata
                    var pathMetadata = saveFileDialogMetadata.FileName;

                    //If the file exists, append a version number
                    pathMetadata = AlterExistingFileName(pathMetadata, ".json");
                    nameMetadata = pathMetadata.Split('\\').Last();

                    WebRequest requestMetadata = WebRequest.Create(new Uri("https://api.gdc.cancer.gov/files"));
                    requestMetadata.Method = "POST";
                    requestMetadata.ContentType = "application/json";
                    requestMetadata.ContentLength = byteArray.Length;
                    try
                    {
                        using (var dataStream = requestMetadata.GetRequestStream())
                        {
                            dataStream.Write(byteArray, 0, byteArray.Length);
                        }
                        using (var response = (HttpWebResponse)requestMetadata.GetResponse())
                        {
                            StreamReader reader = new StreamReader(response.GetResponseStream());
                            string text = reader.ReadToEnd();
                            int startIndex = text.IndexOf("[");
                            File.WriteAllText(pathMetadata, text.Substring(startIndex, (text.LastIndexOf("]") + 1) - startIndex));
                        }
                    }
                    catch (WebException)
                    {
                        fileNameBox.Text = "Error connecting to API. Make sure internet is working";
                        return;
                    }
                    #endregion

                    fileNameBox.Text = $"Data downloaded to {nameManifest} and {nameMetadata}";
                }
            }
        }

        private String AlterExistingFileName(String path, String ext)
        {
            if (File.Exists(path))
            {
                var version = 1;
                while (File.Exists(path + "." + version + ".txt"))
                {
                    version++;
                }
                return path + "." + version + ".txt";
            }
            return path;
        }

        private void detailsLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Details().ShowDialog();
        }
    }
}
