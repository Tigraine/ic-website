namespace ImagineClub.Web.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class FileHelper
    {
        private IDictionary<string, string> fileEndings = new Dictionary<string, string>()
                                                              {
                                                                  {".pdf", "page_white_acrobat"},
                                                                  {".xls", "page_white_excel"},
                                                                  {".xlsx", "page_white_excel"},
                                                                  {".doc", "page_white_word"},
                                                                  {".docx", "page_white_word"},
                                                                  {".zip", "page_white_zip"},
                                                                  {".rar", "page_white_zip"},
                                                                  {".7z", "page_white_zip"},
                                                                  {".ppt", "page_white_powerpoint"},
                                                                  {".pptx", "page_white_powerpoint"},
                                                                  {".cs", "page_white_code"},
                                                                  {".patch", "page_white_code"},
                                                                  {".jpg", "page_white_picture"},
                                                                  {".gif", "page_white_picture"},
                                                                  {".png", "page_white_picture"}
                                                              };

        public string GetIconForFile(string fileName)
        {
            var baseUrl = "/Content/images/fileicons/";
            foreach (var ending in fileEndings.Keys)
            {
                if (fileName.EndsWith(ending))
                    return String.Format("{0}{1}.png", baseUrl, fileEndings[ending]);
            }
            return String.Format("{0}{1}.png", baseUrl, "page_white");
        }

        public string GetFileEnding(string fileName)
        {
            if (!fileName.Contains(".")) return "";
            var regex = new Regex(@"(\.[^.]*$)");
            var match = regex.Match(fileName);
            return match.Value.ToUpper();
        }

        public string FuzzyFileSize(long size)
        {
            long newSize = size;
            string unit;
            if (size < 1024)
                unit = "bytes";
            if (size < 1048576)
            {
                newSize = size/1024;
                unit = "KB";
            }
            else
            {
                newSize = size/(1024*1024);
                unit = "MB";
            }

            return String.Format("{0} {1}", newSize, unit);
        }
    }
}