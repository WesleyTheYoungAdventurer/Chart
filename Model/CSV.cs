using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chart.Model
{
    class CSV
    {
        //public static void TestReadCsv(string path, int XAxis, int YAxis,  out List<string> data, out List<List<string[]>> dataArry)
        //{
        //    // List<string[]> da = new List<string[]>();
        //    data = new List<string>();
        //    dataArry = new List<List<string[]>>();
        //    List<string> fileName = new List<string>();
        //    DirectoryInfo folder = new DirectoryInfo(path);
        //    foreach (FileInfo file in folder.GetFiles("*.csv"))
        //    {
        //        fileName.Add(file.FullName);
        //    }
        //    List<float> daT = new List<float>();
        //    List<float> daY = new List<float>();
        //    for (int i = 0; i < fileName.Count; i++)
        //    {
        //        int add = 0;
        //        StreamReader sr;
        //        try
        //        {
        //            using (sr = new StreamReader(fileName[i], Encoding.GetEncoding("GB2312")))
        //            {
        //                List<string[]> fileArry = new List<string[]>();
        //                string str = "";
        //                while ((str = sr.ReadLine()) != null)
        //                {
        //                    data.Add(str);
        //                    fileArry.Add(str.Split(','));
        //                    if (add > 0)
        //                    {
        //                        daT.Add(float.Parse(str.Split(',')[XAxis]));
        //                        daY.Add(float.Parse(str.Split(',')[YAxis]));
        //                    }
        //                    add++;
        //                }
        //                dataArry.Add(fileArry);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //    try
        //    {
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("读取文件数据格式错误");
        //    }
        //}
        public static void ReadCsv(string path, out List<string> data, out Dictionary<string, List<string[]>> dataDict)
        {
            dataDict = new Dictionary<string, List<string[]>>();

            data = new List<string>();

            // 获取文件夹下的指定文件
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles("*.csv");
            foreach (var i in files)
            {
                using (StreamReader sr = new StreamReader(i.FullName, Encoding.GetEncoding("GB2312")))
                {
                    List<string[]> fileArry = new List<string[]>();
                    string str = "";

                    while ((str = sr.ReadLine()) != null)
                    {
                        data.Add(str);
                        fileArry.Add(str.Split(','));
                    }
                    dataDict.Add(i.Name.TrimEnd(".csv".ToArray()), fileArry);
                }
            }
        }
    }
}