using System;
using System.IO;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace Library.Entities
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public static class Utilities
    {
        #region Methods

        /// <summary>
        /// Get a Specific Connection String
        /// </summary>
        /// <param name="connectionName">Name of the Connecction String</param>
        /// <returns>Connection String</returns>
        public static string GetConnectionString(string connectionName)
        {
            try
            {
                return ConfigurationManager.ConnectionStrings.Count > 0
                           ? ConfigurationManager.ConnectionStrings[connectionName].ConnectionString
                           : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the value of a specific AppSetting
        /// </summary>
        /// <param name="appSettingName">AppSetting Name</param>
        /// <returns>AppSetting Value</returns>
        public static string GetAppValue(string appSettingName)
        {
            try
            {
                return WebConfigurationManager.AppSettings.Count > 0
                           ? WebConfigurationManager.AppSettings[appSettingName]
                           : string.Empty;
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }

        /// <summary>
        /// Write a text in a file
        /// </summary>
        /// <param name="text">Text to save</param>
        /// <param name="filePath">File Path</param>
        public static void WriteInFile(string text, string filePath)
        {
            try
            {
                var date = DateTime.Now;
                var textToSave = new StringBuilder();

                textToSave.Append(date.ToString("yyyy/MM/dd hh:mm:ss:ffff"));
                textToSave.Append(" - ");
                textToSave.Append(text);
                textToSave.Append(" - ");
                textToSave.Append(filePath);

                if (File.Exists(filePath))
                {
                    using (var streamWriter = File.AppendText(filePath))
                    {
                        streamWriter.WriteLine(textToSave.ToString());
                    }
                }
                else
                {
                    using (var streamWriter = File.CreateText(filePath))
                    {
                        streamWriter.WriteLine(textToSave.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("There is an error trying to write in the file.\n" + e.Message);
            }
        }

        /// <summary>
        /// Get the parent path of a folder
        /// </summary>
        /// <param name="folderName">Folder Name</param>
        /// <returns>Parent path</returns>
        public static string GetParentPath(string folderName)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            while (Directory.GetParent(path) != null)
            {
                path = Directory.GetParent(path).ToString();
                if (Directory.Exists(path + "\\" + folderName))
                {
                    return path + "\\" + folderName;
                }
            }

            return null;
        }

        #endregion
    }
}
