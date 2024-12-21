using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg
{
    public class FileUtil
    {
        public static string GetAndDeleteLine(string FileTxT)
        {
            int num = 0;
            string[] array = File.ReadAllLines(FileTxT);
            if (array == null || array.Length <= 0)
            {
                return "";
            }
            string result = array[0];
            using (StreamReader streamReader = new StreamReader(FileTxT))
            {
                using (StreamWriter streamWriter = new StreamWriter("FileĐệm.txt"))
                {
                    string value;
                    while ((value = streamReader.ReadLine()) != null)
                    {
                        num++;
                        bool flag = num == 1;
                        if (!flag)
                        {
                            streamWriter.WriteLine(value);
                        }
                    }
                }
            }
            string value2 = File.ReadAllText("FileĐệm.txt");
            File.Delete("FileĐệm.txt");
            File.Delete(FileTxT);
            using (StreamWriter streamWriter2 = new StreamWriter(FileTxT, true))
            {
                streamWriter2.WriteLine(value2);
                streamWriter2.Close();
            }
            return result;
        }
    }
}
