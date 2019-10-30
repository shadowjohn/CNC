using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Configuration;
using System.Runtime.Serialization;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;

namespace utility
{
    public class myinclude
    {

        public string pwd()
        {
            return Directory.GetCurrentDirectory();
        }
        public bool is_dir(string path)
        {
            return Directory.Exists(path);
        }
        public bool is_file(string filepath)
        {
            return File.Exists(filepath);
        }
        public void unlink(string filepath)
        {
            if (is_file(filepath))
            {
                File.Delete(filepath);
            }
        }


        public bool is_string_like(string data, string find_string)
        {
            return (data.IndexOf(find_string) == -1) ? false : true;
        }
        public bool is_istring_like(string data, string find_string)
        {
            return (data.ToUpper().IndexOf(find_string.ToUpper()) == -1) ? false : true;
        }

        //大小寫
        public string strtoupper(string input)
        {
            return input.ToUpper();
        }
        public string strtolower(string input)
        {
            return input.ToLower();
        }



        public string basename(string path)
        {
            return Path.GetFileName(path);
        }
        public string mainname(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
        public string subname(string path)
        {
            return Path.GetExtension(path).Replace(".", "");
        }
        public string[] explode(string keyword, string data)
        {
            return data.Split(new string[] { keyword }, StringSplitOptions.None);
        }
        public string implode(string keyword, string[] arrays)
        {
            return string.Join(keyword, arrays);
        }
        public string trim(string input)
        {
            return input.Trim();
        }
        public string implode(string keyword, List<string> arrays)
        {
            return string.Join(keyword, arrays.ToArray());
        }
        public byte[] file_get_contents(string url)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(url);
            string sContents = sr.ReadToEnd();
            sr.Close();
            return s2b(sContents);
        }
        public void file_put_contents(string filepath, byte[] input)
        {
            FileStream myFile = File.Open(@filepath, FileMode.Create);
            myFile.Write(input, 0, input.Length);
            myFile.Dispose();
        }
        public string b2s(byte[] input)
        {
            return System.Text.Encoding.UTF8.GetString(input);
        }
        public byte[] s2b(string input)
        {
            return System.Text.Encoding.UTF8.GetBytes(input);
        }
        public void mkdir(string path)
        {
            Directory.CreateDirectory(path);
        }
        public void copy(string sourceFile, string destFile)
        {
            System.IO.File.Copy(sourceFile, destFile, true);
        }
        public string dirname(string path)
        {
            return Directory.GetParent(path).FullName;
        }
        public string basedir()
        {
            //取得專案的起始位置
            return pwd();
        }


    }

}